using System;
using System.Configuration;
using System.Windows;
using PowerShellTerminal.Client.Network;
using PowerShellTerminal.Middleware.Network;

namespace PowerShellTerminal.Client.Windows
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var defAddress = ConfigurationManager.AppSettings.Get("DefaultAddress");
            TbAddress.Text = defAddress;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TbAddress.Text))
                return;

            try
            {
                var serverProxy = new ServiceClientFactory<IServerEngine>().Create(TbAddress.Text);
                var result = serverProxy.Connect();
                if (!result.Success)
                    return;

                
                var terminalWindow = new TerminalWindow(serverProxy, TbAddress.Text);
                terminalWindow.Show();
                Close();
            }
            catch
            {
                MessageBox.Show("Cannot connect to server", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
