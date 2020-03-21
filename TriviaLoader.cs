using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DisneyBot
{
    public class TriviaLoader {
        public async Task<List<JeopardyQuestion>> LoadQuestions(string path) {
            var final = new List<JeopardyQuestion>();
            if(System.IO.File.Exists(path)){
                var json = await System.IO.File.ReadAllTextAsync(path);
                if(!string.IsNullOrEmpty(json)) {
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JeopardyQuestion>>(json);
                    foreach(var d in data) {
                        d.Question = d.Question.Replace("<br />", "\n");
                        d.Answer = d.Answer.Replace("<br />", "\n");
                        if(!d.Answer.Contains("<a ")) {
                            final.Add(d);
                        }
                    }
                    return final;
                }
            }
            return null;
        }
    }
}