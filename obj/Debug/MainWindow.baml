   M S B A M L   `   `   `  ����>  :RCP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:RCPRCP  5   	       RCP.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:RCP     5   	     � Title$  RCP Pracowników��5   	   $	��400��6!   $	��600��6.    � 	Resources 5	        RCP.MainWindowViewModel   dane��%   &	          5
   
   6$   5      .��5      �  � DataContext# [  6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��10 �6   .+�5   
   � $��1��6   $��	0,10,0,10q�6   $��0��62    5      �� 5      $��* �6#   �� 5      $
��auto �6#   .+�5      �� $��0��6     placeHolderMiddle#��[ 5      
 � Tag$D >Wpisz ścieżkę do folderu lub wskaż za pomocą przycisku...��5       {�5   E   �� *EnabledSciezka+��5!      ��  �� Path$ Sciezka� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5"      $��1��6   $��Wybierz folder ��6%     PrzyciskiNaRaportach#���  5#        MyFocusVisualStyle	 � FocusVisualStyle#	 [ 5$      ��5%      �� $ Wybierz�{�5&      �� *EnabledSciezka+5'      �� 5)      $��Wczytaj��6   $��1��6'   $��1��67   $
��auto��5*      $��Center=�6"   #���  5+      #	 [ 5,      ��5-      �� $ Wczytaj�y�5.      �� *VisibilityStart+&   System.Windows.Controls.DataGrid  51   
   -   5        
  Name$
 dg_wynik��51      $��2��6%   $��1��62   $��	0,10,0,10q�6B     DatagridPodstawowy#��[ 53         AutoGenerateColumns .    CanUserAddRows .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 55   1      ClipboardCopyMode$ IncludeHeader=�6A   ��52      �� $ Siatka�$ OneWay=�$ PropertyChanged=� �� NotifyOnSourceUpdated . {�55      �� *	Enabled+y�56      �� *
Visibility+57      �� 59   
   $��Zamknij aplikację��6   $��1��6-   $��3��6=   $��0,5,0,10q�6J   $	��100��5:      $��Right=�6!   #���  5;      #	 [ 5<      ��5=      �� $ Zamknij���5>      ��  �� RelativeSource ��  �� AncestorType" �5@      5A      