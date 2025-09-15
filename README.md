# Chat Realtime Backend  
API ASP.NET Core que implementa um sistema de chat em tempo real utilizando **SignalR** e **autenticação JWT**.  

## 📌 Descrição  
Esta aplicação permite que usuários se autentiquem, entrem em salas de chat e troquem mensagens em tempo real.  
O backend gerencia autenticação, persistência de dados e comunicação via WebSockets utilizando SignalR.  

## 🚀 Funcionalidades  
- Registro e autenticação de usuários com JWT.  
- Conexão em tempo real via **SignalR**.  
- Envio e recebimento de mensagens em salas de chat.  
- Estrutura organizada em camadas (Controllers, Services, DTOs, Models, Data, Hubs).  
- Persistência em banco de dados relacional via **Entity Framework Core**.  

## 📡 Endpoints Principais  
### Autenticação  
- **POST** `/api/auth/register` → Registro de novo usuário.  
- **POST** `/api/auth/login` → Login e geração de token JWT.  

### Chat (SignalR Hub)  
- **Hub** `/chat` → Conexão WebSocket para troca de mensagens.  

Exemplo de chamada ao **Hub** (em cliente JavaScript):  
```js
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5107/chatHub", { accessTokenFactory: () => token })
    .build();

await connection.start();
await connection.invoke("SendMessage", "sala1", "Olá, mundo!");
```

## 📂 Estrutura do Projeto  
```
chat-realtime-backend/
 ┣ 📂 ChatClient/           → Cliente para testes da API e do Hub  
 ┣ 📂 Controllers/          → Controllers de autenticação e APIs REST  
 ┣ 📂 Data/                 → Configuração do DbContext  
 ┣ 📂 DTOs/                 → Objetos de transferência de dados  
 ┣ 📂 Hubs/                 → Hubs do SignalR (ex: ChatHub.cs)  
 ┣ 📂 Migrations/           → Migrações do Entity Framework  
 ┣ 📂 Models/               → Entidades do domínio  
 ┣ 📂 Services/             → Regras de negócio e lógica de autenticação/chat  
 ┣ .env                     → Variáveis de ambiente (chaves secretas, DB, etc.)  
 ┣ appsettings.json         → Configuração da aplicação  
 ┣ Dockerfile               → Definição da imagem Docker  
 ┣ Program.cs               → Configuração principal da aplicação  
 ┣ README.md                → Documentação do projeto  
```

## 🛠️ Tecnologias  
- **C# 12 / .NET 8**  
- **ASP.NET Core**  
- **SignalR**  
- **Entity Framework Core**  
- **JWT Authentication**  
- **SQL Server ou PostgreSQL** (configurável)  
- **Docker**  

## 🐳 Execução com Docker  
1. Criar a imagem:  
```bash
docker build -t chat-realtime-backend .
```

2. Rodar o contêiner:  
```bash
docker run -d -p 5107:5107 --name chat-api chat-realtime-backend
```

3. Testar a API (exemplo usando `curl`):  
```bash
curl -X POST http://localhost:5107/api/auth/login   -H "Content-Type: application/json"   -d '{ "email": "teste@teste.com", "senha": "123456" }'
```

## 📌 Observações  
- A aplicação está configurada para rodar na porta **5107**.  
- O arquivo **.env** deve conter a chave JWT e string de conexão com o banco.  
- As migrações do **Entity Framework** devem ser aplicadas antes de rodar o projeto.  

## 👤 Autor  
**Rodrigo Sousa Sales**
