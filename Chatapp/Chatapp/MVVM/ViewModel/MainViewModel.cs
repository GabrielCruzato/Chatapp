using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using ChatClient.MVVM.Core;
using ChatClient.MVVM.Model;
using ChatClient.Networking;

namespace ChatClient.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string username = string.Empty;
        private string message = string.Empty;

        private Server server;

        public ObservableCollection<UserModel> Users { get; set; } = new ObservableCollection<UserModel>();
        public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();

        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }

        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                    ConnectToServerCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Message
        {
            get => message;
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged();
                    SendMessageCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public MainViewModel()
        {
            server = new Server();

            server.userConnectedEvent += (username, uidString) =>
            {
                if (Guid.TryParse(uidString, out Guid uid))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (!Users.Any(u => u.UID == uid))
                        {
                            Users.Add(new UserModel { Username = username, UID = uid });
                            Messages.Add($"** {username} entrou no chat **");
                        }
                    });
                }
            };

            server.msgReceivedEvent += (msg) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(msg);
                });
            };

            server.userDisconnectedEvent += () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add("** Um usuário saiu do chat **");
                });
            };

            ConnectToServerCommand = new RelayCommand(
                o => server.ConnectToServer(Username),
                o => !string.IsNullOrWhiteSpace(Username));

            SendMessageCommand = new RelayCommand(
                o =>
                {
                    server.SendMessageToServer(Message);
                    Messages.Add($"Você: {Message}");
                    Message = string.Empty;
                },
                o => !string.IsNullOrWhiteSpace(Message));
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
