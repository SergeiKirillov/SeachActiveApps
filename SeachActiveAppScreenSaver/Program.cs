namespace SeachActiveAppScreenSaver
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length>0)
            {
                string firstArgument = args[0].ToLower().Trim();
                string secongArgument = null;

                if (firstArgument.Length>2)
                {
                    secongArgument = firstArgument.Substring(3).Trim();
                    firstArgument = firstArgument.Substring(0, 2);
                }
                else if (args.Length>1)
                {
                    secongArgument = args[1];
                }

                if (firstArgument=="/c")
                {
                    //TODO Configuration mode
                }
                else if (firstArgument=="/p")
                {
                    //TODO Preview mode
                    if (secongArgument==null)
                    {
                        MessageBox.Show("Sorry, but the expected window handle was not provided.","ScreenSaver", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    }
                    IntPtr previewWndHandle = new IntPtr(long.Parse(secongArgument));
                    Application.Run(new Form1(previewWndHandle));

                }
                else if (firstArgument=="/s")
                {
                    ShowScreenSaver();
                    Application.Run();
                }
                else 
                {
                    MessageBox.Show("Sorry, but the command line argument \"" + firstArgument + "\" is not valid.", "ScreenSaver",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            else
            {

            }
            
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }

        static void ShowScreenSaver()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                Form1 screensaver = new Form1(screen.Bounds);
                screensaver.Show();
            }
        }

    }
}