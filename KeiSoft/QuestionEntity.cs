using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeiSoft
{
    public class QuestionEntity
    {
        public static List<string> GetQuestion(int seed)
        {
            List<string> result = new List<string>();
            LoadFucntion(seed, result);
            return result;
        }

        private static void LoadFucntion(int seed,List<string> resources)
        {
           var  questionbase = ConfigurationManager.AppSettings[$"Q{seed}"];
           var sp = questionbase.Split(new string[] { "|" }, StringSplitOptions.None);
            foreach (var item in sp)
            {
                resources.Add(item);
            }
        }
    }
}
