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
using System.Text.RegularExpressions;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.TxtBx.PreviewTextInput += textBox_PreviewTextInput;
        }

        private double[] Parse_TextBox (string p_sText,out string p_sNotNum)
        {
            string[] saArr;                 
            string sStr = "";               
            char[] cSep = { '\n', '\t', ' ' };
            double dNum = 0;
            bool bCanConvert = false;
            int i = 0;
            p_sNotNum = "";
            sStr = p_sText.Trim();
            saArr = sStr.Split(cSep, StringSplitOptions.RemoveEmptyEntries);
            double[] dArr = new double[saArr.Length];
            
            foreach (var item in saArr) {
                bCanConvert = double.TryParse(item, out dNum);
                if (bCanConvert) {
                    dArr[i++] = dNum;
                } else {
                    p_sNotNum = item; 
                    return new double[saArr.Length];
                }
            }
            return dArr;
        }

        private void Min_Button_Click(object sender, RoutedEventArgs e)
        {
            double dMin = 0;
            double[] dArr;
            string sNotNum;
            dArr = Parse_TextBox(TxtBx.Text, out sNotNum);

            if (sNotNum == "") {
                dMin = dArr[0];
                foreach (var item in dArr) {
                    if (dMin > item) {
                        dMin = item;
                    }
                }
                MessageBox.Show(Convert.ToString(dMin));
            } else {
                MessageBox.Show(Convert.ToString(sNotNum) + "는 숫자가 아닙니다");
            }
        }
        private void Max_Button_Click (object sender, RoutedEventArgs e)
        {
            double dMax = 0;
            double[] dArr;
            string sNotNum;
            dArr = Parse_TextBox(TxtBx.Text, out sNotNum);

            if (sNotNum == "") {
                dMax = dArr[0];
                foreach (var item in dArr) {
                    if (dMax < item) {
                        dMax = item;
                    }
                }
                MessageBox.Show(Convert.ToString(dMax));
            } else {
                MessageBox.Show(Convert.ToString(sNotNum) + "는 숫자가 아닙니다");
            }
        }
        private void Sum_Button_Click (object sender, RoutedEventArgs e)
        {
            double dSum = 0;
            double[] dArr;
            string sNotNum = "";
            dArr = Parse_TextBox(TxtBx.Text, out sNotNum);

            if (sNotNum == "") {
                foreach (var item in dArr) {
                    dSum += item;
                }
                MessageBox.Show(Convert.ToString(dSum));
            } else {
                MessageBox.Show(Convert.ToString(sNotNum) + "는 숫자가 아닙니다");
            }
        }
        private void Order_Button_Click (object sender, RoutedEventArgs e)
        {
            double[] dArr;
            string sNotNum;
            dArr = Parse_TextBox(TxtBx.Text, out sNotNum);
            if (sNotNum =="") {
                Array.Sort(dArr);
                TxtBx.Clear();
                foreach (var item in dArr) {
                    TxtBx.Text += item;
                    TxtBx.Text += ' ';
                }
                MessageBox.Show("정렬되었습니다!");
            } else {
                MessageBox.Show(Convert.ToString(sNotNum) + "는 숫자가 아닙니다");
            }
        }

        private void textBox_PreviewTextInput (object sender, TextCompositionEventArgs e)
        {
            if (!IsdNumeric(e.Text)) {
                MessageBox.Show(" 숫자만 입력하세요!");
                e.Handled = !IsdNumeric(e.Text);         //e.handled = true
            }
        }
        private bool IsdNumeric (string sSource)
        {
            Regex regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(sSource);
        }
    }
}
