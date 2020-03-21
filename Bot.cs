using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace DisneyBot
{
    public class Bot
    {
        ITelegramBotClient botClient;
        Telegram.Bot.Types.User _botUser;
        private readonly string _apiKey;
        List<JeopardyQuestion> _questions;

        Game _game = null;
        public Bot(List<JeopardyQuestion> questions, Game game = null, string apiKey = null)
        {
            _apiKey = apiKey;
            if (string.IsNullOrEmpty(_apiKey)) _apiKey = "926018386:AAFrqJJTzdGHr1RVQFsJWt94Rr3r5fWzxVY";
            botClient = new TelegramBotClient(_apiKey);
            _questions = questions;
            _game = game;
            if (_game == null) _game = new Game(questions);

        }


        public async Task Start()
        {
            _botUser = await botClient.GetMeAsync();
            Console.WriteLine(
              value: $"Hello, World! I am user {_botUser.Id} and my name is {_botUser.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
        }

        async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var response = _game.OnCommand(e);
            if (!string.IsNullOrEmpty(response))
            {
               await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: response
                );
            }

        }
    }
}