using System;
using System.Runtime.InteropServices;

public class Class1
{
	public Class1()
	{
    #region Hide Console App
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    //And then if you want to hide it use this command:

    public static void HideConsoleApp(bool blHide)
    {
        if (blHide)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
        }
        else
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_SHOW);
        }


    }
}
}
