   M S B A M L   `   `   `  ����I  ERC_Charter2WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:RC_Charter2WPF.Views.PartsRC_Charter2WPF.Views.Parts  5      .    (RC_Charter2WPF.Views.Parts.FlightDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:RC_Charter2WPF.Views.Parts     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .��5      �   � RowDefinitions  5      �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5      � $��0��6	   .+�5      �� 
���   �$��0,0,0,1q�6    ._�5      �� $��4q�5       �� Padding$ 4q�5      $��30(�5      $��Flight Leg Details��5      5      5      � 5      -   5         �Name$ GridFlightLegDetails��5      $��1��5      $y�	Collapsed=�5       � DataContext 5      ��  �� Path$ SelectedFlight� � ColumnDefinitions 5       �� 5!      $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   �� 5$      $
��Auto �6     5&      �� 5'      $
��Auto �6   �� 5(      $
��Auto �6   �� 5)      $
��Auto �6   �� 5*      $
��Auto �6   �� 5+      $
��Auto �6   .+�5-      �� $��0��5.      $��0��5/      $��4,4,4,10q�50      $ 4q�51      $
��Bold"�52      $��Date:��53      �� 54      $��1��55      $��0��56      $��4,4,4,10q�57      $ 4q�58      $
��Bold"�59      $��Time Departure:��5:      �� 5;      $��2��5<      $��0��5=      $��4,4,4,10q�5>      $ 4q�5?      $
��Bold"�5@      $��Time Arrival:��5A      �� 5B      $��3��5C      $��0��5D      $��4,4,4,10q�5E      $ 4q�5F      $
��Bold"�5G      $��Origin:��5H      �� 5I      $��4��5J      $��0��5K      $��4,4,4,10q�5L      $ 4q�5M      $
��Bold"�5N      $��Destination:��5O      �� 5P      $��0��5Q      $��2��5R      $��4,4,4,10q�5S      $ 4q�5T      $
��Bold"�5U      $��Distance Flown:��5V      �� 5W      $��1��5X      $��2��5Y      $��4,4,4,10q�5Z      $ 4q�5[      $
��Bold"�5\      $��Waiting Time:��5]      �� 5^      $��2��5_      $��2��5`      $��4,4,4,10q�5a      $ 4q�5b      $
��Bold"�5c      $��Fuel Usage:��5d      �� 5e      $��3��5f      $��2��5g      $��4,4,4,10q�5h      $ 4q�5i      $
��Bold"�5j      $��Order:��5k      �� 5m      $��0��5n      $��1��5o      $��4q�5p      $ 4q�5q      $
��Left=�5r      ��5s      �� $
 Date� �� StringFormat$ {0:MM/dd/yyyy}���� 5t      $��1��5u      $��1��5v      $��4q�5w      $ 4q�5x      $
��Left=�5y      ��5z      �� $ TimeDeparture�$ t���� 5{      $��2��5|      $��1��5}      $��4q�5~      $ 4q�5      $
��Left=�5�      ��5�      �� $ TimeArrival�$ t���� 5�      $��3��5�      $��1��5�      $��4q�5�      $ 4q�5�      $
��Left=�5�      ��5�      �� $ Origin��� 5�      $��4��5�      $��1��5�      $��4q�5�      $ 4q�5�      $
��Left=�5�      ��5�      �� $ Destination��� 5�      $��0��5�      $��3��5�      $��4q�5�      $ 4q�5�      $
��Left=�5�      ��5�      �� $ DistanceFlown��� 5�      $��1��5�      $��3��5�      $��4q�5�      $ 4q�5�      $
��Left=�5�      ��5�      �� $ WaitingTime��� 5�      $��2��5�      $��3��5�      $��4q�5�      $ 4q�5�      $
��Left=�5�      ��5�      �� $ 	FuelUsage��� 5�      $��3��5�      $��3��5�      $��4q�5�      $ 4q�5�      $
��Left=�5�      ��5�      �� $ Order�5�      � 5�      $��2��6	   $
��Left=�6   .+�5�      �� -   5         ��Name$ BtnBack��5�      $��4q�5�      $��4q�5�      $
��Back��5�      ��5�      �� $ LoadCharterTripsCommand�5�      5�      5�      5�      