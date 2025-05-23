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

        private Server? _server;

        public MainViewModel()
        {
        }
    }
}