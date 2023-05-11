using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using MyLibenNetFramework;

 /// <summary>
/// модуль отвечает за запись/чтение данных в базу LiteDb
/// </summary>
/// 
public class clData1Hour
{
    [BsonId]

    public Guid ID { get; set; }
    public DateTime dtApp { get; set; }
    public String strApp { get; set; }
    public int Raz1Minut { get; set; }
}
public class clDataAppCount
{

    public string strApp { get; set; }
    public int CountMinut { get; set; }
}
public class clRWLiteDB
{
    public clRWLiteDB()
    {
        

    }

    public static void AciveApp()
    {
        ///<summary>Запуск программы сборщика информации с циклом 1 мин. Предварительно производим проверку необходимо ли роизводить сбор данных</summary>

        while (true)
        {
            if ((WorkInReestr.blToAPP("EnableActiveAppSaving"))) //Если в реестре включен сбор информации то заходим внутрь 
            {
                string strActivApp = clWinAPI.GetCaptionOfActiveWindow();
                DateTime dtActiveApp = DateTime.Now;
                new clRWLiteDB(dtActiveApp, strActivApp, 1);
            }
           

            Thread.Sleep(TimeSpan.FromMinutes(1));

        }
    }



    public clRWLiteDB(DateTime dt, string message, int time1min)
    {
        ///<summary>С реестра считывается значение и идет запуск функции по сбору и записи данных</summary>
        //if (Properties.Settings.Default.blWriteFile)
        //{
        //    WriteFileTXT(dt, message, time1min);
        //}

        //if (Properties.Settings.Default.blWriteBD)
        //{
        //    WriteBD(dt, message, time1min);
        //}
        if (WorkInReestr.blToAPP("SaveDateToBD"))
        {
            WriteBD(dt, message, time1min);
        }

        if (WorkInReestr.blToAPP("SaveDateToFile"))
        {
            WriteFileTXT(dt, message, time1min);
        }
    }

    #region считываем значения с БД 

        


        //Все значения 
        public IList<clData1Hour> GetAll()
        {
            string NameDB = DateTime.Now.ToString("MM-yyyy");
            string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + NameDB + ".db";

            var rezult = new List<clData1Hour>();

            using (var db = new LiteDatabase(pathProg))
            {
                var apps = db.GetCollection<clData1Hour>("Hour1");
                var resultsLDB = apps.FindAll().OrderByDescending(x => x.dtApp);

                foreach (clData1Hour item in resultsLDB)
                {
                    rezult.Add(item);
                }

                return rezult;
            }
        }

        #endregion


