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
using WpfApp1.AppData;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Venicles.xaml
    /// </summary>
    public partial class Venicles : Page
    {
        public Venicles()
        {
            InitializeComponent();
            ClientsLV.ItemsSource = Connect.context.Vehicles.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddVenicl(null));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var delPostavshik = ClientsLV.SelectedItems.Cast<Vehicles>().ToList();


            foreach (var Postavshik in delPostavshik)
            {
                if (Connect.context.Vehicles.Any(x => x.VehicleID == Postavshik.VehicleID))
                {
                    MessageBox.Show("Данные используются в таблице продаж", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (MessageBox.Show($"Удалить {delPostavshik.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Vehicles.RemoveRange(delPostavshik);
                try
                {
                    Connect.context.SaveChanges();
                    ClientsLV.ItemsSource = Connect.context.Vehicles.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
           Nav.MainFrame.GoBack();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddVenicl((sender as Button).DataContext as Vehicles));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClientsLV.ItemsSource = Connect.context.Vehicles.ToList();
        }
    }
}
