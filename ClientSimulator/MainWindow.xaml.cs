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
            InitializeComponent();
        }

        private void GridSplitter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
