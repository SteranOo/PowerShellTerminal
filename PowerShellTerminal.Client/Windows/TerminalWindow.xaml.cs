using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PowerShellTerminal.Middleware.Network;

namespace PowerShellTerminal.Client.Windows
{
    public partial class TerminalWindow
    {
        private readonly IServerEngine _serverProxy;

        public TerminalWindow(IServerEngine serverProxy, string address)
        {
            InitializeComponent();
            _serverProxy = serverProxy;
            TbTerminalLog.Text = $"PowerShell Terminal\nConnected to: {address}\n\n";
            UpdateCurrentLocation();
        }

        private void UpdateCurrentLocation()
        {
            var result = _serverProxy.GetCurrentLocation();
            if (result.Success)
                TbCurPass.Text = result.Response + ">";
        }

        private void CpTextColor_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue == null || TbCommand == null || TbTerminalLog == null || TbCurPass == null)
                return;

            TbCommand.Foreground = new SolidColorBrush(e.NewValue.Value);
            TbTerminalLog.Foreground = new SolidColorBrush(e.NewValue.Value);
            TbCurPass.Foreground = new SolidColorBrush(e.NewValue.Value);
        }

        private void CpNgColor_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue == null || TbCommand == null || TbTerminalLog == null || TbCurPass == null)
                return;

            TbCommand.Background = new SolidColorBrush(e.NewValue.Value);
            TbTerminalLog.Background = new SolidColorBrush(e.NewValue.Value);
            TbCurPass.Background = new SolidColorBrush(e.NewValue.Value);
        }

        private void TbCommand_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    TbTerminalLog.Text += TbCurPass.Text + TbCommand.Text + "\n";
                    var result = _serverProxy.RunScript(TbCommand.Text);
                    if (result.Success && !string.IsNullOrEmpty(result.Response))
                        TbTerminalLog.Text += result.Response;
                    UpdateCurrentLocation();
                    TbCommand.Clear();
                    break;
            }
        }

        private void BtnNewWindow_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }
    }
}
