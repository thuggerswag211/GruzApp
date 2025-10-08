using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для Requests.xaml
    /// </summary>
    public partial class Requests : Page
    {
        public Requests()
        {
            InitializeComponent();
            ClientsLV.ItemsSource = Connect.context.Requests.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddReq(null));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var delRequests = ClientsLV.SelectedItems.Cast<AppData.Requests>().ToList();

            foreach (var request in delRequests)
            {
                if (Connect.context.Requests.Any(x => x.RequestID == request.RequestID))
                {
                    MessageBox.Show("Данные используются в таблице продаж", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (MessageBox.Show($"Удалить {delRequests.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Requests.RemoveRange(delRequests);
                try
                {
                    Connect.context.SaveChanges();
                    ClientsLV.ItemsSource = Connect.context.Requests.ToList();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        Console.WriteLine($"Сущность: {error.Entry.Entity.GetType().Name}");
                        foreach (var err in error.ValidationErrors)
                        {
                            Console.WriteLine($"- Поле: {err.PropertyName}");
                            Console.WriteLine($"- Ошибка: {err.ErrorMessage}");
                        }
                    }
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddReq((sender as Button).DataContext as AppData.Requests));
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClientsLV.ItemsSource = Connect.context.Requests.ToList();
        }
    }
}
