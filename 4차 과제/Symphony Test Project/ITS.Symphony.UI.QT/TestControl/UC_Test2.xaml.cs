﻿using System;
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

namespace ITS.Symphony.UI.QT.TestControl
{
    /// <summary>
    /// UC_Test2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UC_Test2 : UserControlBase
    {
        public UC_Test2()
        {
            InitializeComponent();
            this.xdgUser.Initialize();
            CallUserSvc();
        }
        private void UserFilter_Click (object sender, RoutedEventArgs e)
        {
            if (this.xdgUser.FieldSettings.AllowRecordFiltering != true) {
                this.xdgUser.FieldSettings.AllowRecordFiltering = true;
            } else {
                this.xdgUser.FieldSettings.AllowRecordFiltering = false;
            }
        }
        public override void Receive (uint svcNum, EntityBase entity)
        { 
            if (svcNum == 6022) {
                this.Dispatcher.Invoke(new SspResponseDelegate(Receive_6022), DispatcherPriority.Normal, entity);
            }
        }
        public override void ReceiveError (uint svcNum, SysReject error)
        {
            // error 처리
        }
        private void Receive_6022 (EntityBase entity)
        {
            if (entity == null) {
                return;
            }

            var msg = entity as UserList;
            this.xdgUser.DataSource = msg.User;
        }
        private void CallUserSvc ()
        {
            var msg = new DummyMsg();
            ScreenLock();
            base.requestClientArea = (new SspAccess()).SendMessage<DummyMsg>(6022, msg, typeof(UserList), this);
        }
    }
    public class UserMsgConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) {
                return null;
            }

            if (parameter.Equals("496")) {
                if (value.Equals("F")) {
                    return "Front";
                } else if (value.Equals("B")) {
                    return "Back";
                } else if (value.Equals("M")) {
                    return "Middle";
                } else if (value.Equals("E")) {
                    return "Etc";
                }
            } else if (parameter.Equals("500")) {
                if(value.Equals("A")){
                    return "Active";
                } else if (value.Equals("D")) {
                    return "Disable";
                }
            }
            return null;
        }
        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
