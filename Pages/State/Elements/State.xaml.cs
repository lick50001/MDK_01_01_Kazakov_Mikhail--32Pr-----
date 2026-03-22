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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VinylRecordsApplication_2.Pages.State.Elements
{

    public partial class State : UserControl
    {
        Classes.State state;
        Pages.State.Main main;

        public State(Classes.State state, Pages.State.Main main)
        {
            InitializeComponent();
            this.state = state;
            this.main = main;
            tbName.Text = this.state.Name;
            tbSubName.Text = this.state.SubName;
            tbDescription.Text = this.state.Description;
        }

        private void EditState(object sender, RoutedEventArgs e) => MainWindow.mainWindow.OpenPage(new Pages.State.Add(state));

        private void DeleteState(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить состояние: {this.state.Name}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }

            IEnumerable<Classes.Record> AllRecord = Classes.Record.AllRecords();

            if (AllRecord.Any(x => x.IdState == state.Id))
            {
                MessageBox.Show($"Состояние {this.state.Name} невозможно удалить, так как оно используется в списке пластинок. Сначала удалите зависимости.", "Ошибка удаления");
                return;
            }

            try
            {
                this.state.Delete();
                main.stateParent.Children.Remove(this);
                MessageBox.Show($"Состояние {this.state.Name} успешно удалено.", "Уведомление");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении из базы данных: " + ex.Message);
            }
        }
    }
}
