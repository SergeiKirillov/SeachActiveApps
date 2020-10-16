using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


class clWinAPI
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


    #endregion


    #region #region Active App V1

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    public static extern int SendMessage(int hWnd, IntPtr msg, int wParam, int lParam);

    public const int WM_SYSCOMMAND = 0x0112;
    public const int SC_CLOSE = 0xF060;

    public static void FindActiveWindows()
    {
        IntPtr handle = GetActiveWindow();
        //SendMessage(handle, WM_SYSCOMMAND, SC_CLOSE, 0);

    }
    #endregion

    #region Active App V2 


    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetWindowTextLength(IntPtr hWnd);

    public static string GetCaptionOfActiveWindow()
    {
        var strTitle = string.Empty;
        var handle = GetForegroundWindow();
        // Obtain the length of the text   
        var intLength = GetWindowTextLength(handle) + 1;
        var stringBuilder = new StringBuilder(intLength);
        if (GetWindowText(handle, stringBuilder, intLength) > 0)
        {
            strTitle = stringBuilder.ToString();
        }
        return strTitle;
    }

    #endregion



}

