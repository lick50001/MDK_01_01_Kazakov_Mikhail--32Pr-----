using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinylRecordsApplication_2.Pages.Manufacturer
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public IEnumerable<Classes.Country> AllCountries = Classes.Country.AllCountries();
        Classes.Manufacturer changeManufacturer;
        public Add(Classes.Manufacturer changeManufacturer = null)
        {
            InitializeComponent();
            foreach (var Countrie in AllCountries)  
                tbCountry.Items.Add(Countrie.Name);
            if (AllCountries.Count() > 0)
                tbCountry.SelectedIndex = 0;

            if (changeManufacturer != null)
            {
                this.changeManufacturer = changeManufacturer;
                tbName.Text = changeManufacturer.Name;
                tbPhone.Text = changeManufacturer.Phone;
                tbEmail.Text = changeManufacturer.Mail;
                tbCountry.SelectedIndex = AllCountries.ToList().FindIndex(x => x.Id == changeManufacturer.CountryCode);
                addBtn.Content = "Изменить";
            }
        }

        private void AddManufacturer(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbName.Text))
                if (!String.IsNullOrWhiteSpace(tbPhone.Text))
                    if (!String.IsNullOrWhiteSpace(tbEmail.Text))
                        if (CorrectPhone(tbPhone.Text))
                            if (CorrectEmail(tbEmail.Text))
                            {
                                if (changeManufacturer == null)
                                {
                                    Classes.Manufacturer manufacturer = new Classes.Manufacturer()
                                    {
                                        Name = tbName.Text,
                                        Phone = tbPhone.Text,
                                        Mail = tbEmail.Text,
                                        CountryCode = AllCountries.Where(x => x.Name == tbCountry.SelectedItem.ToString()).First().Id
                                    };

                                    manufacturer.Save();
                                    MessageBox.Show($"Поставщик {manufacturer.Name} успешно добавле.", "Уведомление");
                                    MainWindow.mainWindow.OpenPage(new Add(manufacturer));
                                }
                                else
                                {
                                    changeManufacturer.Name = tbName.Text;
                                    changeManufacturer.Phone = tbPhone.Text;
                                    changeManufacturer.Mail = tbEmail.Text;
                                    changeManufacturer.CountryCode = AllCountries.Where(x => x.Name == tbCountry.SelectedItem.ToString()).First().Id;
                                    changeManufacturer.Save(true);
                                    MessageBox.Show($"Поставщик {changeManufacturer.Name} успешно изменен.", "Увдеомление");
                                }

                                MainWindow.mainWindow.OpenPage(new Pages.Manufacturer.Main());
                            } else
                                MessageBox.Show($"Пожалуйста, укажите почту поставщика в формате xx@xx.xx.", "Предупреждение");
                        else
                            MessageBox.Show($"Пожалуйста, укажите номер поставщика в формате 89000000000.", "Предупреждение");
                    else
                        MessageBox.Show($"Пожалуйста, укажите почту поставщика.", "Предупреждение");
                else
                    MessageBox.Show($"Пожалуйста, укажите телефон поставщика.", "Предупреждение");
            else
                    MessageBox.Show($"Пожалуйста, укажите наименование поставщика.", "Предупреждение");
        }

        public bool CorrectPhone(string Value)
        {
            string sRegex = "89[0-9]{9}$";
            Regex regex = new Regex(sRegex);
            MatchCollection matches = regex.Matches(Value);
            return matches.Count > 0;
        }

        public bool CorrectEmail(string Value)
        {
            string sRegex = "[aA-zZ]{2,20}@[aA-zZ]{2,20}.[aA-zZ]{2,3}";
            Regex regex = new Regex(sRegex);
            MatchCollection matches = regex.Matches(Value);
            return matches.Count > 0;
        }

        private void tbPreviewNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
