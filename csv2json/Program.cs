using System;
using System.IO;
using System.Text;
using System.Collections.Generic;


namespace csv2json
{
    class Program
    {
        static void Main(string[] args)
        {

            //подключаем украинский язык в консоли
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            //Console.InputEncoding = System.Text.Encoding.Unicode;

            /*

            string path = "products.csv";
            List<string> fileLine = new List<string>();

            fileLine = readCsvFile(path, "Windows-1251");




            

            List<string> str = new List<string>();

            foreach (string line in fileLine)
            {
                str = parseCsvString(line, ';');

                foreach (string s in str) Console.WriteLine(s);

            }
          */


            string text = "111;\"113\"\"\"\";23123\"\"\";23end\";\"123\";123";

            List<string> str = new List<string>();

            string[] s = new string[2];
            s[1] = text;
            while (s[1]!= "")
            {
               s = fieldParse(text);
               str.Add(s[0]);
                text = s[1];
                Console.WriteLine(s[0]);
            }

         

            foreach (string st in str) Console.WriteLine(st);
        }

        static string[] fieldParse(string text)
        {
            string[] outString = new string[2];

            int i = 0;
            if (text.IndexOf(";")==-1)
            {
                outString[0] = text;
                outString[1] = "";
                return outString;
            }

            if (text[i] != ';' && text[i] != '"' && i == 0)
            {
            //    Console.WriteLine(text.Substring(0, text.IndexOf(";", i)));
                outString[0]=text.Substring(0, text.IndexOf(";", i));
                outString[1] = text.Substring(text.IndexOf(";", i) + 1);
                return outString;
            }

            if (text[i] == ';' && i == 0)
            {
            //    Console.WriteLine("empty field");
                outString[0]=" ";
                outString[1] = text.Substring(1);
                return outString;
            }

            if (text[i] == '"' && i == 0)
            {
                while (i < text.Length)
                {
                    if (text.IndexOf("\";", i) != -1 && text[(text.IndexOf("\";", i) - 1)] != '"')
                    {
              //          Console.WriteLine(text.Substring(1, text.IndexOf("\";", i) - 1));
                        outString[0]=text.Substring(1, text.IndexOf("\";", i) - 1);
                        outString[1] = text.Substring(text.IndexOf("\";", i) + 2);
                        return outString;
                        
                    }
                    i++;
                }
            }

            return outString;
        }


        static List<string> parseCsvString(string csvString, char separator)
        {
            List<string> str = new List<string>();

            bool itemOpen = false;
            int indexOpen = 0;
            int indexClosed = 0;
            int openQuotes = 0;
            int indexQuotes = 0;
            string quotes = "\"";

            csvString = csvString.Insert(csvString.Length, separator.ToString());

            for (int i = 0; i < csvString.Length; i++)
            {

                if (csvString[i] == '"' && itemOpen == false)
                {
                     openQuotes = i;
                    if (csvString.IndexOf((quotes + quotes + separator), i) == -1)
                    {
                        
                        indexQuotes = csvString.IndexOf((quotes + separator), i);
                        str.Add(csvString.Substring(i + 1, indexQuotes - i - 1));
                        i = indexQuotes;
                    } else
                    {
                        Console.WriteLine((quotes + quotes + separator));

                        i = csvString.IndexOf((quotes + quotes + separator), i)+3;
                        indexQuotes = csvString.IndexOf((quotes + separator), i);
                        str.Add(csvString.Substring(openQuotes + 1, indexQuotes - i - 1));
                        i = indexQuotes;
                    }

                }

                if (csvString[i] != '"' && itemOpen == false)
                {
                    itemOpen = true;
                    indexOpen = i;
                }
                
                if (csvString[i] == separator && itemOpen == true)
                {
                    indexClosed = i;
                    str.Add(csvString.Substring(indexOpen, indexClosed - indexOpen));
                    itemOpen = false;
                    indexOpen = i;
                }
/*
                if (csvString.Length - 1 == i && itemOpen == true)
                {
                    indexClosed = csvString.Length;
                    str.Add(csvString.Substring(indexOpen, indexClosed - indexOpen));
                }
*/
            }

            return str;

        }

        static List<string> readCsvFile(string pathFile, string encodingFile)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Encoding enc = Encoding.GetEncoding(encodingFile);

           // Encoding utf8 = Encoding.GetEncoding("UTF-8");
           // Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            List<string> fileLine = new List<string>();

            using (StreamReader reader = new StreamReader(pathFile, enc))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    fileLine.Add(line);
                    Console.WriteLine(line);
                }
            }
            return fileLine;
        }




    }
}
