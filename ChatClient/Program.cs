using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

class Program
{
    private static string apiUrl = "http://localhost:5107";
    private static HubConnection? connection;

    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Cliente de Chat ===");

        // 1) Login
        var token = await LoginAsync();
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Não foi possível autenticar.");
            return;
        }

        // 2) Conectar ao hub
        await ConectarAsync(token);

        // 3) Iniciar menu
        await MenuAsync();

        // 4) Encerrar conexão
        await connection!.StopAsync();
        Console.WriteLine("Conexão encerrada. Até logo!");
    }

    private static async Task<string?> LoginAsync()
    {
        Console.Write("E-mail: ");
        var email = Console.ReadLine();

        Console.Write("Senha: ");
        var senha = Console.ReadLine();

        var http = new HttpClient();
        var response = await http.PostAsJsonAsync($"{apiUrl}/api/auth/login", new { Email = email, Password = senha });

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Erro ao fazer login: " + await response.Content.ReadAsStringAsync());
            return null;
        }

        var loginResult = await response.Content.ReadFromJsonAsync<LoginResponse>();
        Console.WriteLine("Login realizado com sucesso!");
        return loginResult!.Token;
    }

    private static async Task ConectarAsync(string token)
    {
        connection = new HubConnectionBuilder()
            .WithUrl($"{apiUrl}/chat", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token)!;
            })
            .WithAutomaticReconnect()
            .Build();

        // Escutar mensagens recebidas
        connection.On<int, string>("ReceiveMessage", (userId, message) =>
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n📩 Mensagem recebida de [{userId}]: {message}");
            Console.ResetColor();
            Console.Write("Opção: "); // mantém prompt ativo
        });

        await connection.StartAsync();
        Console.WriteLine("✅ Conectado ao Hub!");
    }

    private static async Task MenuAsync()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n--- Menu ---");
            Console.WriteLine("1 - Enviar mensagem");
            Console.WriteLine("2 - Sair");
            Console.Write("Opção: ");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out opcao)) continue;

            switch (opcao)
            {
                case 1:
                    Console.Write("Id do usuário receptor: ");
                    if (!int.TryParse(Console.ReadLine(), out int receiverId))
                    {
                        Console.WriteLine("❌ Id inválido!");
                        break;
                    }

                    Console.Write("Mensagem: ");
                    var msg = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(msg)) break;

                    await connection!.InvokeAsync("SendMessage", receiverId, msg);
                    Console.WriteLine("✅ Mensagem enviada!");
                    break;

                case 2:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

        } while (opcao != 2);
    }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}
