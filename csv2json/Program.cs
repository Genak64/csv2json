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
            if (args.Length == 0)
            {
                Console.WriteLine(" отсутствуют аргументы, варианты:");
                Console.WriteLine(" /имя файла/");
                Console.WriteLine(" /имя файла/ /кодировка/");
                Console.WriteLine(" /имя файла csv/ /кодировка/ /разделитель/");
                Console.WriteLine(" например: csv2json file.csv Windows-1251 ;");
                return;
            }

            if (args.Length==1)
            {


            }

            //Кодировка файла
            string fileEncoding = "Windows-1251";
            string path = "products.csv";
            char separator = ';';

            table tableItems = new table();

            tableItems =ParseCSV.loadCsvFile(path, fileEncoding, separator);
                       
        }

            

    }

      
        class table
        {
          public  List<List<string>> item = new List<List<string>>();

        }

       



   
}
