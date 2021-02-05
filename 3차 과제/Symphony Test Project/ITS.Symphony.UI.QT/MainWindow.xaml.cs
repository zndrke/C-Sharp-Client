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

using ITS.Symphony.Common;
using ITS.Symphony.Common.Base;
using ITS.Symphony.Common.Data;
using ITS.Symphony.Common.Helper;
using ITS.Symphony.Common.Net;
using ITS.Symphony.Entity;
using ITS.Symphony.UI.QT.TestControl;

using Infragistics.Windows.Ribbon;

namespace ITS.Symphony.UI.QT
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : RibbonWindowBase
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion


        #region Event Handler - Application Menu

        private void AppMenu_Click(object sender, RoutedEventArgs e)
        {
            this.mtcMain.BlindScreen(true);

            ButtonTool btl = sender as ButtonTool;
            if (btl == null || btl.Tag == null) {
                return;
            }

            switch (btl.Tag.ToString()) {
                case "CloseAll":
                    // CM_008: 모든 탭을 닫으시겠습니까?
                    if (MessageHelper.Show("CM_008", null, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                        this.mtcMain.TabControl.Items.Clear();
                    }
                    break;
                case "Exit":
                    this.Close();
                    break;
                case "Logout":
                    Global.MainTaskBar.ExitApp();
                    break;
            }

            this.mtcMain.BlindScreen(false);
        }

        private void btlFootToolbar_Click(object sender, RoutedEventArgs e)
        {
            this.appMenu.IsOpen = false;
        }

        #endregion


        #region Event Handler - Ribbon Menu

        private void TestMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonTool btl = sender as ButtonTool;
            if (btl == null) {
                return;
            }
            if (btl.Tag == null) {
                return;
            }

            switch (btl.Tag.ToString()) {
                case "Test1":
                    this.mtcMain.CreateTab(new UC_Test1(), "Test1", "Test1", "Test1", "Test1");
                    break;
                case "Test2":
                    this.mtcMain.CreateTab(new UC_Test2(), "Test2", "Test2", "Test2", "Test2");
                    break;
            }
        }

        #endregion


        #region Event Handler - RibbonWindow Closed (Logout)
        
        private void RibbonWindowBase_Closed(object sender, EventArgs e)        
        {
            try {
                CustomizationHelperQT.Save<SystemData>(Global.SystemData, typeof(SystemData));

                // Logout 처리
                SysLogout inMsg = new SysLogout() {
                    SysUserID = Global.SystemData.UserID
                };
                new SspAccess().SendMessage<SysLogout>(3, inMsg, typeof(SysLogoutOut), this);
                SspAccess.Disconnect();
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("ITS.Symphony.UI.QT.MainWindow.xaml.cs::RibbonWindowBase_Closed: " + ex.Message);
            }
        }

        #endregion
    }
}