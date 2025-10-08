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
    /// Логика взаимодействия для AddVenicl.xaml
    /// </summary>
    public partial class AddVenicl : Page
    {
        Vehicles ven;
        bool checkNew;
        public AddVenicl(Vehicles v)
        {
            InitializeComponent();
            if (v == null)
            {
                v = new Vehicles();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = ven = v;
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                Connect.context.Vehicles.Add(ven);
            }
            try
            {
                Connect.context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Nav.MainFrame.GoBack();
        }

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
