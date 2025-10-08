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
using iTextSharp.text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ex.xaml
    /// </summary>
    public partial class ex : Page
    {
        public ex()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
         Nav.MainFrame.GoBack();
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            var inf = Connect.context.Vehicles.ToList();
            Excel.Application ap = new Excel.Application();
            ap.Visible = true;
            Excel.Workbook worbook = ap.Workbooks.Add(Type.Missing);
            Excel.Worksheet worsheet = (Excel.Worksheet)ap.Worksheets.get_Item(1);
            worsheet.Name = "Учётная  таблица";
            Excel.Range ran = worsheet.Range["A1", "D2"];
            ran.Merge();
            ran.Value = "Ведомость ТС";
            ran.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ran.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            worsheet.Cells.Font.Name = "Times New Roman";
            worsheet.Cells[3, 1].Value = "ID";
            worsheet.Cells[3, 2].Value = "Государственный номер";
            worsheet.Cells[3, 3].Value = "Модель автомобиля";
            worsheet.Cells[3, 4].Value = "Грузоподъёмность";
            worsheet.Cells[3, 5].Value = "Статус ТС";
            var curRowt = 6;
            foreach (var item in inf)
            {
                worsheet.Cells[curRowt, 1].Value = item.VehicleID;
                worsheet.Cells[curRowt, 2].Value = item.LicensePlate;
                worsheet.Cells[curRowt, 3].Value = item.Model;
                worsheet.Cells[curRowt, 4].Value = item.LoadCapacity;
                worsheet.Cells[curRowt, 4].Value = item.Status;
                curRowt++;
            }
            worsheet.Cells[curRowt, 1].Value = "Кол-во записей: ";
            Excel.Range range = worsheet.Range[worsheet.Cells[curRowt, 1], worsheet.Cells[curRowt, 4]];
            range.Merge();
            Excel.Range rang = worsheet.Range[worsheet.Cells[3, 1], worsheet.Cells[curRowt, 4]];
            rang.Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
            worsheet.Columns.AutoFit();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = System.IO.Path.Combine(desktopPath, "Ведомость ТС");
            worbook.SaveAs(filePath);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ap);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ClientsLV.ItemsSource = Connect.context.Vehicles.ToList();
        }
    }
}
