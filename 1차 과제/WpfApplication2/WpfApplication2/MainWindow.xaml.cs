using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading.Tasks;

namespace WpfApplication2
{
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent();
        }
        /*
        private void src_Box_TextChanged (object sender, TextChangedEventArgs e)
        {
            
            Number Num = new Number();
            Num.Str = src_Box.Text;
            tar_Box.DataContext = Num.Str;
            double dResult;
            if (string.IsNullOrEmpty(Num.Str) == true) { 
                tar_Box.Text = Num.Str;
                return;
            }
            if (!double.TryParse(Num.Str, out dResult)) {
                MessageBox.Show(Num.Str + " 파싱 불가! ");
                tar_Box.Text = "";
                
            } else {
                tar_Box.Text = dResult.ToString("#,##0.##" + "%");
            }
        }
       */
        private bool IsdNumeric (string sSource)
        {
            Regex regex = new Regex("[^0-9.-]");
            return !regex.IsMatch(sSource);
        }
        
        public class Number : INotifyPropertyChanged
        {
            private string sStr;
            public string Str
            {
                get { return sStr; }
                set
                {
                    this.sStr = value;
                    OnPropertyChanged("Str");
                }
            }

            private void OnPropertyChanged (string info)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) {
                    handler(this, new PropertyChangedEventArgs(info));
                    double dValue;
                    if (double.TryParse(info, out dValue) == true) {
                        // 세팅
                    } else {
                        //  null Text
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}