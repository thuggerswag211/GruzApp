using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для AddReq.xaml
    /// </summary>
    public partial class AddReq : Page
    {
        AppData.Requests req;
        bool checkNew;
        public AddReq(AppData.Requests r)
        {
            InitializeComponent();
            if (r == null)
            {
                r = new AppData.Requests();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = req = r;
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew)
            {
                Connect.context.Requests.Add(req);
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
