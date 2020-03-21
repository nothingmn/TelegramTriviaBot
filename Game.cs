using System;
using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace DisneyBot
{


    //represents the game play
    public class Game
    {
        System.Random rnd = new Random();
        Dictionary<long, JeopardyQuestion> currentQuestions = new Dictionary<long, JeopardyQuestion>();
        private List<JeopardyQuestion> _questions;
        public Game(List<JeopardyQuestion> questions)
        {
            this._questions = questions;

        }

        public string OnCommand(MessageEventArgs e)
        {

            if (e.Message.Text == null) return null;
            var message = e.Message.Text;

            if (message.StartsWith("/"))
            {
                //command
                var cmd = message.ToLowerInvariant().Substring(1);
                if (cmd.Equals("question", StringComparison.InvariantCultureIgnoreCase))
                {
                    var question = NextQuestion(e.Message.Chat.Id);
                    if (currentQuestions.ContainsKey(e.Message.Chat.Id))
                    {
                        currentQuestions[e.Message.Chat.Id] = question;
                    }
                    else
                    {
                        currentQuestions.Add(e.Message.Chat.Id, question);
                    }
                    return $"Here is your question...\n\nCategory:{question.Category}\nAnswer:{question.Question}\n\n({question.Answer})";

                }
            }
            else
            {
                //others, an attempted answer.
                var activeQuestion = GetActiveQuestion(e.Message.Chat.Id);
                if (activeQuestion == null)
                {
                    return "Try using /question to ask for a new question";
                }
                else
                {
                    var fixedMessage = CleanUpMessage(message);

                    var diff = Fastenshtein.Levenshtein.Distance(fixedMessage, activeQuestion.Answer.ToLowerInvariant());
                    if (diff <= 2)
                    {
                        var next = NextQuestion(e.Message.Chat.Id);
                        return $"{e.Message.From.FirstName} Wins with {activeQuestion.Answer} ({diff})!!!\n\nHere is your next question..\n\nCategory:{next.Category}\nAnswer:{next.Question}\n\n({next.Answer})";
                    }
                    else
                    {
                        return $"Wrong, try again! {diff}";
                    }
                }

            }

            return message;
        }

        private JeopardyQuestion NextQuestion(long chatId)
        {
            var question = RandomQuestion();
            if (currentQuestions.ContainsKey(chatId))
            {
                currentQuestions[chatId] = question;
            }
            else
            {
                currentQuestions.Add(chatId, question);
            }
            return question;
        }

        private JeopardyQuestion RandomQuestion()
        {
            return _questions[rnd.Next(_questions.Count)];
        }

        private JeopardyQuestion GetActiveQuestion(long chatId)
        {
            if (currentQuestions.ContainsKey(chatId))
            {
                return currentQuestions[chatId];
            }
            return null;
        }

        private string CleanUpMessage(string message)
        {
            var fixedMessage = message.ToLowerInvariant();

            if (fixedMessage.StartsWith("who is ")) fixedMessage = fixedMessage.Substring("who is ".Length);
            if (fixedMessage.StartsWith("what is ")) fixedMessage = fixedMessage.Substring("what is ".Length);
            if (fixedMessage.StartsWith("where is ")) fixedMessage = fixedMessage.Substring("where is ".Length);
            if (fixedMessage.StartsWith("when is ")) fixedMessage = fixedMessage.Substring("when is ".Length);
            if (fixedMessage.StartsWith("why is ")) fixedMessage = fixedMessage.Substring("why is ".Length);
            if (fixedMessage.StartsWith("how is ")) fixedMessage = fixedMessage.Substring("how is ".Length);

            if (fixedMessage.StartsWith("who is a ")) fixedMessage = fixedMessage.Substring("who is a ".Length);
            if (fixedMessage.StartsWith("what is a ")) fixedMessage = fixedMessage.Substring("what is a ".Length);
            if (fixedMessage.StartsWith("where is a ")) fixedMessage = fixedMessage.Substring("where is a ".Length);
            if (fixedMessage.StartsWith("when is a ")) fixedMessage = fixedMessage.Substring("when is a ".Length);
            if (fixedMessage.StartsWith("why is a ")) fixedMessage = fixedMessage.Substring("why is a ".Length);
            if (fixedMessage.StartsWith("how is a ")) fixedMessage = fixedMessage.Substring("how is a ".Length);

            if (fixedMessage.StartsWith("who are ")) fixedMessage = fixedMessage.Substring("who are ".Length);
            if (fixedMessage.StartsWith("what are ")) fixedMessage = fixedMessage.Substring("what are ".Length);
            if (fixedMessage.StartsWith("where are ")) fixedMessage = fixedMessage.Substring("where are ".Length);
            if (fixedMessage.StartsWith("when are ")) fixedMessage = fixedMessage.Substring("when are ".Length);
            if (fixedMessage.StartsWith("why are ")) fixedMessage = fixedMessage.Substring("why are ".Length);
            if (fixedMessage.StartsWith("how are ")) fixedMessage = fixedMessage.Substring("how are ".Length);

            if (fixedMessage.StartsWith("who ")) fixedMessage = fixedMessage.Substring("who ".Length);
            if (fixedMessage.StartsWith("what ")) fixedMessage = fixedMessage.Substring("what ".Length);
            if (fixedMessage.StartsWith("where ")) fixedMessage = fixedMessage.Substring("where ".Length);
            if (fixedMessage.StartsWith("when ")) fixedMessage = fixedMessage.Substring("when ".Length);
            if (fixedMessage.StartsWith("why ")) fixedMessage = fixedMessage.Substring("why ".Length);
            if (fixedMessage.StartsWith("how ")) fixedMessage = fixedMessage.Substring("how ".Length);
            if (fixedMessage.StartsWith("?")) fixedMessage = fixedMessage.Substring("?".Length);
            return fixedMessage;
        }

    }
}