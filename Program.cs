using System;

namespace DisneyBot
{
    class Program
    {
        static Bot _bot;

        static void Main()
        {
            var loader = new TriviaLoader();
            Console.WriteLine("Loading trivia, please wait..");
            var questions = loader.LoadQuestions(@"C:\data\source\github\Telegram.Disney\\JEOPARDY_QUESTIONS1.json").Result;
            Console.WriteLine($"Loaded {questions.Count} trivia questions...");
            _bot = new Bot(questions:questions);
            _bot.Start();            

            Console.WriteLine("hit any key to exit");
            Console.ReadLine();
        }
    }
}