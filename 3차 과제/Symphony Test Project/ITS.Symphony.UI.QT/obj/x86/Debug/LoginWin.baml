   M S B A M L   `   `   `  ����N  JITS.Symphony.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null`Cclr-namespace:ITS.Symphony.Common.Base;assembly=ITS.Symphony.CommonITS.Symphony.Common.Base  5      O KITS.Symphony.UI.MAIN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_Cclr-namespace:ITS.Symphony.UI.MAIN.G2;assembly=ITS.Symphony.UI.MAINITS.Symphony.UI.MAIN.G2 5       ITS.Symphony.UI.QT!   ITS.Symphony.UI.QT.LoginWin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      TbaseCclr-namespace:ITS.Symphony.Common.Base;assembly=ITS.Symphony.Common     5      VuiMainCclr-namespace:ITS.Symphony.UI.MAIN.G2;assembly=ITS.Symphony.UI.MAIN     5      -   5          � WindowStartupLocation$  CenterScreen=�5      $	��220��6*   $	��230��66    � Title$ Login��5      .��5
      � -   5         �Name$ gdMain��5
      
������� � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��10 �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��ID��61   $��10,20,10,10q�6;   �� 5   
   -   5         ��Name$ tbxID��5   /   $��0��6   $��2��6   $	��100��6>   $
��Left=�6J   $��
0,20,10,10q�6e   �� 5   
   $��1��6   $��0��6!   $��Password��61   $��	10,0,0,10q�6A   G� 5   
   -   5         G�Name$ 	pbxPasswd��5   3   $��1��6   $��2��6#   $	��100��6F   $
��Left=�6R   $��	0,0,10,10q�6m   �� 5   
   $��2��6   $��0��6!   $��	Server IP��61   $��	10,0,0,10q�6B   �� 5   
   $��2��6   $��2��6   $	��100��6V   $
��Left=�6b   $��	0,0,10,10q�6}   ��6/   �� *
ServerIP+ �� Mode$ TwoWay=�6}   �� 5!   
   $��3��6   $��0��6!   $��Server Port��61   $��	10,0,0,20q�6D   �� 5"   
   $��3��6   $��2��6   $	��100��6X   $
��Left=�6d   $��	0,0,10,20q�6   ��6/   �� *
ServerPort+$ TwoWay=�6   �� 5$   
   $��4��6   $��0��6"   $��3��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $	��Top=�6|   $��0,10,0,5q�6�   .+�5%      �� -   5        	 ��Name$	 btnLogin��5%         LoginBtnStyle2#��[  6'   6O   �� 5&      -   5        $	 btnExit��5&        ExitBtnStyle2#��[ 6&   $��5,0,4,0q�6c   5'      5(      5)      