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
    /// Логика взаимодействия для Flit.xaml
    /// </summary>
    public partial class Flit : Page
    {
        public Flit()
        {
            InitializeComponent();
            ClientsLV.ItemsSource = Connect.context.Requests.ToList();

            var cmbFil = Connect.context.Requests.OrderBy(x => x.Status).ToList();
            cmbFil.Insert(0, new AppData.Requests { Status = "По умолчанию" });
            FioFlit.ItemsSource = cmbFil.Select(x => x.Status).ToList().Distinct();
            FioFlit.SelectedIndex = 0;
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void FioFlit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filt = Connect.context.Requests.ToList();
            if (FioFlit.SelectedIndex > 0)
                filt = filt.Where(x => x.Status.ToString().Contains(FioFlit.SelectedItem.ToString())).ToList();
            ClientsLV.ItemsSource = filt;
        }
    }
}
