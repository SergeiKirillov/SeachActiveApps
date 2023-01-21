namespace ScreenSaveTest1
{
    internal static class Program
    {
        /// &lt;summary>
        ///  The main entry point for the application.
        /// &lt;summary>
        /// https://www.nookery.ru/how-to-write-screen-saver-on-c/
        /// 

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length>0)
            {
                if (args[0].ToLower().Trim().Substring(0,2)=="/s")
                {
                    Application.EnableVisualStyles();   
                    Application.SetCompatibleTextRenderingDefault(false);
                    ShowScreeSaver();
                    Application.Run();
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/p")
                {
                    
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //args[1] — дескриптор окна предварительного просмотра
                    Application.Run(new Form1(new IntPtr(long.Parse(args[1]))));
                    
                }
                else if (args[0].ToLower().Trim().Substring(0, 2) == "/c")
                {
                    MessageBox.Show("Эта заставка не имеет опций, которые вы можете установить.", ".NET Screen Saver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    //Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new frmConfig);
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    ShowScreeSaver();
                    Application.Run();
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ShowScreeSaver();
                Application.Run();
            }
            

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }

        static void ShowScreeSaver()
        {
            foreach (Screen scrItem in Screen.AllScreens)
            {
                Form1 screenSave = new Form1(scrItem.Bounds);
                screenSave.Show();
            }
        }
    }
}