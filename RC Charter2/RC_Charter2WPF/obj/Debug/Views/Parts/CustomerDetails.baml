   M S B A M L   `   `   `  ����I  ERC_Charter2WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:RC_Charter2WPF.Views.PartsRC_Charter2WPF.Views.Parts  5      0    *RC_Charter2WPF.Views.Parts.CustomerDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:RC_Charter2WPF.Views.Parts     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5      �   � RowDefinitions  5      �� 5      $
��Auto �6   �� 5      .+�5      � $��0��6	   .+�5      �� $��	10,0,10,0q�5      
���   �$��0,0,0,1q�5      ._�5      �� $��4q�5       �� Padding$ 4q�5      $��30(�5      $��Customer Details��5      5      5      � 5      $��1��6	    � DataContext 6   ��  �� Path$ SelectedCustomer� � ColumnDefinitions 5      �� 5      $
��Auto �6   �� 5        5!      �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6   �� 5%      $
��Auto �6   .+�5'      �� $��0��5(      $��0��5)      $��4q�5*      $ 4q�5+      $��Right=�5,      $��Name: ��5-      �� 5.      $��1��5/      $��0��50      $��4q�51      $ 4q�52      $��Right=�53      $��	Address: ��54      �� 55      $��2��56      $��0��57      $��4q�58      $ 4q�59      $��Right=�5:      $��Available Credits: ��5;      �� 5<      $��3��5=      $��0��5>      $��4q�5?      $ 4q�5@      $��Right=�5A      $��Used Credits: ��5B      �� 5D      $��0��5E      $��1��5F      $��4q�5G      $ 4q�5H      $
��Left=�5I      ��5J      �� $
 Name��� 5K      $��1��5L      $��1��5M      $��4q�5N      $ 4q�5O      $
��Left=�5P      ��5Q      �� $ Address��� 5R      $��2��5S      $��1��5T      $��4q�5U      $ 4q�5V      $
��Left=�5W      ��5X      �� $ AvailableCredits� �� StringFormat$ 
{0:#,#.00}���� 5Y      $��3��5Z      $��1��5[      $��4q�5\      $ 4q�5]      $
��Left=�5^      ��5_      �� $ UsedCredits�$ 
{0:#,#.00}��5`      5a      5b      