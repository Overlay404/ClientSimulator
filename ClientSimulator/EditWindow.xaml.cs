using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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
                DealShare = double.Parse(DealShare.Text.Trim())
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
                bool checkValue = false;
            try
            {

                switch (IsRealtor)
                {
                    case false:


                        Regex regexEmail = new Regex(@"([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+)");
                        Regex regexPhone = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");
                        string txtMessage = "";



                        if (!regexEmail.IsMatch(Email.Text) && !regexPhone.IsMatch(Phone.Text))
                        {
                            MessageBox.Show("В поле Почта или Телефон неверный формат строки");
                            checkValue = true;
                        }

                        break;
                    case true:

                        if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Surname.Text)
                        || string.IsNullOrEmpty(Patronomic.Text) || string.IsNullOrEmpty(DealShare.Text))
                        {
                            MessageBox.Show("Нет ФИО");
                            checkValue = true;
                        }

                        if (DealShare.Text.All(d => char.IsLetter(d)) || double.Parse(DealShare.Text) > 100 || double.Parse(DealShare.Text) < 0)
                        {
                            MessageBox.Show("В поле Процентная ставка неверный формат строки");
                            checkValue = true;
                        }
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
                checkValue = true;
            }
                return checkValue;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.db.SaveChanges();
        }

        private void Name_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            HandledDigit(e);
        }

        private void Surname_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            HandledDigit(e);
        }

        private void Patronomic_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            HandledDigit(e);
        }

        void HandledLetter(System.Windows.Input.TextCompositionEventArgs e)
        {
            if (double.TryParse(DealShare.Text + e.Text, out _) == false)
                e.Handled = true;
        }

        void HandledDigit(System.Windows.Input.KeyEventArgs e)
        {
            if (new KeyConverter().ConvertToString(e.Key).All(letter => char.IsDigit(letter)))
                e.Handled = true;
        }

        private void DealShare_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            HandledLetter(e);
        }

        private void Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Regex regexPhone = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$");

            if (!regexPhone.IsMatch(Phone.Text))
            {
                MessageBox.Show("Не верный формат ввода номера телефона");
                Phone.Text = "";
            }
        }
    }
}
