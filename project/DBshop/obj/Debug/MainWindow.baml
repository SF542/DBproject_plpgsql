   M S B A M L   `   `   `  ����A  =DBshop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DBshopDBshop  5   	       DBshop.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:DBshop     5   	   -   5          � Title$   ��5   	   $	��450��6   $	��820��6    � WindowStartupLocation$ CenterScreen=�6D    � 
ResizeMode$ NoResize=�6i   .��5	      � .+�5
   
   �� -   5        $��Login��5
      $
��Left=�6!   $��
621,10,0,0q�6<   $	��Top=�6P   $��16(�6h   6v   �� 5   
   -   5        $��Sign up��5      $
��Left=�6#   $��
689,10,0,0q�6>   $	��Top=�6R   $��16(�6j   6x   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DG��5      $��Center=�6   $	��288��6<   $��0,55,0,0q�6I   $	��Top=�6[   $	��692��6s      AutoGenerateColumns .    CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     CanUserResizeColumns .  	   CanUserResizeRows	 .  
   Columns
 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      -   5        	BuyColumn��,  &System.Windows.Controls.DataGridColumn   Header$	 Buy��5   <      CellTemplate 5      �� 5      .R�5      �� -   5        $��Order��5   %   65   5      5      5      �� 5   
   -   5         ��Name$ custname��5      $�� ��6"   $
��Left=�6-   $��	54,17,0,0q�6H   $	��Top=�6[   $��18(�6s   $
��Bold"�6�   $y�	Collapsed=�6�   �� 5   
   -   5         ��Name$ orderButton��5      $��
MAKE ORDER��6&   $
��Left=�6;   $��
494,10,0,0q�6V   $	��Top=�6j   $��16(�6�   $y�	Collapsed=�6�   �� 5   
   -	   5        $	 Add��5      $	��Add��6   $
��Left=�6,   $��
402,10,0,0q�6G   $	��Top=�6[   $��16(�6s   $y�	Collapsed=�6�   �� 5   
   -
   5        $	 Del��5      $��Delete��6   $
��Left=�6/   $��703,361,0,0q�6J   $	��Top=�6_   $��16(�6w   $y�	Collapsed=�6�   6�   �� 5   
   -   5         ��Name$ delBox��5      $
��Left=�6"   $��621,365,0,0q�6=    �� TextWrapping$
 Wrap=�6R   $	��Top=�6f   $��61��6~   $y�	Collapsed=�6�   �� 5   
   -   5         ��Name$ Kolvo��5      $
��Left=�6#   $��
495,35,0,0q�6>   $
��Wrap=�6R   $	��Top=�6f   $��49��6~   $y�	Collapsed=�6�   �� 5   
   -   5        $ Summa��5      $
��Left=�6#   $��
555,35,0,0q�6>   $
��Wrap=�6R   $	��Top=�6f   $��40��6~   $y�	Collapsed=�6�   �� 5   
   -   5        $	 Out��5      $��Output file��6   $
��Left=�64   $��515,361,0,0q�6O   $	��Top=�6d   $��16(�6|   $y�	Collapsed=�6�   6�   �� 5   
   -   5        $ outBox��5      $
��Left=�6"   $��433,365,0,0q�6=   $
 Wrap=�6R   $	��Top=�6f   $��61��6~   $y�	Collapsed=�6�   �� 5    
   -   5        $ leftBut��5       $��<��6"   $
��Left=�61   $��
751,55,0,0q�6L   $	��Top=�6`   $��27��6x   $y�	Collapsed=�6�   6�   �� 5!   
   -   5        $ rightBut��5!      $��>��6#   $
��Left=�62   $��
778,55,0,0q�6M   $	��Top=�6a   $��27��6y   $y�	Collapsed=�6�   6�   5"      5#      