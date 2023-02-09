using ClientSimulator.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System;
using System.Text.RegularExpressions;

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

        public static MainWindow Instance;

        public MainWindow()
        {
            Clients = new ObservableCollection<Client>(App.db.Client);
            Realtors = new ObservableCollection<Realtor>(App.db.Realtor);
            InitializeComponent();
            Instance = this;
        }

        private void RemoveClient(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ClientsTable.SelectedItem == null) return;

            if(MessageBox.Show("Вы действительно хотите удалить?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.db.Client.Remove(ClientsTable.SelectedItem as Client);
                Clients.Remove(ClientsTable.SelectedItem as Client);
            }
        }

        private void AddClient(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new EditWindow(false).Show();
        }

        private void RemoveRealtor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (RealtorsTable.SelectedItem == null) return;

            if (MessageBox.Show("Вы действительно хотите удалить?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.db.Realtor.Remove(RealtorsTable.SelectedItem as Realtor);
                Realtors.Remove(RealtorsTable.SelectedItem as Realtor);
            }
        }

        private void AddRealtor(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            new EditWindow(true).Show();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            App.db.SaveChanges();
        }

        private void RealtorsTable_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            Regex regexEmail = new Regex(@"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)");
            Regex regexPhone = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");

            try
            {
                if (e.Column.Header.ToString() == "Процентная ставка" && int.Parse((e.EditingElement as TextBox).Text) > 100)
                {
                    MessageBox.Show("Значение может быть в диапазоне от 0 до 100");
                    (e.EditingElement as TextBox).Text = "0";
                }

                if (e.Column.Header.ToString() == "Телефон" && !regexPhone.IsMatch((e.EditingElement as TextBox).Text))
                {
                    MessageBox.Show("Поле Телефон не соответствует формату");
                    (e.EditingElement as TextBox).Text = "";
                }

                if (e.Column.Header.ToString() == "Почта" && !regexEmail.IsMatch((e.EditingElement as TextBox).Text))
                {
                    MessageBox.Show("Поле Почта не соответствует формату");
                    (e.EditingElement as TextBox).Text = "";
                }
                if (e.Column.Header.ToString() == "Имя" && (e.EditingElement as TextBox).Text.All(c => char.IsLetter(c)))
                {
                    MessageBox.Show("Поле Почта не соответствует формату");
                    (e.EditingElement as TextBox).Text = "";
                }
                if (e.Column.Header.ToString() == "Фамилия" && (e.EditingElement as TextBox).Text.All(c => char.IsLetter(c)))
                {
                    MessageBox.Show("Поле Почта не соответствует формату");
                    (e.EditingElement as TextBox).Text = "";
                }
                if (e.Column.Header.ToString() == "Отчество" && (e.EditingElement as TextBox).Text.All(c => char.IsLetter(c)))
                {
                    MessageBox.Show("Поле Почта не соответствует формату");
                    (e.EditingElement as TextBox).Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
