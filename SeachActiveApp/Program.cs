using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeachActiveApp
{
    class Program
    {
        static void Main(string[] args)
        {
            clWinAPI.HideConsoleApp(true);
            while (true)
            {


                //DateTime dtNow = DateTime.Now;
                //Console.WriteLine(dtNow.ToString("dd.MM.yyyy HH:mm:ss") + "-" + clWinAPI.GetCaptionOfActiveWindow());


                //if (message0 != message)
                //{

                //    tmptxt = String.Format("{0:dd.MM.yyyy HH:mm:ss} {1}", currenttime, message);


                //}
                //else
                //{
                //    tmptxt = String.Format("{0:dd.MM.yyyy HH:mm:ss} {1}", currenttime, "-----Повтор---- " + message);

                //}


                clFileRW.WriteFileTXT(DateTime.Now, clWinAPI.GetCaptionOfActiveWindow());

                Thread.Sleep(60000);
            }

        }
    }
}
