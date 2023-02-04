using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientSimulator
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        readonly bool IsRealtor;
        public EditWindow(bool _IsRealtor)
        {
            InitializeComponent();
            IsRealtor = _IsRealtor;
            if (IsRealtor)
            {
                GroupEmail.Visibility = Visibility.Collapsed;
                GroupPhone.Visibility = Visibility.Collapsed;
            }
            else
            {
                GroupDealShare.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsRealtor)
            {
                AddNewRealtors();
                return;
            }
            AddNewClients();
        }

        private void AddNewRealtors()
        {
            if (CheckValueFields()) return;
            var NewObjectRealtor = new Model.Realtor
            {
                Name = Name.Text.Trim(),
                Surname = Surname.Text.Trim(),
                Patronymic = Patronomic.Text.Trim(),
                DealShare = int.Parse(DealShare.Text.Trim())
            };
            MainWindow.Instance.Realtors.Add(NewObjectRealtor);
            App.db.Realtor.Add(NewObjectRealtor);
            Close();
        }

        private void AddNewClients()
        {
            if (CheckValueFields()) return;
            var NewObjectClient = new Model.Client
            {
                Name = Name.Text.Trim(),
                Surname = Surname.Text.Trim(),
                Patronymic = Patronomic.Text.Trim(),
                Email = Email.Text.Trim() ?? null,
                Phone = Phone.Text.Trim() ?? null
            };
            MainWindow.Instance.Clients.Add(NewObjectClient);
            App.db.Client.Add(NewObjectClient);
            Close();
        }

        private bool CheckValueFields()
        {
            switch (IsRealtor)
            {
                case false:
                    if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Surname.Text)
                    || string.IsNullOrEmpty(Patronomic.Text) || string.IsNullOrEmpty(Phone.Text) && string.IsNullOrEmpty(Email.Text)) return true;
                    break;
                case true:
                    if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Surname.Text)
                    || string.IsNullOrEmpty(Patronomic.Text) || string.IsNullOrEmpty(DealShare.Text)) return true;
                    break;
            }
                return false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.db.SaveChanges();
        }
    }
}
