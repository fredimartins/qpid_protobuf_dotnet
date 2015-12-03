using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication4
{
    public class Class1
    {
        const int MAX_SUBJECT_TYPES = 62;

        public string[] qpid_subject = new string[MAX_SUBJECT_TYPES]
    {
    "",
  "PB_MTC550_POSITION",
  "PB_MTC550_SETUP",
  "PB_MXT1xx_POSITION",
  "PB_MXT1xx_SETUP",
  "PB_MTC500_POSITION",
  "PB_MTC500_SETUP",
  "PB_MTC400_POSITION",
  "PB_MTC400_SETUP",
  "PB_IDP780_POSITION",
  "PB_IDP780_SETUP",
  "PB_IDP780_SETUP_VAR",
  "PB_MTC550SKYWAVE_POSITION",
  "PB_TD50SKYWAVE_POSITION",
  "PB_MXT1xx_G100",
  "PB_IDP780SKYWAVE_POSITION",
  "PB_IDP780SKYWAVE_SETUP",
  "PB_IDP780SKYWAVE_SETUP_VAR",
  "PB_MTC550_BLACK_BOX_MINUTE",
  "PB_COMMAND",
  "PB_COMMAND_RESPONSE",
  "LIBRARY_RESPONSE",
  "HUGE_LIBRARY_RESPONSE",
  "PB_MODULE_CONNECTION",
  "PB_MXT1xx_IP_GPRS_CONNECTION",
  "PB_MTC550SKYWAVE_POSITION_AND_FREE_DATA",
  "PB_COMMAND_DATA_TERMINAL_PWD_GENERATOR_RESPONSE",
  "PB_MXT1XX_ICC_ID_ANSWER",
  "PB_MTC550SATAMATICS_POSITION",
  "PB_MTC550SATAMATICS_POSITION_AND_FREE_DATA",
  "PB_IDP780_BLACK_BOX_MINUTE",
  "PB_MXT1XX_VERSION_PACKET",
  "PB_IDP780SKYWAVE_BLACK_BOX_MINUTE",
  "PB_MTC550_PARAMETERS",
  "PB_IDP780SKYWAVE_REDUCED_POSITION",
  "PB_IDP780SKYWAVE_REDUCED_EMBEDDED_ACTION",
  "PB_IDP780SKYWAVE_REDUCED_MACRO",
  "PB_IDP780SKYWAVE_REDUCED_DELTAS",
  "PB_IDP780SKYWAVE_REDUCED_TELEMETRY_EVENTS",
  "PB_IDP780SKYWAVE_REDUCED_ACCELEROMETER_EVENTS",
  "PB_IDP780SKYWAVE_REDUCED_TRIP",
  "PB_IDP780SKYWAVE_REDUCED_TEXT",
  "PB_IDP780SKYWAVE_REDUCED_ADC",
  "PB_IDP780SKYWAVE_REDUCED_OPEN_DATA",
  "PB_IDP780SKYWAVE_REDUCED_TELEMETRY",
  "PB_MTC550SKYWAVE_REDUCED_POSITION",
  "PB_IDP780_EXTENDED_LOG",
  "PB_IDP780_ICCID",
  "PB_IDP780SKYWAVE_ICCID",
  "PB_IDP780_FILES_LIST",
  "PB_IDP780SKYWAVE_FILES_LIST",
  "PB_MXT1XX_TRANSPARENT_RESPONSE",
  "PB_MTC550_PRESENTATION",
  "PB_IDP780_PRESENTATION",
  "PB_IDP780_HW_INFO",
  "MXT1XX_KEEP_ALIVE",
  "PB_IDP780SKYWAVE_HW_INFO",
  "PB_IDP780_BLACK_BOX_INFO",
  "PB_MXT1XX_REQUEST_OLD_POS_STS",
  "PB_MTC550_BLACK_BOX_INFO",
  "PB_MXT1XX_DEVICE_INFORMATION",
  "PB_MXT1XX_DYNAMIC_SETUP"};

        public enum qpid_subject_type
        {
            qpid_st_none = 0,
            qpid_st_pb_mtc550_pos = 1,
            qpid_st_pb_mtc550_setup = 2,
            qpid_st_pb_mxt1xx_pos = 3,
            qpid_st_pb_mxt1xx_setup = 4,
            qpid_st_pb_mtc500_pos = 5,
            qpid_st_pb_mtc500_setup = 6,
            qpid_st_pb_mtc400_pos = 7,
            qpid_st_pb_mtc400_setup = 8,
            qpid_st_pb_idp780_pos = 9,
            qpid_st_pb_idp780_setup = 10,
            qpid_st_pb_idp780_setup_var = 11,
            qpid_st_pb_mtc550skywave_pos = 12,
            qpid_st_pb_td50skywave_pos = 13,
            qpid_st_pb_mxt1xx_g100 = 14,
            qpid_st_pb_idp780skywave_pos = 15,
            qpid_st_pb_idp780skywave_setup = 16,
            qpid_st_pb_idp780skywave_setup_var = 17,
            qpid_st_pb_mtc550_black_box_minute = 18,
            qpid_st_pb_command = 19,
            qpid_st_pb_command_response = 20,
            qpid_st_library_response = 21,
            qpid_st_huge_library_response = 22,
            qpid_st_pb_module_connection = 23,
            qpid_st_pb_mxt1xx_ip_gprs_conn = 24,
            qpid_st_pb_mtc550skywave_pos_and_free_data = 25,
            qpid_st_pb_command_data_terminal_pwd_generator_response = 26,
            qpid_st_pb_mxt1xx_icc_id_answer = 27,
            qpid_st_pb_mtc550satamatics_pos = 28,
            qpid_st_pb_mtc550satamatics_pos_and_free_data = 29,
            qpid_st_pb_idp780_black_box_minute = 30,
            qpid_st_pb_mxt1xx_version_packet = 31,
            qpid_st_pb_idp780skywave_black_box_minute = 32,
            qpid_st_pb_mtc550_parameters = 33,
            qpid_st_pb_idp780skywave_reduced_pos = 34,
            qpid_st_pb_idp780skywave_reduced_embedded_actions = 35,
            qpid_st_pb_idp780skywave_reduced_macro = 36,
            qpid_st_pb_idp780skywave_reduced_deltas = 37,
            qpid_st_pb_idp780skywave_reduced_telemetry_events = 38,
            qpid_st_pb_idp780skywave_reduced_accelerometer_events = 39,
            qpid_st_pb_idp780skywave_reduced_trip = 40,
            qpid_st_pb_idp780skywave_reduced_text = 41,
            qpid_st_pb_idp780skywave_reduced_adc = 42,
            qpid_st_pb_idp780skywave_reduced_open_data = 43,
            qpid_st_pb_idp780skywave_reduced_telemetry = 44,
            qpid_st_pb_mtc550skywave_reduced_pos = 45,
            qpid_st_pb_idp780_extended_log = 46,
            qpid_st_pb_idp780_iccid = 47,
            qpid_st_pb_idp780skywave_iccid = 48,
            qpid_st_pb_idp780_files_list = 49,
            qpid_st_pb_idp780skywave_files_list = 50,
            qpid_st_pb_mxt1xx_transparent_response = 51,
            qpid_st_pb_mtc550_presentation = 52,
            qpid_st_pb_idp780_presentation = 53,
            qpid_st_pb_idp780_hw_info = 54,
            qpid_st_pb_mxt1xx_keep_alive = 55,
            qpid_st_pb_idp780skywave_hw_info = 56,
            qpid_st_pb_idp780_black_box_info = 57,
            qpid_st_pb_mxt1xx_request_old_pos_sts = 58,
            qpid_st_pb_mtc550_black_box_info = 59,
            qpid_st_pb_mxt1xx_device_information = 60,
            qpid_st_pb_mxt1xx_dynamic_setup = 61
        };

        public qpid_subject_type get_subject_type(String subject)
        {
            qpid_subject_type type = qpid_subject_type.qpid_st_none;
            for (int i = 0; i < MAX_SUBJECT_TYPES; i++)
            {
                if (qpid_subject[i] == subject)
                {
                    type = (qpid_subject_type)i;
                    break;
                }
            }
            return (type);
        }
    }
}
