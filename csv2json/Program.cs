using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.VisualBasic.FileIO;

namespace csv2json
{
    class Program
    {
        static void Main(string[] args)
        {

            //подключаем украинский язык в консоли
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            //Console.InputEncoding = System.Text.Encoding.Unicode;
            settings setPr = new settings();
            string path = "Settings.json";
            FileInfo fSettings = new FileInfo(path);
            if (fSettings.Exists)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string tmp = sr.ReadToEnd();
                    setPr = JsonConvert.DeserializeObject<settings>(tmp);
                }

            } else
            {
                switch (args.Length)
                {
                    case 0:
                        Console.WriteLine(" отсутствуют аргументы, варианты:");
                        Console.WriteLine(" /имя входного файла csv/");
                        Console.WriteLine(" /имя входного файла csv/ /имя выходного файла json/");
                        Console.WriteLine(" /имя входного файла csv/ /имя выходного файла json/ /кодировка/");
                        Console.WriteLine(" /имя входного файла csv/ /имя выходного файла json/ /кодировка/ /разделитель/");
                        Console.WriteLine(" пример: csv2json file.csv file.json Windows-1251 ;");
                        Console.WriteLine(" имя выходного файла по умолчанию: output.json");
                        Console.WriteLine(" кодировка по умолчанию: Windows-1251");
                        Console.WriteLine(" разделитель по умолчанию: ;");
                        return;
                    case 1:
                        setPr.inputFileName = args[0];
                        setPr.outputFileName = "output.json";
                        setPr.inputFileEncoding = "Windows-1251";
                        setPr.separator = ";";
                        break;
                    case 2:
                        setPr.inputFileName = args[0];
                        setPr.outputFileName = args[1];
                        setPr.inputFileEncoding = "Windows-1251";
                        setPr.separator = ";";
                        break;
                    case 3:
                        setPr.inputFileName = args[0];
                        setPr.outputFileName = args[1];
                        setPr.inputFileEncoding = args[2];
                        setPr.separator = ";";
                        break;
                    case 4:
                        setPr.inputFileName = args[0];
                        setPr.outputFileName = args[1];
                        setPr.inputFileEncoding = args[2];
                        setPr.separator = args[3];
                        break;
                    default:
                        return;
                }

            }

            table tableItems = new table();

            tableItems = ParseCSV.loadCsvFile(setPr.inputFileName, setPr.inputFileEncoding, setPr.separator[0]);

            string tableJson = JsonConvert.SerializeObject(tableItems);

            using (StreamWriter sw = new StreamWriter(setPr.outputFileName, false, Encoding.Default))
            {
                sw.Write(tableJson);
            }


            string filename = "products.csv";

            //    using (StreamReader sr2 = new StreamReader(path))

            /*
            using (TextFieldParser r = new TextFieldParser(filename))
            {
               r.TextFieldType =FieldType.Delimited;
                
               r.SetDelimiters(",");
                foreach (string s in r.ReadFields()) Console.WriteLine(s);
            }
            */
            List<string> fileLine = new List<string>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Encoding enc = Encoding.GetEncoding("Windows-1251");

            //fileLine = ParseCSV.readCsvFile("products.csv", "Windows-1251");

            using (TextFieldParser r = new TextFieldParser(filename, enc))
            {
                r.TextFieldType = FieldType.Delimited;

                r.SetDelimiters(";");
                //   foreach (string s in r.ReadFields()) Console.WriteLine(s);

                while (!r.EndOfData)
                {
                    foreach (string s in r.ReadFields()) Console.WriteLine(s);
                }
            }
            



        }






    }

    [Serializable]
    class table
    {
       public  List<List<string>> item = new List<List<string>>();
    }
    [Serializable]
    class settings
    {
       public string inputFileName { get; set; }
       public string outputFileName { get; set; }
       public string inputFileEncoding { get; set; }
       public string separator { get; set; }

    }



   
}
