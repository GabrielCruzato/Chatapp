ChatApp - Aplicação de Chat TCP em WPF (C#)
Descrição

ChatApp é um cliente e servidor de chat simples desenvolvido em C# com WPF no cliente e sockets TCP para comunicação em tempo real. O projeto utiliza o padrão MVVM para organização do código no cliente e implementa comunicação via pacotes customizados para troca de mensagens, conexão e desconexão de usuários.
Funcionalidades principais

  - Conexão simultânea de múltiplos clientes

  - Envio e recepção de mensagens em tempo real

  - Notificação de entrada e saída de usuários no chat

  - Interface cliente responsiva usando WPF com binding via MVVM

  - Comunicação via TCP usando sockets e protocolo simples de pacotes com códigos de operação (opcodes)

Tecnologias utilizadas

  - C# com .NET (WPF para o cliente, Console para o servidor)

  - Sockets TCP para comunicação de rede

   - Padrão MVVM no cliente para desacoplamento e testabilidade

   - Collections observáveis para atualizar UI em tempo real

Estrutura do projeto

   - ChatServer: Servidor TCP que gerencia conexões e broadcast de mensagens

   - ChatClient: Aplicação cliente WPF com ViewModels, Models e lógica de rede

   - Networking: Classes para construção e leitura de pacotes TCP customizados

Como usar

   - Execute o servidor (ChatServer) primeiro para começar a escutar conexões.

   - Inicie o cliente (ChatClient), informe seu nome de usuário e conecte ao servidor.

   - Envie mensagens para o grupo; mensagens são transmitidas para todos os usuários conectados.

Possíveis melhorias futuras

   - Autenticação de usuários

   - Mensagens privadas / chats individuais

   - Histórico de mensagens persistente

   - Interface mais completa e responsiva

   - Criptografia da comunicação

Contato

Gabriel Cruzato – gabrielcruzato@gmail.com
LinkedIn: https://www.linkedin.com/in/gabriel-cruzato/
