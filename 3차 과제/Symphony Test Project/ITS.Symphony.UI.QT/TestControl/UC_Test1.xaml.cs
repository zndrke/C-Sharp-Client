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
    public enum Category
    {
        경제,
        에세이,
        소설,
        문학,
        IT
    }

    /// <summary>
    /// UC_Test1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UC_Test1 : UserControlBase
    {
        public UC_Test1 ()
        {
            InitializeComponent();
            this.xamDataGrid1.Initialize();
            
            ObservableCollection<Book_Data> datasource = new ObservableCollection<Book_Data>();

            datasource.Add(new Book_Data("WPF 프로그래밍", "이", "가메", 1, "20101213", "이것은", Category.경제, 0.3));
            datasource.Add(new Book_Data("C#", "김", "YH", 1000, "20140914", "공부", Category.에세이, 0.1085));
            datasource.Add(new Book_Data("Hello", "김", "우리", 999, "20140911", "인사",Category.소설, 1));
            datasource.Add(new Book_Data("OOP", "게리", "Relly", 22012, "19121216", "객체지향", Category.문학, - 0.702));
            this.xamDataGrid1.DataSource = datasource;
        }
        public class Book_Data
        {
            public string sTitle { get; set; }
            public string sWriter { get; set; }
            public string sPublisher { get; set; }
            public int iPage { get; set; }
            public string sDate { get; set; }
            public string sInfo { get; set; }
            public Category eCategory { get; set; }
            public double dRate { get; set; }

            public Book_Data (string sTitle, string sWriter, string sPublisher, int iPage, string sDate, string sInfo, Category eCategory, double dRate)
            {
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
            if (this.xamDataGrid1.FieldSettings.AllowRecordFiltering != true) {   
                this.xamDataGrid1.FieldSettings.AllowRecordFiltering = true;
            } else {
                this.xamDataGrid1.FieldSettings.AllowRecordFiltering = false;
            }     
        }
    }
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) {
                return "";
            }
            bool bCanConvert;
            string sStr = value.ToString();
            string sResult="";
            if(parameter.Equals("iPage")) {
                int item;
                bCanConvert = int.TryParse(sStr,out item);
                if (bCanConvert == true) {
                    sResult = item.ToString("#,##0");
                } else {
                    return "";
                }
            } else if (parameter.Equals("dRate")) {
                double item;
                bCanConvert = double.TryParse(sStr, out item);
                if (bCanConvert == true) {
                    sResult = item.ToString("P");
                } else {
                    return "";
                }
            }
            return sResult;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