    #region Вывод в файл
        private static void WriteFileTXT(DateTime dt, string message, int time1min)
        {
            try
            {
                if (message != "" || message != null || message != " ")
                {
                    string tmptxt;
                    DateTime TimeWrite = dt;

                    tmptxt = dt.ToString("dd.MM.yyyy HH:mm:ss") + ";" + message;

                    //Если не удачно то записываем в локальный файл
                    string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "Log.txt";
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathProg, true))
                    {

                        file.WriteLine(tmptxt);
                        file.Close();
                    }


                }

            }
            catch
            { }
        }
        #endregion

    #region запись в Базу данных
        private static void WriteBD(DateTime dt, string message, int time1min)
        {
            try
            {
                string NameDB = DateTime.Now.ToString("MM-yyyy");
                string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + NameDB + ".db";


                
                var DataWrite = new clData1Hour
                {
                    ID = Guid.NewGuid(),
                    dtApp = dt,
                    strApp = message,
                    Raz1Minut = time1min
                };

                using (var db = new LiteDatabase(pathProg))
                {
                    var DBWrite = db.GetCollection<clData1Hour>("Hour1");

                    DBWrite.Insert(DataWrite);
                    IndexIsWrite(DBWrite);

                }


            }
            catch (Exception exc)
            {
                // в случае ошибки записываем её в файл логов
                WriteFileTXT(DateTime.Now,exc.Message,1);
            }
            
        }

        private static void IndexIsWrite(ILiteCollection<clData1Hour> dBWrite)
        {
            dBWrite.EnsureIndex(x => x.ID);
            dBWrite.EnsureIndex(x => x.dtApp);
            dBWrite.EnsureIndex(x => x.strApp);
        }

        #endregion

    public IList<clDataAppCount> Get(bool Day, DateTime dt)
    {
        var rezult = new List<clData1Hour>();
        var resultAppCount = new List<clDataAppCount>();

        int day = dt.Day;
        int mount = dt.Month;
        int year = dt.Year;

        string pathProg = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + mount.ToString("D2") + "-" + year.ToString() + ".db";



        //using ( var db = new LiteDatabase(pathProg))
        using (var db = new LiteDatabase(@"Filename="+pathProg+";Connection=shared"))
        {
            var apps = db.GetCollection<clData1Hour>("Hour1");

            //var resultsLDB = apps.FindAll().OrderByDescending(x => x.dtApp);

            //Групировка по полю ------ SELECT strApp, count(Raz1Minut) FROM Hour1 Group by strApp Order by Count(Raz1Minut) Desc;

            //var groupedPost = posts.Find(p => p.Tags.Count > 10 ).GroupBy(*Group Condition*);
            //сгрупировать по приложению и подсчитать кол-во элементов

            /*              
            Или просумировать по полю Raz1Minut при одинаковом имени поля
             */





            //////Первый вариант 
            ///
            //var resultsLDB = apps.FindAll().OrderByDescending(x => x.dtApp);
            //foreach (clData1Hour item in resultsLDB)
            //{

            //    rezult.Add(item);
            //}
            ////return rezult.FindAll(i=>i.dtApp.Date==dt);


            //передор значенией с подсчетом по столбцу Raz1Minut и группировкой
            //https://www.google.com/search?client=firefox-b-d&q=list+group+by+count+c%23



            //Второй вариант - Групировка 
            //var result = apps
            //    .Find(Query.EQ("strApp", "Debug"))
            //    .Where(x => x.dtApp <= DateTime.Now.Date)
            //    .OrderBy(x => x.strApp)
            //    .Select(x => new
            //    {
            //        App =x.strApp,
            //        Dt = x.dtApp
            //    });

            //-----------
            //var result = apps
            //    .Find(Query.Not("strApp", "Debug"))
            //    .Where(x => x.dtApp <= DateTime.Now.Date)
            //    .OrderBy(x => x.strApp)
            //    ;
            //int CountResult = result.Count();

            //foreach (clData1Hour item in result)
            //{

            //    rezult.Add(item);
            //}

            if (Day)
            {
                var result = apps
                .Find(Query.Not("strApp", null))
                .Where(x => x.dtApp.Date == dt.Date)
                .GroupBy(x => x.strApp)
                .Select(x => new clDataAppCount
                {
                    strApp = x.Key,
                    CountMinut = x.Count()
                }
                )
                ;
                int countResult = result.Count();

                foreach (var item in result.OrderByDescending(x=>x.CountMinut))
                {
                    string name = item.strApp;
                    int count = item.CountMinut;
                    System.Diagnostics.Debug.WriteLine(name + " --- " + count.ToString());

                    resultAppCount.Add(item);

                }
            }
            else
            {
                var result = apps
                .Find(Query.Not("strApp", null))
                .Where(x => x.dtApp.Date.Month == dt.Date.Month)
                .GroupBy(x => x.strApp)
                .Select(x => new clDataAppCount
                {
                    strApp = x.Key,
                    CountMinut = x.Count()
                }
                )
                ;
                int countResult = result.Count();

                foreach (var item in result.OrderByDescending(x => x.CountMinut))
                {
                    string name = item.strApp;
                    int count = item.CountMinut;
                    System.Diagnostics.Debug.WriteLine(name + " --- " + count.ToString());

                    resultAppCount.Add(item);

                }
            }






            return resultAppCount;


        }
    }
    


}

