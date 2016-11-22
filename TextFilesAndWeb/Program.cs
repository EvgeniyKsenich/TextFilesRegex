using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFilesAndWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex t1 = new Regex(@"(?:(((http|https):\/\/)([^\W][a-zA-Z\d\.-]+\.)([a-z]+))\-)");
            MatchCollection tmp = t1.Matches(" ");
            Console.WriteLine("Enter start url: ");
            string url = Console.ReadLine();
            string path_URL = @"F:\html_pr\3\urls.txt";
            bool tr = true;
            while (tr)
            {
                string DATA = newData(url);
                MatchCollection newAllUrl = getAllUrl(DATA);
                write(path_URL, newAllUrl);
               // while (true) {
                    url = NewRNDurl(newAllUrl);
                   // if (!cheakOne(path_URL,url)) ;
                   // else break;
                  //  if (!cheackAll(path_URL, newAllUrl)) ;
                  //  else break;
               // }
                tmp = t1.Matches(" ");
                //if (!cheackAll(path_URL, newAllUrl)) ;
                //else break;
            }
        }

        static bool cheackAll(string path, MatchCollection url)
        {
            string all = "";
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(1251)))
            {
                all = sr.ReadToEnd();
            }
            int lic=0;
            if (url.Count == 0) return false;
            for (int i=0;i<url.Count;i++)
            {
                if (!File.Exists(path)) return true;
                else
                {
                    if (all.Contains(url[i].Groups[0].Value))
                    {
                        int a = all.IndexOf(url[i].Groups[0].Value, StringComparison.CurrentCulture);
                        a += url[i].Groups[0].Length;
                        a++;
                        if (all[a] == '+') lic++;
                    }
                }
            }
            if (lic == url.Count) return false;
            else
            return false;
        }

        static bool cheakOne(string path, string url)
        {
            if (!File.Exists(path)) return true;
            else
            {
                string all = "";
                using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(1251)))
                {
                    all = sr.ReadToEnd();
                }
                if (all.Contains(url))
                {
                    int a = all.IndexOf(url, StringComparison.CurrentCulture);
                    a += url.Length;
                    a++;
                    if (all[a] == '+') return false;
                    else return true;
                }
                else return true;
            }
        }

        static string NewRNDurl(MatchCollection tmp)
        {
            int cout = tmp.Count;
            Random rnd = new Random();
            int r = rnd.Next(1, cout);
            return tmp[r].Groups[0].Value;
        }

        static void appearnd(string path, string f)
        {

        }

        static MatchCollection getAllUrl(string DATA)
        {
            Regex t1 = new Regex(@"\b(((http|https):\/\/)([^\W][a-zA-Z\d\.-]+\.)([a-z]+))\b");
            MatchCollection tmp = t1.Matches(DATA);
            return tmp;
        }

        static bool isUniq(string path, string objecT)
        {
            if (!File.Exists(path)) return true;
            else
            {
                string all = "";
                    using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(1251)))
                    {
                        all = sr.ReadToEnd();
                    }
                if (all.Contains(objecT)) return false;
                else return true;
            }
        }

        static void write(string path, MatchCollection file)
        {
            int coute = file.Count;
            bool cheak = false;
            for (int i=0;i< coute; i++)
            {
                cheak = isUniq(path, file[i].Groups[0].Value);
                if (File.Exists(path))
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        if(cheak) sw.Write(file[i].Groups[0].Value+"-" + Environment.NewLine);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        if (cheak) sw.Write(file[i].Groups[0].Value + "-" + Environment.NewLine);
                        sw.Close();
                    }
                }
            }
        }

        static string newData(string url)
        {
            WebRequest request = WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            return reader.ReadToEnd();
        }
    }
}
