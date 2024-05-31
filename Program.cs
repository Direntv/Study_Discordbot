using Discord;
using Discord.WebSocket;

namespace DiscordTest
{
    class Program
    {
        DiscordSocketClient client;
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            DiscordSocketConfig config = new DiscordSocketConfig();
            config.GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent;
            client = new DiscordSocketClient(config);
            client.MessageReceived += CommandsHandler;
            client.Log += Log;

            var token = "MTI0NjA3MDE1MDYwNDY1Njc5Mw.GqsFVx.PLRfLzFo0U_ku1nPQRFovySQ8w0MMpPxTdjcjM";

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            Console.ReadLine();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private Task CommandsHandler(SocketMessage msg)
        {
            if (!msg.Author.IsBot)
                switch (msg.Content)
                {
                    case "!привет":
                        {
                            msg.Channel.SendMessageAsync($"Привет, {msg.Timestamp}");
                            break;
                        }
                    case "!рандом":
                        {
                            Random rnd = new Random();
                            msg.Channel.SendMessageAsync($"Выпало число {rnd.Next(-1000, 1000)}");
                            break;
                        }
                }
            return Task.CompletedTask;
        }
    }
}
