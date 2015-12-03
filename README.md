# QPID PROTOBUF DOTNET #
Projeto de exemplo .NET para conexão em uma fila no Qpid para obter dados e deserializar estes dados no formato protobuf.

- Visual Studio 2013 (msvc-12.0)
- AMQP 0-10 (Qpid 0.24) e Google Protobuf 2.5.0

## [Dependências] ##
Bibliotecas utilizadas

- org.apache.qpid.messaging.dll
- Google.ProtocolBuffers.dll
- Google.ProtocolBuffersLite.dll

A biblioteca org.apache.qpid.messaging.dll apenas utiliza as bibliotecas em c++ do qpid, então é necessário compilar primeiro o qpid.

# QPID #

- Baixar e extrair o arquivo http://archive.apache.org/dist/qpid/0.24/qpid-cpp-0.24.tar.gz
	>Instruções para compilar: qpid-cpp-0.24\qpidc-0.24\INSTALL-WINDOWS
- Baixe e instale o CMake: [https://cmake.org/download/](https://cmake.org/download/) (Usado: 3.4.0)
- Baixe e instale o python 2: [http://www.python.org](http://www.python.org) (Usado: 2.7.10)
- Baixe e instale o Ruby: [http://rubyinstaller.org/downloads/](http://rubyinstaller.org/downloads/) (Usado: 2.2.3)
- Baixar e extrair o Boost: [http://sourceforge.net/projects/boost/files/boost/1.59.0/boost_1_59_0.7z/download](http://sourceforge.net/projects/boost/files/boost/1.59.0/boost_1_59_0.7z/download) (Usado 1.59)

## [Compilando o Boost] ##
Com o prompt de comando na pasta raiz do boost execute:

    bootstrap.bat

E depois para (win32):

    b2 toolset=msvc-12.0 --build-type=complete --with-iostreams --with-chrono --with-date_time --with-filesystem --with-regex --with-serialization --with-signals --with-system --with-thread --with-program_options --with-test --libdir=.\lib32 install

ou para (Win64)

    b2 toolset=msvc-12.0 --build-type=complete --with-iostreams --with-chrono --with-date_time --with-filesystem --with-regex --with-serialization --with-signals --with-system --with-thread --with-program_options --with-test architecture=x86 address-model=64 --libdir=.\lib64 install

## [Gerando os projetos do Qpid] ##
- No primeiro campo, coloque a pasta raiz do qpid
- No segundo bampo, coloque onde será a saída do projeto para o VS
- Clique em Add Entry para adicionar a pasta do boost
- Clique em "Configure" e se tudo der certo clique em "Generate"
![](http://s24.postimg.org/vb47gg0v9/cmake_qpid.jpg)

## [Compilando as biliotecas do qpid] ##
- Resolvendo o conflito do boost Thread com a implementação do Qpid 0.24

	altere a linha 227 do arquivo qpid-cpp-0.24\qpidc-0.24\src\qpid\sys\windows\Thread.cpp

	altere a linha 76 do arquivo qpid-cpp-0.24\qpidc-0.24\src\qpid\client\SessionBase_0_10.cpp
	de:
	>return impl;
	
	para:
	>return !!impl;

- Abra a solução

	C:\Users\Fred\Desktop\Desenvolvimento\exemplos qpid protobuf\qpid-cpp-0.24\qpidc-0.24\bin64\qpid-cpp.sln
- Compile o projeto qpidmessaging

	As bibliotecas serão geradas em: qpid-cpp-0.24\qpidc-0.24\bin64\src\Debug

## [Compilando a biblioteca org.apache.qpid.messaging.dll] ##
- Vá até o diretório qpid-cpp-0.24\qpidc-0.24\bindings\qpid\dotnet
- Duplique a pasta msvc10 e renomeie a cópia para msvc12
- Entre na pasta msvc12 e abra a solução org.apache.qpid.messaging.sln

	Altere o Target para o .net 4.5 e autorize a migração das dll's do c++ de VC100 para VC120

- Compile o projeto org.apache.qpid.messaging (c++) 

	Exclua a referencia ao arquivo org.apache.qpid.messaging.rc se der erro

- Copie as bibliotecas boost compiladas anteriormente para a pasta de libs do projeto
	Libs: boost_date_time
		  boost_program_options
		  boost_thread
		  boost_system
		  boost_chrono

## [Compilando a biblioteca Google.ProtocolBuffers.dll] ##
- Baixe o projeto em https://github.com/jskeet/protobuf-csharp-port.git
- Abra o projeto e compile na seguinte orgem: ProtocolBuffers; Protogen; ProtocolBuffers.Serialization; ProtocolBuffersLite; ProtocolBuffersLite.Serialization
- Copie os arquivos Google.ProtocolBuffers.dll e ProtoGen.exe para a pasta proto do projeto
- Baixe o protoc.exe e coloque na mesma pasta que o Protogen 
	[https://github.com/google/protobuf/releases/download/v2.5.0/protoc-2.5.0-win32.zip](https://github.com/google/protobuf/releases/download/v2.5.0/protoc-2.5.0-win32.zip)
- Adicione a referencia Google.ProtocolBuffers.dll
- Use o comando "ProtoGen.exe ProtoFiles\mxt1xx.proto -output_directory=Dotnet" no prompt de comando para gerar a classe mxt1xx e adicionbe esta classe ao seu projeto.


## [Pasta Lib] ##
As Bibliotecas já compiladas estão na pasta lib, se não conseguir executar o programa, copie as bibliotecas para a pasta de compilação ex: qpid_protobuf_dotnet\qpidProto\bin\x64\Debug
Teste feito em Debug x64
