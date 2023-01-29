using ClientSimulator.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;

namespace ClientSimulator
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Client> Clients
        {
            get { return (ObservableCollection<Client>)GetValue(ClientsProperty); }
            set { SetValue(ClientsProperty, value); }
        }

        public static readonly DependencyProperty ClientsProperty =
            DependencyProperty.Register("Clients", typeof(ObservableCollection<Client>), typeof(MainWindow));


        public ObservableCollection<Realtor> Realtors
        {
            get { return (ObservableCollection<Realtor>)GetValue(RealtorsProperty); }
            set { SetValue(RealtorsProperty, value); }
        }

        public static readonly DependencyProperty RealtorsProperty =
            DependencyProperty.Register("Realtors", typeof(ObservableCollection<Realtor>), typeof(MainWindow));

        public MainWindow()
        {
            Clients = new ObservableCollection<Client>(App.db.Client);
            Realtors = new ObservableCollection<Realtor>(App.db.Realtor);
            InitializeComponent();
        }

        private void RemoveClient(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ClientsTable.SelectedItem == null) return;

            Clients.Remove(ClientsTable.SelectedItem as Client);
        }

        private void AddClient(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void RemoveRealtor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (RealtorsTable.SelectedItem == null) return;

            Realtors.Remove(RealtorsTable.SelectedItem as Realtor);
        }

        private void AddRealtor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

    }
}
