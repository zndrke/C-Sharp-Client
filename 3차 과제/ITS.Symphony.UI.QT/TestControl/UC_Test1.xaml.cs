using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;

using ITS.Symphony.Controls;
using ITS.Symphony.Common;
using ITS.Symphony.Common.Base;
using ITS.Symphony.Common.Converter;
using ITS.Symphony.Common.Net;
using ITS.Symphony.Common.Helper;
using ITS.Symphony.Entity;

using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Editors;
using Infragistics.Windows.DataPresenter.Events;

namespace ITS.Symphony.UI.QT.TestControl
{
    /// <summary>
    /// UC_Test1.xaml에 대한 상호 작용 논리
    /// </summary>d
    
    public enum Category
    {
        경제,
        에세이,
        소설,
        문학,
        IT
    }
    public partial class UC_Test1 : UserControlBase
    {
        
        public UC_Test1 ()
        {
            InitializeComponent();

            this.xamDataGrid1.Initialize();

            ObservableCollection<Book_Data> datasource = new ObservableCollection<Book_Data>();

            datasource.Add(new Book_Data(false,"WPF 프로그래밍", "이", "가메", 1, "20101213", "이것은", Category.경제, 0.3));
            datasource.Add(new Book_Data(false,"C#", "김", "YH", 1000, "20140914", "공부", Category.에세이, 0.1085));
            datasource.Add(new Book_Data(false,"Hello", "김", "우리", 999, "20140911", "인사",Category.소설, 1));
            datasource.Add(new Book_Data(false,"OOP", "게리", "Relly", 22012, "19121216", "객체지향", Category.문학, - 0.702));
            this.xamDataGrid1.DataSource = datasource;
            
        }
        private XamDateTimeEditor CreateDateEditor ()
        {
            XamDateTimeEditor editor = new XamDateTimeEditor();
            editor.AutoFillDate = AutoFillDate.MonthAndYear;
            editor.DropDownButtonDisplayMode = DropDownButtonDisplayMode.Always;
            editor.Mask = "{yyyy/mm/dd}";
            return editor;
        }

        public class Book_Data
        {
            public bool cbxCheck { get; set; }
            public string sTitle { get; set; }
            public string sWriter { get; set; }
            public string sPublisher { get; set; }
            public int iPage { get; set; }
            public string sDate { get; set; }
            public string sInfo { get; set; }
            public Category eCategory { get; set; }
            public double dRate { get; set; }


            public Book_Data (bool cbxCheck, string sTitle, string sWriter, string sPublisher, int iPage, string sDate, string sInfo, Category eCategory, double dRate)
            {
                this.cbxCheck = cbxCheck;
                this.sTitle = sTitle;
                this.sWriter = sWriter;
                this.sPublisher = sPublisher;
                this.iPage = iPage;
                this.sDate = sDate;
                this.sInfo = sInfo;
                this.eCategory = eCategory;
                this.dRate = dRate;
            }
        }

        private void ShowFilter_Click (object sender, RoutedEventArgs e)
        {
            //필터 보이기

        }

    }
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            string sStr = value.ToString();
            String sResult="";
            if(parameter.Equals("iPage")) {
                var item = int.Parse(sStr);
                sResult = item.ToString("#,##0");
            } else if (parameter.Equals("dRate")) {
                var item = double.Parse(sStr);
                sResult = item.ToString("P");
            }

            return sResult;
        }
 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
