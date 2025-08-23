using System.Diagnostics;

namespace DAWPresenceBackgroundApp
{
    public static class NotificationService
    {
        public static void ShowNotification(string title, string message)
        {
            string psCommand =
                $"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] > $null;" +
                $"$template = [Windows.UI.Notifications.ToastTemplateType]::ToastText02;" +
                $"$xml = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent($template);" +
                $"$textNodes = $xml.GetElementsByTagName('text');" +
                $"$textNodes.Item(0).AppendChild($xml.CreateTextNode('{title}')) > $null;" +
                $"$textNodes.Item(1).AppendChild($xml.CreateTextNode('{message}')) > $null;" +
                $"$toast = [Windows.UI.Notifications.ToastNotification]::new($xml);" +
                $"$notifier = [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('DAWPresence');" +
                "$notifier.Show($toast);";

            var psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-NoProfile -Command \"{psCommand.Replace("\"", "`\"").Replace("\n", " ")}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {
                using var process = Process.Start(psi);
                process?.WaitForExit(4000);
            }
            catch
            {
            }
        }
    }
}