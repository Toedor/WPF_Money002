   M S B A M L   `   `   `  ����C  ?Money002, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Money002Money002  5          Money002.ExpanceReport   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Money002     5        N� Title$  ExpanceReport��5	      .��5      � $��
10,0,10,10q�6    � ColumnDefinitions 5   
   �� 5      $��50��6   $	��150��6-   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridExpenceTable��5         SelectionMode$ Single=�6j      AutoGenerateColumns .  $��Stretch=�6�   $
��Auto��6�   $��1��6�   $��1��6�   $��2��6�   $��2��6�   $
��Auto��6     Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ V��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 64   0  *System.Windows.Controls.DataGridTextColumn  5      $
 Date��6%   $
 Auto 6S   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 63   �� *expanseDate+6S     5      $ Product��6%   $
 Auto 6V   	 66   �� *expanseName+6V     5      $ Count��6%   $
 Auto 6U   	 64   �� *expanseCount+6U     5      $ 
Count Type��6%   $
 Auto 6^   	 69   �� *expanseCountType+6^     5      $	 Sum��6%   $
 Auto 6Q   	 62   �� *
expanseSum+6Q     5      $ Price��6%   $
 Auto 6U   	 64   �� *expansePrice+6U     5       $ Product group��6%   $
 Auto 6]   	 6<   �� *expanceGroup+6]   5"      �� 5#   
   -   5        $��Сформировать��5#      $��1��6(   $
��Left=�68   $��	10,10,0,0q�6S   $��3��6f   $	��Top=�6s   $
��Auto��6�   6�   �� 5$   
   -   5        $��Очистить��5$      $��1��6$   $
��Left=�64   $��
102,10,0,0q�6O   $��3��6c   $	��Top=�6p   $��75��6�   6�   �� 5%   
   $%��Выбранный расход��6   $��3��6-   $��2��6:   $��2,2,2,2q�6J   
���   �
�������.��5&      �� $
��Auto=�6   .��5'      � $��1,1,0,1q�6    5(      �� 5)      $��* �6+   �� 5*      $��2* �6+    5,      �� 5-      $
��Auto �6(   �� 5.      $
��Auto �6(   �� 5/      $
��Auto �6(   �� 50      $
��Auto �6(   �� 51      $
��Auto �6(   .+�53      �� $��	Дата:��6   $��0��6,   $��0��6<   $��Right=�6I   $��10(�6e   �� 54      $(��"Название продукта:��6   $��0��69   $��1��6I   $��Right=�6V   $��10(�6r   �� 55      $��Количество:��6   $��0��62   $��2��6B   $��Right=�6O   $��10(�6k   �� 56      $��Сумма:��6   $��0��6-   $��3��6=   $��Right=�6J   $��10(�6f   �� 57      $'��!Цена за ед. товара:��6   $��0��6:   $��4��6J   $��Right=�6W   $��10(�6s   �� 58      -   5        
 ��Name$
 lDate��58      $��1��6+   $��0��6;   $
��Left=�6H   �� 59      -   5        $
 lProductName��59      $��1��62   $��1��6B   $
��Left=�6O   �� 5:      -   5        $
 lCount��5:      $��1��6,   $��2��6<   $
��Left=�6I   �� 5;      -   5        $

 lSum��5;      $��1��6*   $��3��6:   $
��Left=�6G   �� 5<      -   5        $
 lPrice��5<      $��1��6,   $��4��6<   $
��Left=�6I   5=      5>      5?      �� 5@   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��2��6>   $��0��6R   $��0,3,3,0q�6_   .+�5A      �� -	   5         ��Name$ comboBoxProductList��5A       �� 
IsEditable .  $��Center=�6G   $��0,0,2,0q�6b   $	��140��6s   6�   �� 5B      -
   5        $ comboBoxProductGroupList��5B       .  $��Center=�6L   $��0,0,2,0q�6g   $	��140��6x   (  "System.Windows.Controls.DatePicker  5C      -   5          Name$ datePickerFilter��5C      $��Center=�63   $��0,0,2,0q�6N      FirstDayOfWeek$ Monday=�6_   �� 5D      -   5        $%��Применить фильтр��5D      $��0,0,2,0q�61   $��Center=�6B   $��Center=�6_   $
��Auto��6z   6�   �� 5E      -   5        $#��Сбросить фильтр��5E      $��0,0,2,0q�6/   $��Center=�6@   $
��Auto��6[   6h   5F      �� 5G   
   $ Vertical=�6   $��0��6,   $��1��6<   .+�5H      �� $��Потрачено��6   $��Center=�6(   �� 5I      -   5        $
 lExpancedAll��5I      $��0��6*   $��Center=�66   5J      �� 5K   
   -   5         ��Name$ btnMainWindow��5K      $��На главную��6(   $��0��6=   $��0��6M   $
��Auto��6Z   $
��Auto��6h   $��Stretch=�6u   $��2,2,2,2q�6�   6�   5M      5N      