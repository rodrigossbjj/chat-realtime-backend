# 💬 Chat Realtime Backend  

Backend de um sistema de **chat em tempo real**, desenvolvido em **.NET 8 + SignalR + JWT + EF Core**.  
A aplicação permite a comunicação instantânea entre usuários com autenticação via **JWT**, persistência de mensagens e suporte a **WebSockets** através do **SignalR**.  

---

## ✨ Funcionalidades  

- Registro e autenticação de usuários com **JWT**  
- Persistência de mensagens no banco de dados (SQLite ou PostgreSQL)  
- Envio e recebimento de mensagens em tempo real via **SignalR**  
- Histórico de conversas disponível por usuário  
- Segurança: apenas usuários autenticados podem interagir com o chat  
- Configuração simples via `.env` e `appsettings.json`  
- Suporte a **Docker** para facilitar o deploy  

---

## 📂 Estrutura do Projeto  

```
chat-realtime-backend/
│-- ChatClient/        -> Cliente simples para teste do chat
│-- Controllers/       -> Endpoints da API (Auth, Messages)
│-- Data/              -> AppDbContext.cs (EF Core)
│-- DTOs/              -> Objetos de transferência (UserDto, MessageDto)
│-- Hubs/              -> ChatHub.cs (SignalR)
│-- Migrations/        -> Histórico de migrações do banco
│-- Models/            -> Entidades (User.cs, Message.cs)
│-- Services/          -> Lógica de negócio (AuthService, ChatService)
│-- .env               -> Variáveis de ambiente (ex: JWT_SECRET)
│-- appsettings.json   -> Configurações da aplicação
│-- Dockerfile         -> Containerização do projeto
│-- Program.cs         -> Configuração da aplicação
│-- README.md          -> Documentação
```

---

## 🔑 Endpoints  

### Autenticação  
- `POST /auth/register` → Registrar novo usuário  
- `POST /auth/login` → Autenticar usuário e receber token JWT  

### Mensagens  
- `GET /messages/{userId}` → Buscar histórico de mensagens  
- `POST /messages` → Enviar mensagem (salva no banco + notifica via SignalR)  

### SignalR Hub  
- Endpoint: `/chat`  
- Método: `SendMessage(string user, string message)`  

---

## 🛠️ Tecnologias Utilizadas  

- **.NET 8 / ASP.NET Core**  
- **Entity Framework Core** (SQLite ou PostgreSQL)  
- **SignalR**  
- **JWT (JSON Web Token)**  
- **Docker**  
- **C#**  

---

## ▶️ Como Executar  

### 🔹 Localmente  

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/chat-realtime-backend.git
   cd chat-realtime-backend
   ```

2. Configure o banco de dados no `appsettings.json` ou via `.env`.  

3. Rode as migrations:
   ```bash
   dotnet ef database update
   ```

4. Execute a aplicação:
   ```bash
   dotnet run
   ```

5. Acesse:
   - API: `http://localhost:5107`  
   - Hub SignalR: `http://localhost:5107/chat`  

---

### 🔹 Com Docker  

1. Build da imagem:
   ```bash
   docker build -t chat-realtime-backend .
   ```

2. Rodar o container:
   ```bash
   docker run -p 5107:5107 chat-realtime-backend
   ```

---

## 📄 Licença  

Este projeto é open-source e está sob a licença MIT.  

---

👨‍💻 Desenvolvido por Rodrigo Sousa
