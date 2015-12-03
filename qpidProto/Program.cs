using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Org.Apache.Qpid.Messaging;

/*Dll's utilizadas:*/

/*Comando para gerar os .cs a partir do .proto
 ProtoGen.exe ProtoFiles\mxt1xx.proto -output_directory=Dotnet*/

/*Material de apoio
https://developers.google.com/protocol-buffers/
 */

namespace maxtrack
{
    class Program
    {
        public static void Main(string[] args)
        {

            string broker_ip = "192.168.0.78:5672";
            string data_queue = "fila_dados_ext";
            string command_queue = "mxt_command_qpid";
            byte[] bMessage = null;
            Connection connection = null;
            bool response_received = true;

            try
            {
                /*Cria a conexão com o Qpid e abre a sessão sem autenticar*/
                connection = new Connection(broker_ip);
                connection.Open();
                Session session = connection.CreateSession();
                Receiver receiver = session.CreateReceiver(data_queue + "; {create: always}");
                Sender sender = session.CreateSender(command_queue + "; {create: always}");

                /*Cria uma nova mensagem e recebe a proxima mensagem da fila se o Fetch() 
                 * não tiver argumentos ele irá esperar a proxima mensagem sem tempo
                 * definido use (DurationConstants.SECOND * 1) para aguardar 1 segundo*/
                while (true)
                {
                    Message message = new Message();
                    bool message_receive = receiver.Fetch(ref message);
                    String data_gateway = message.ContentType;
                    while (message_receive)
                    {
                        /*Classe para verificar o tipo da mensagem*/
                        ConsoleApplication4.Class1 c1 = new ConsoleApplication4.Class1();
                        ConsoleApplication4.Class1.qpid_subject_type type = c1.get_subject_type(message.Subject);

                        switch (type)
                        {
                            case ConsoleApplication4.Class1.qpid_subject_type.qpid_st_none:
                                Console.WriteLine("Unknow Message");
                                break;
                            case ConsoleApplication4.Class1.qpid_subject_type.qpid_st_pb_mxt1xx_pos:
                                /*Cria um novo byte array com o tamanho da mensagem*/
                                bMessage = new byte[message.ContentSize];
                                /*Escreve o conteudo da mensagem no byte array*/
                                message.GetContent(bMessage);
                                /*Constroi o objeto pos_mxt1xx usando o byte array para preenche-lo*/
                                maxtrack.pb.mxt1xx.mxt1xx_u_position pos_mxt1xx = maxtrack.pb.mxt1xx.mxt1xx_u_position.CreateBuilder().MergeFrom(bMessage).BuildPartial();

                                Console.Write("Serial: " + pos_mxt1xx.Firmware.Serial + "\n" +
                                              "Memory Index: " + pos_mxt1xx.Firmware.MemoryIndex + "\n" +
                                              "Latitude: " + pos_mxt1xx.GpsModem.Latitude + "\n" +
                                              "Longitude: " + pos_mxt1xx.GpsModem.Longitude + "\n" +
                                              "DATE: " + pos_mxt1xx.GpsModem.Date + "\n" +
                                              "CSQ: " + pos_mxt1xx.GpsModem.Csq + "\n" +
                                              "CellId: " + pos_mxt1xx.CellInfo.CellId + "\n" +
                                              "LocalAreaCode: " + pos_mxt1xx.CellInfo.LocalAreaCode + "\n" +
                                              "NetworkCode: " + pos_mxt1xx.CellInfo.NetworkCode + "\n" +
                                              "CountryCode: " + pos_mxt1xx.CellInfo.CountryCode + "\n" +
                                              "-----------------------------------------" + "\n"
                                              );

                                /*Campos Optional*/
                                if (pos_mxt1xx.Firmware.HasLifeTime)
                                {
                                    Console.WriteLine("Life Time: " + pos_mxt1xx.Firmware.LifeTime + "\n" +
                                        "-----------------------------------------" + "\n");
                                }
                                if (response_received)
                                    response_received = mxt1xx_output_control(!pos_mxt1xx.HardwareMonitor.Outputs.Output1, pos_mxt1xx, sender);
                                break;
                            case ConsoleApplication4.Class1.qpid_subject_type.qpid_st_pb_command_response:
                                bMessage = new byte[message.ContentSize];
                                message.GetContent(bMessage);
                                maxtrack.pb.commands.u_command_response response = maxtrack.pb.commands.u_command_response.CreateBuilder().MergeFrom(bMessage).BuildPartial();
                                if (response.Status == 5)
                                {
                                    Console.WriteLine("Command response: Success");
                                    response_received = true;
                                }
                                else
                                    Console.WriteLine("Command response: " + response.Status.ToString());

                                break;
                            default:
                                break;
                        }
                        /*Diz ao servidor que a mensagem foi recebida, o servidor apagará a mensagem*/
                        session.Acknowledge();
                        message_receive = false;
                        /*Fecha a conexão*/
                        //connection.Close();

                        //System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0}.", e);
                System.Threading.Thread.Sleep(10000);
            }
        }

        public static bool mxt1xx_output_control(bool activate, maxtrack.pb.mxt1xx.mxt1xx_u_position pos, Sender sender)
        {
            try
            {
                int aux = activate ? 1 : 0;
                maxtrack.pb.commands.u_command.Builder command = maxtrack.pb.commands.u_command.CreateBuilder();
                Message message = new Message();
                byte[] bMessage = null;

                command.SetProtocol(pos.Firmware.Protocol);
                command.SetSerial(pos.Firmware.Serial);
                command.SetId("Controla Saida " + pos.Firmware.Serial.ToString());
                command.SetType(51);
                command.SetAttempt(50);
                command.SetTimeout("2020-12-31 00:00:00");
                //type 2 para protobuf
                command.SetHandlerType(2);
                command.SetTransport("GPRS");

                maxtrack.pb.commands.u_parameter.Builder parameter = maxtrack.pb.commands.u_parameter.CreateBuilder();

                parameter.SetId("SET_OUTPUT");
                parameter.SetValue("1");
                command.AddParameter(parameter);

                parameter.SetId("SET OUTPUT 1");
                parameter.SetValue(aux.ToString());
                command.AddParameter(parameter);

                parameter.SetId("SET OUTPUT 2");
                parameter.SetValue(aux.ToString());
                command.AddParameter(parameter);

                parameter.SetId("SET OUTPUT 3");
                parameter.SetValue(aux.ToString());
                command.AddParameter(parameter);

                parameter.SetId("SET OUTPUT 4");
                parameter.SetValue(aux.ToString());
                command.AddParameter(parameter);

                maxtrack.pb.commands.u_command new_command = command.Build();
                using (MemoryStream stream = new MemoryStream())
                {
                    new_command.WriteTo(stream);
                    bMessage = stream.ToArray();
                }
                message.SetProperty("qpid.subject", "PB_COMMAND");
                message.SetContent(bMessage);
                sender.Send(message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0}.", e);
                System.Threading.Thread.Sleep(10000);
                return true;
            }
        }



    }
}