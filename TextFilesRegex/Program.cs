using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFilesRegex
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"F:\html_pr\test.html";
            string path2 = @"F:\html_pr\test.txt";
            Console.WriteLine("Count of p: "+CoP(path).ToString());
            Console.WriteLine("Count of img: "+ CoIMG(path).ToString());
            Console.WriteLine();
            Console.WriteLine(CoHTTP_S(path));
            Console.WriteLine(MAIL(path));

            string output = "Count of p: " + CoP(path).ToString()+ Environment.NewLine;
            output += "Count of img: " + CoIMG(path).ToString() + Environment.NewLine;
            output += CoHTTP_S(path).ToString() + Environment.NewLine;
            output += MAIL(path).ToString() + Environment.NewLine;

            Output(path2, output);
        }

        static void Output(string path, string file)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                    sw.Write(file);
            }
        }

        static int CoP(string path)
        {
            Regex temperature = new Regex(@"<p>((?:.|\n)*?)<\/p>");
            MatchCollection tmp= temperature.Matches(" ");
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string all = sr.ReadToEnd();
                    tmp = temperature.Matches(all);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return tmp.Count;
        }


        static int CoIMG(string path)
        {
            Regex temperature = new Regex(@"<img src=[""](.+)[""]\/>");
            MatchCollection tmp = temperature.Matches(" ");
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string all = sr.ReadToEnd();
                    tmp = temperature.Matches(all);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return tmp.Count;
        }

        static string CoHTTP_S(string path)
        {
            string oo="";
            Regex t1 = new Regex(@"\b(((http|https):\/\/)([^\W][a-zA-Z\d\.-]+\.)+([/A-Za-z%\d_\.?=&#]+[^\W-])(:\d+)?)\b");
            Regex t2 = new Regex(@"-\.");
            Match tm2;
            MatchCollection tmp = t1.Matches(" ");
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string all = sr.ReadToEnd();
                    tmp = t1.Matches(all);
                    oo="http or https: " + Environment.NewLine;
                    for (int i=0;i<tmp.Count;i++)
                    {
                        tm2 = t2.Match(tmp[i].Groups[0].Value);
                        if (!tm2.Success)
                        {
                            oo+=tmp[i].Groups[0].Value + Environment.NewLine;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return oo;
        }

        static string MAIL(string path)
        {
            string oo = "";
            Regex t1 = new Regex(@"((?:(?:[0-9a-zA-Z.\-_]){1,}[^.@\-_])@(?:(?:[\d]|[a-zA-Z]){1,})(?:\.(?:[\d]|[a-zA-Z]){1,})+)");
            MatchCollection tmp = t1.Matches(" ");
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string all = sr.ReadToEnd();
                    tmp = t1.Matches(all);
                    oo="email: "+Environment.NewLine;
                    for (int i = 0; i < tmp.Count; i++)
                    {
                            oo+=tmp[i].Groups[0].Value+Environment.NewLine;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return oo;
        }


    }
}
