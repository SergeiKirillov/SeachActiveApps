using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


class clWinAPI
{
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    public static extern int SendMessage(int hWnd, IntPtr msg, int wParam, int lParam);

    public const int WM_SYSCOMMAND = 0x0112;
    public const int SC_CLOSE = 0xF060;

    #region V2

    
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

    public static void FindActiveWindows()
    {
        IntPtr handle = GetActiveWindow();
        //SendMessage(handle, WM_SYSCOMMAND, SC_CLOSE, 0);

    }

}

