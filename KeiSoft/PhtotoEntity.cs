using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeiSoft
{
    public class PhtotoEntity
    {
        public static string BasePath { get; set; } = System.Environment.CurrentDirectory;
        public static Dictionary<Bitmap,string> GetBitmaps(int seed)
        {
            Dictionary<Bitmap, string> result = new Dictionary<Bitmap, string>();
            switch (seed)
            {
                case 1:
                    GroupOne(result);
                    break;
                case 2:
                    GroupTwo(result);
                    break;
                default:
                    throw new Exception($"not define seed:{seed} ");
             
            }
            return result;
        }

        private static void GroupOne(Dictionary<Bitmap, string> resources)
        {
          
            string file1 = "T1.JPG";
            resources.Add(GetBitmap(file1), file1);
            string file2 = "T2.JPG";
            resources.Add(GetBitmap(file2), file2);
        }

        private static void GroupTwo(Dictionary<Bitmap, string> resources)
        {
            string file1 = "Q1.JPG";
            resources.Add(GetBitmap(file1), file1);
            string file2 = "Q2.JPG";
            resources.Add(GetBitmap(file2), file2);
        }

        private static Bitmap GetBitmap(string fileName)
        {
            try
            {
            return new Bitmap(Path.Combine(BasePath + "/Photo", fileName));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    
}
