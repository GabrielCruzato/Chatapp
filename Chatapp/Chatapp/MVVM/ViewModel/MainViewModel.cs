using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChatClient.MVVM.Core;
using ChatClient.Networking;
using ChatClient.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace ChatClient.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string username = string.Empty;

        private Server server;

        public ObservableCollection<UserModel> Users { get; set; } = new ObservableCollection<UserModel>();

        public RelayCommand ConnectToServerCommand { get; set; }

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

        public MainViewModel()
        {
            server = new Server();

            server.userConnectedEvent += (username, uidString) =>
            {
                if (Guid.TryParse(uidString, out Guid uid))
                {
                    if (!Users.Any(u => u.UID == uid))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Users.Add(new UserModel { Username = username, UID = uid });
                            Console.WriteLine($"Adicionado sexo: {username}");
                        });
                    }
                }
                else
                {
                    Console.WriteLine($"UID inválido recebido: {uidString}");
                }
            };


            ConnectToServerCommand = new RelayCommand(
                o => server.ConnectToServer(Username),
                o => !string.IsNullOrWhiteSpace(Username));
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
