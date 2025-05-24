using ChatClient.MVVM.Core;
using ChatClient.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.MVVM.ViewModel
{
    public class MainViewModel
    {
        public required RelayCommand ConnectToServerCommand { get; set; }

        private Server server;

        public string Username { get; set; } = string.Empty;

        public MainViewModel()
        {
            server = new Server();

            ConnectToServerCommand = new RelayCommand(o =>
            
                server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
        }
    }
}