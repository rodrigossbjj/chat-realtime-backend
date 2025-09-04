using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var apiUrl = "http://localhost:5107"; // sua API rodando
        var email = "teste2@teste.com";       // usuário já registrado
        var senha = "12345";                 // senha cadastrada

        // 1) Fazer login e obter token JWT
        var http = new HttpClient();
        var response = await http.PostAsJsonAsync($"{apiUrl}/api/auth/login", new { Email = email, Password = senha });

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Erro ao fazer login: " + await response.Content.ReadAsStringAsync());
            return;
        }

        var loginResult = await response.Content.ReadFromJsonAsync<LoginResponse>();
        var token = loginResult!.Token;

        Console.WriteLine($"Token recebido: {token.Substring(0, 20)}...");

        // 2) Conectar ao Hub passando o token
        var connection = new HubConnectionBuilder()
            .WithUrl($"{apiUrl}/chat", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token)!;
            })
            .WithAutomaticReconnect()
            .Build();

        // 3) Escutar mensagens recebidas
        connection.On<int, string>("ReceiveMessage", (userId, message) =>
        {
            Console.WriteLine($"[Mensagem de {userId}]: {message}");
        });

        await connection.StartAsync();
        Console.WriteLine("Conectado ao Hub!");

        int opcao;
        do
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1 - Enviar mensagem");
            Console.WriteLine("2 - Sair");
            Console.Write("Opção: ");
            var input = Console.ReadLine();
            if (!int.TryParse(input, out opcao)) continue;

            switch (opcao)
            {
                case 1:
                    Console.Write("Digite o Id do usuário receptor: ");
                    if (!int.TryParse(Console.ReadLine(), out int receiverId))
                    {
                        Console.WriteLine("Id inválido!");
                        break;
                    }

                    Console.Write("Digite a mensagem: ");
                    var msg = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(msg)) break;

                    // Chamar hub enviando para usuário específico
                    await connection.InvokeAsync("SendMessage", receiverId, msg);
                    break;

                case 2:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

        } while (opcao != 2);

        await connection.StopAsync();
    }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}
