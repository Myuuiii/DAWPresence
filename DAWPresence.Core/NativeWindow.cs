using System.Runtime.InteropServices;
using System.Text;

namespace DAWPresence;

/// <summary>
///     Helpers for reading window titles directly via Win32, for apps where
///     Process.MainWindowTitle picks the wrong top-level window.
/// </summary>
internal static class NativeWindow
{
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    /// <summary>
    ///     Finds the title of the top-level window owned by the given process whose
    ///     window class name matches <paramref name="className"/>, or null if none is found.
    /// </summary>
    public static string? FindWindowTitleByClassName(int processId, string className)
    {
        string? title = null;
        var classBuffer = new StringBuilder(256);
        var titleBuffer = new StringBuilder(512);

        EnumWindows((hWnd, _) =>
        {
            GetWindowThreadProcessId(hWnd, out var pid);
            if (pid != (uint)processId) return true;

            classBuffer.Clear();
            GetClassName(hWnd, classBuffer, classBuffer.Capacity);
            if (!string.Equals(classBuffer.ToString(), className, StringComparison.Ordinal)) return true;

            titleBuffer.Clear();
            GetWindowText(hWnd, titleBuffer, titleBuffer.Capacity);
            title = titleBuffer.ToString();
            return false;
        }, IntPtr.Zero);

        return title;
    }
}
