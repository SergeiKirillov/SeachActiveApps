
using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace AppToAppClient
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Получено сообщение :");

            while (true)
            {
                //Массив для сообщений из общей памяти
                char[] message1;

                //Размер введенного сообщения
                int size;

                //Отсчет до выключния
                int message2;

                //получение существующего участка разделенной памяти 
                //параметр - название участка

                MemoryMappedFile shareMemory = MemoryMappedFile.OpenExisting("TimeDisableScreenSave");

                //Сначала считываем размер сообщния, чтобы создать массив данного размера
                //Integer занимает 4 байта, начинается с первого байта, поэтому передаем цифры 0 и 4

                using (MemoryMappedViewAccessor reader = shareMemory.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read))
                {
                    size = reader.ReadInt32(0);
                }


                using (MemoryMappedViewAccessor reader = shareMemory.CreateViewAccessor(4, 4, MemoryMappedFileAccess.Read))
                {
                    message2 = reader.ReadInt32(0);
                }

                //Считываем сообщение, используя полученный выше размер
                //Сообщение - это строка или массив объектов char, каждый из которых занимает два байта
                //Поэтому вторым параметром передаем число символов умножив на из размер в байтах плюс
                //А первый параметр - смещение - 4 байта, которое занимает размер сообщения
                using (MemoryMappedViewAccessor rear = shareMemory.CreateViewAccessor(8, size * 2, MemoryMappedFileAccess.Read))
                {
                    //Массив символов сообщения
                    message1 = new char[size];
                    rear.ReadArray<char>(0, message1, 0, size);
                }


              


                Console.Write(DateTime.Now + " -1- ");
                Console.Write(message1);
                Console.Write('\n');
                Console.WriteLine(DateTime.Now + " -2- " + message2);
                //Console.WriteLine("Для выхода из программы нажмите любую клавишу");
                //Console.ReadLine();



                Thread.Sleep(TimeSpan.FromMinutes(1));
            }



        }
    }
}