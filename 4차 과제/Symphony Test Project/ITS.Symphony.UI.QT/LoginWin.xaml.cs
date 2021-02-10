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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.ComponentModel;

using ITS.Symphony.Common;
using ITS.Symphony.Common.Base;
using ITS.Symphony.Common.Data;
using ITS.Symphony.Common.Helper;
using ITS.Symphony.Common.Net;
using ITS.Symphony.Entity;
using ITS.Symphony.UI.MAIN;
using ITS.Symphony.UI.MAIN.G2;

namespace ITS.Symphony.UI.QT
{
    /// <summary>
    /// LoginWin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWin : RibbonWindowBase
    {
        #region Member Value

        private SysLoginOut m_SysLoginOut = null;

        #endregion


        #region Constructor

        public LoginWin()
        {
            InitializeComponent();
        }

        #endregion


        #region Event Handler, Method - SystemData

        protected override void BeginInitializeWindow()
        {
            base.BeginInitializeWindow();

            GetSystemData();
            SetLoginInfo();
        }

        private void ucLoginPane_LoginSuccessed()
        {
            if (!string.IsNullOrEmpty(m_SysLoginOut.SysUserName)) {
                if (Global.SystemData != null) {
                    Global.SystemData.UserID = m_SysLoginOut.SysUserID;
                    Global.SystemData.CompID = m_SysLoginOut.CompId;	// 회사식별자를 Loader에서 인식하기 위해 SystemData에 저장함

                    CustomizationHelperQT.Save<SystemData>(Global.SystemData, typeof(SystemData));
                }

                // 사용자 정보 설정
                // (통신 모듈에서 설정된 Session Key를 유지하기 위해 UserInfoContext 개체를 얻어서 설정)
                UserInfoContext.GetUserInfoContext().SysUserID = m_SysLoginOut.SysUserID;
                UserInfoContext.GetUserInfoContext().SysUserName = m_SysLoginOut.SysUserName;
                UserInfoContext.GetUserInfoContext().SysTeamID = m_SysLoginOut.SysTeamID;
                UserInfoContext.GetUserInfoContext().SysTeamName = m_SysLoginOut.SysTeamName;
                UserInfoContext.GetUserInfoContext().BizDate = m_SysLoginOut.BizDateInfo.BizDate;
                UserInfoContext.GetUserInfoContext().PrevBizDate = m_SysLoginOut.BizDateInfo.PrevBizDate;
                UserInfoContext.GetUserInfoContext().NextBizDate = m_SysLoginOut.BizDateInfo.NextBizDate;
                Global.CompID = m_SysLoginOut.CompId;	// 회사식별자를 솔루션 전역에서 사용하기 위해 지정함

                // 통화정보 목록 설정
                Global.CcyInfoList = new Dictionary<string, CurrencyInfo>();
                foreach (CurrencyInfo ccyInfo in m_SysLoginOut.CcyInfo) {
                    Global.CcyInfoList.Add(ccyInfo.Ccy, ccyInfo);
                }

                // Codename 세팅
                if (m_SysLoginOut.CodeTypeInfo != null) {
                    foreach (CodeTypeInfo codeType in m_SysLoginOut.CodeTypeInfo) {
                        Global.AddCodeNameDic(codeType.CodeSect, codeType.CodeName);
                    }
                }

                // G2Info 세팅
                if (m_SysLoginOut.G2Info != null) {
                    Global.ConfigData = m_SysLoginOut.G2Info;
                }

                // LocalCcy 설정
                if (m_SysLoginOut.G2Info != null && string.IsNullOrEmpty(m_SysLoginOut.G2Info.LocalCcy) == false) {
                    Global.LocalCcy = m_SysLoginOut.G2Info.LocalCcy;
                } else {
                    Global.LocalCcy = "KRW";
                }

                // RibbonMenuDic 설정
                if (m_SysLoginOut.G2Info != null && m_SysLoginOut.G2Info.RibbonMenu != null) {
                    // RibbonMenu : Dictionary<string, List<Tuple<string, string, string>>>
                    List<Tuple<string, string, string>> tabList;
                    foreach (RibbonMenu r in m_SysLoginOut.G2Info.RibbonMenu) {
                        if (string.IsNullOrWhiteSpace(r.AppSect)) {
                            continue;
                        }
                        if (Global.RibbonMenuDic.ContainsKey(r.AppSect)) {
                            tabList = Global.RibbonMenuDic[r.AppSect];
                        } else {
                            tabList = new List<Tuple<string, string, string>>();
                            Global.RibbonMenuDic[r.AppSect] = tabList;
                        }

                        string[] item_list = r.RibbonItemSect.Split(',');
                        foreach (string item in item_list) {
                            tabList.Add(new Tuple<string, string, string>(r.RibbonTabSect, r.RibbonGrpSect, item.Trim()));
                        }
                    }
                }

                // 사용자 권한 목록 설정
                Global.UserAuth = new HashSet<string>();
                foreach (string auth in m_SysLoginOut.SysAuthCode) {
                    Global.UserAuth.Add(auth);
                }

                if (!DesignerProperties.GetIsInDesignMode(this)) {
                    // G2에서는 서버에서 수신한 메뉴 정보 대신 고정 메뉴 항목 사용.
                    Global.SysMenuMsg = new SysMenuMsg[]
					{
					    new SysMenuMsg() {	MenuId = 1,		MenuIsShow = true,	MenuExecCd = "PR",	MenuIsDupInstance = true,	MenuIsEnable = true,	MenuLabel = "Product Control",		ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 2,		MenuIsShow = true,	MenuExecCd = "TR",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Trading",				ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 3,		MenuIsShow = true,	MenuExecCd = "TM",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Trade Monitoring",		ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 4,		MenuIsShow = true,	MenuExecCd = "PA",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Position Analysis",	ParentMenuId = 0	},
						new SysMenuMsg() {	MenuId = 5,		MenuIsShow = true,	MenuExecCd = "RE",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Trade Report",			ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 6,		MenuIsShow = true																																	},
					    new SysMenuMsg() {	MenuId = 7,		MenuIsShow = true,	MenuExecCd = "TC",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Trade Confirmation",	ParentMenuId = 0	},
						new SysMenuMsg() {	MenuId = 8,		MenuIsShow = true,	MenuExecCd = "SC",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Settlement & Closing",	ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 9,		MenuIsShow = true,	MenuExecCd = "JS",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Job Schedule",			ParentMenuId = 0	},
						new SysMenuMsg() {	MenuId = 10,	MenuIsShow = true,	MenuExecCd = "PC",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Parameter Setting",	ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 11,	MenuIsShow = true,	MenuExecCd = "CS",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Curve Setting",		ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 12,	MenuIsShow = true																																	},
					    new SysMenuMsg() {	MenuId = 13,	MenuIsShow = true,	MenuExecCd = "US",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Underlying Setting",	ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 14,	MenuIsShow = true,	MenuExecCd = "MI",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Market Data",			ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 15,	MenuIsShow = true,	MenuExecCd = "DM",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "Document Management",	ParentMenuId = 0	},
					    new SysMenuMsg() {	MenuId = 16,	MenuIsShow = true,	MenuExecCd = "SA",	MenuIsDupInstance = false,	MenuIsEnable = true,	MenuLabel = "System Setting",		ParentMenuId = 0	},
					};
                }

                // 서버 서버 Configuration Parameters 설정
                Global.ServerParam = new ServerParam();
                Global.ServerParam.SetParams(m_SysLoginOut.ConfigCmd);

                if (m_SysLoginOut.G2Info != null) {
                    Global.DeskSect = m_SysLoginOut.G2Info.DeskSect;
                }

                //  계정 사용유효기간 표시
                if (m_SysLoginOut.RemainValidDays != null) {
                    MessageHelper.ShowInfo("CM_RemainValidDays", m_SysLoginOut.RemainValidDays);
                }

                MainWindow win = new MainWindow();
                if ((SystemParameters.PrimaryScreenWidth <= 1280) || (SystemParameters.PrimaryScreenHeight <= 1024)) {
                    win.Width = SystemParameters.WorkArea.Width;
                    win.Height = SystemParameters.WorkArea.Height;
                } else {
                    win.Width = 1280;
                    win.Height = SystemParameters.WorkArea.Height;
                }
                if (win.ShowDialog() == true) {
                } else {
                }
                this.Close();
            }
        }

        private void GetSystemData()
        {
            try {
                Global.SystemData = CustomizationHelperQT.Load<SystemData>(typeof(SystemData));
                if (Global.SystemData == null) {
                    Global.SystemData = GetDefaultSystemData();
                }
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("ITS.Symphony.UI.QT.LoginWin.xaml.cs::GetSystemData: " + ex.Message);

                Global.SystemData = GetDefaultSystemData();
                CustomizationHelperQT.Save<SystemData>(Global.SystemData, typeof(SystemData));
            }
        }

        private SystemData GetDefaultSystemData()
        {
            SystemData systemData = new SystemData {
                ServerIP = "192.1.1.161",
                ServerPort = 5700,
                CompID = "ITS",
                LanguageType = "KOR",
                SkinMode = "G2",
                IsLogoTextVisible = true
            };
            return systemData;
        }

        private void SetLoginInfo()
        {
            if (Global.SystemData == null) {
                return;
            }

            // UserID
            if (string.IsNullOrEmpty(Global.SystemData.UserID) == false) {
                this.tbxID.Text = Global.SystemData.UserID;
            }

            // Setup
            this.DataContext = Global.SystemData;
        }

        #endregion


        #region Event Handler, Method - Login

        private void Login()
        {
            if (string.IsNullOrEmpty(this.tbxID.Text.Trim())) {
                // CM_014: 아이디를 입력해주세요.
                MessageHelper.Show("CM_014");
                return;
            }

            if (string.IsNullOrEmpty(this.pbxPasswd.Password.Trim())) {
                // CM_015: 비밀번호를 입력해주세요.
                MessageHelper.Show("CM_015");
                return;
            }

            RequestLogin(this.tbxID.Text, this.pbxPasswd.Password);
        }

        private void RibbonWindowBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                Login();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void RequestLogin(string id, string pw)
        {
            if (SspAccess.Connect()) {
                // ClientKey얻기. 실패시 '0'으로 패딩된 길이32의 string 리턴.
                string clientKey = FingerPrint.Value();

                // 시스템 로그인
                SysLogin sysLogin = new SysLogin { SysUserID = id, SysPasswd = pw, ClientKey = clientKey, Lang = Global.SystemData.LanguageType };
                base.requestClientArea = (new SspAccess()).SendMessage<SysLogin>(1, sysLogin, typeof(SysLoginOut), this);
            }
        }

        public override void Receive(uint svcNum, EntityBase entity)
        {
            if (svcNum == 1) {	// LoginSvc
                this.Dispatcher.Invoke(new SspResponseDelegate(Receive_0001), DispatcherPriority.Normal, entity);
            }
        }

        public override void ReceiveError(uint svcNum, SysReject error)
        {
            base.ReceiveError(svcNum, error);

            AlertBox.ShowError("로그인 실패\n\n" + error.SysReasonText);
        }

        private void Receive_0001(EntityBase entity)
        {
            SysLoginOut sysLoginOut = entity as SysLoginOut;
            if (sysLoginOut == null) {
                return;
            }

            m_SysLoginOut = sysLoginOut;
            ucLoginPane_LoginSuccessed();
        }

        #endregion


        #region Event Handler - Exit button

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
