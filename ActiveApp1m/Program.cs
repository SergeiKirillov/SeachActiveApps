using System;
using System.Threading;

namespace ActiveApp1m
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DateTime dtNow = DateTime.Now;
                Console.WriteLine(dtNow.ToString("dd.MM.yyyy HH:mm:ss") + "-" + clWinAPI.GetCaptionOfActiveWindow());
                Thread.Sleep(60000);
            }
            
        }

    }
}
