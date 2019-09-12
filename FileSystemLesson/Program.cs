using System;
using System.IO;

namespace FileSystemLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = string.Empty;

            Console.WriteLine("Выбрать диск по номеру:");
            var drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                if(drives[i].IsReady && drives[i].DriveType == DriveType.Fixed)
                {
                    var counter = i + 1;
                    Console.WriteLine($"{counter}. {drives[i].Name} - {drives[i].AvailableFreeSpace} байт.");
                }             
            }

            var driveNumberAsString = Console.ReadLine();
            if(int.TryParse(driveNumberAsString, out var driveUserPosition))
            {
                var driveIndex = driveUserPosition - 1;
                path = drives[driveIndex].Name;

                Console.WriteLine("\n\nВсе директории:");

                foreach(var directoryName in Directory.GetDirectories(path))
                {
                    Console.WriteLine(directoryName);
                }

                Console.WriteLine("Введите имя новой папки:");
                var userDirectoryName = Console.ReadLine();

                path += userDirectoryName;

                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                //Directory.CreateDirectory(path);

                Console.WriteLine("Введите имя нового файла:");
                var userFileName = Console.ReadLine();

                path += $@"\{userFileName}";

                //if(!File.Exists(path))
                //{
                //    File.CreateText(path).Close();
                //}

                //File.AppendAllText(path, "Приятный вечер!");

                //  File.Method... - открывает FileStream
                //using (var stream = new FileStream(path, FileMode.OpenOrCreate)) 
                //{
                //    var text = "Какой-то текст";
                //    var bytes = System.Text.Encoding.Default.GetBytes(text);

                //    stream.Write(bytes, 0, bytes.Length);
                //}

                //using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                //{
                //    var bytes = new byte[stream.Length];
                //    stream.Read(bytes, 0, bytes.Length);

                //    var text = System.Text.Encoding.Default.GetString(bytes);
                //}

                using (var stream = new StreamWriter(path))
                {
                    var text = "Какой-то текст ДВА";
                    stream.WriteLine(text);
                }

                using (var stream = new StreamReader(path))
                {
                    var text = stream.ReadToEnd();
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода!");
            }
            Console.ReadKey();
        }
    }
}
