using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace csv2json
{
    static class ParseCSV
    {


        public static table loadCsvFile(string path, string fileEncoding, char separator)
        {
            //Кодировка файла
            //string fileEncoding = "Windows-1251";
            //string path = "products.csv";
            //char separator = ';';

            table listStr = new table();

            List<string> fileLine = new List<string>();

            fileLine = readCsvFile(path, fileEncoding);

            List<string> str = new List<string>();

            foreach (string line in fileLine)
            {
                str = pCsvString(line, separator);
                listStr.item.Add(new List<string>(str));
            }

            return listStr;
        }


       public static List<string> readCsvFile(string pathFile, string encodingFile)
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
                    Console.WriteLine(line);  //***************************
                }
            }
            return fileLine;
        }


        //возвращает csv-строку разбитую на отдельные элементы разделителем separator
        static List<string> pCsvString(string csvString, char separator)
        {
            List<string> item = new List<string>();

            string[] s = new string[2];
            s[1] = csvString;

            while (s[1] != "")
            {
                s = fieldParse(csvString, separator);
                s[0] = s[0].Replace("\"\"", "\"");
                item.Add(s[0]);
                csvString = s[1];
            }
            return item;

        }

        //вырезает и возвращает отдельный элемент csv-строки в outString[0]
        //и остальную часть строки в outString[1]
        static string[] fieldParse(string text, char separator)
        {
            string[] outString = new string[2];
            int i = 0;
            if (text == null)
            {
                outString[1] = "";
                return outString;
            }
            if (text.IndexOf(separator) == -1 && text.IndexOf("\"") == -1)
            {
                outString[0] = text;
                outString[1] = "";
                return outString;
            }
            if (text.IndexOf("\"") == 0 && text.IndexOf("\"", 1) == text.Length - 1)
            {
                outString[0] = text.Substring(1, text.Length - 2);
                outString[1] = "";
                return outString;
            }
            if (text[i] != separator && text[i] != '"' && i == 0)
            {
                outString[0] = text.Substring(0, text.IndexOf(separator.ToString(), i));
                outString[1] = text.Substring(text.IndexOf(separator.ToString(), i) + 1);
                return outString;
            }
            if (text[i] == separator && i == 0)
            {
                outString[0] = " ";
                outString[1] = text.Substring(1);
                return outString;
            }
            if (text[i] == '"' && i == 0)
            {
                while (i < text.Length)
                {
                    if (text.IndexOf(("\"\"\"\"" + separator.ToString()), i) != -1)
                    {
                        i = text.IndexOf(("\"\"\"\"" + separator.ToString()), i) + 3;
                        continue;
                    }
                    if (text.IndexOf(("\"\"\"" + separator.ToString()), i) != -1)
                    {
                        outString[0] = text.Substring(1, text.IndexOf(("\"" + separator.ToString()), i) - 1);
                        outString[1] = text.Substring(text.IndexOf(("\"" + separator.ToString()), i) + 2);
                        return outString;
                    }
                    if (text.IndexOf("\";", i) != -1 && text[(text.IndexOf(("\"" + separator.ToString()), i) - 1)] != '"')
                    {
                        outString[0] = text.Substring(1, text.IndexOf("\";", i) - 1);
                        outString[1] = text.Substring(text.IndexOf(("\"" + separator.ToString()), i) + 2);
                        return outString;
                    }
                    i++;
                }
            }
            return outString;
        }



    }
}
