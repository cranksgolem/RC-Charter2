   M S B A M L   `   `   `  ����I  ERC_Charter2WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:RC_Charter2WPF.Views.PartsRC_Charter2WPF.Views.Parts  5      -    'RC_Charter2WPF.Views.Parts.EmployeeList   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      4local(clr-namespace:RC_Charter2WPF.Views.Parts  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      .��5      �   � RowDefinitions  5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��8* �6   �� 5      $
��Auto �6   .+�5      � $��0��6	   .+�5      �� $��	10,0,10,0q�5      
���   �$��0,0,0,1q�5      ._�5      �� $��4q�5       �� Padding$ 4q�5      $��20(�5      $��Select Crew Members: ��5      5      5      � 5      $��1��6	    � ColumnDefinitions 5       �� 5!      $��* �6   .+�5#      �� -   5         ��Name$ TbxEmployeeSearch��5$      $��0��5%      $	��300��5&      $��38��5'      $��4q�5(      $��4q�5)      $
��Left=�5*      ��5-      ��  �� Path$ EmployeeSearchText� �� UpdateSourceTrigger$ PropertyChanged=� �� Delay$	 300���� 5.      -   5         ��Name$ TblSearchLabel��5/      $��0��50      $��4,20,4,0q�51      $ 4,0,4,0q�52      $��Italic$�53      
������� J� IsHitTestVisible .  $��Search Employees��56      57      �� 58      -   5        	 ��Name$	 LbxEmployees��59      $��2��5:      $	��150��5;      $��4,0,4,4q�5<      $��4q�5=      ��5>      �� $ EmployeeList�
 �� SelectedItem
 5?      �� $ SelectedEmployee���5@      �� 5A      .R�5B      �� $	 0,3q�6   ��6   �� $
 Name�5C       �� 
GroupStyle 5E      (  "System.Windows.Controls.GroupStyle  5F         ContainerStyle 5G      �� 5H       �� 
TargetType" ��6   .�5I   	   ��  �� Property � �� �� Value 5J   
   �� 5K       �� 
TargetType" ��6   .R�5L      9�  9� 
IsExpanded . ��6   �� $
 Name�.��5M      �� $��10,0,0,0q�6   5N      5O      5Q   
   5R   	   5T      .  (System.Windows.Interactivity.Interaction   Triggers 5V      /  )System.Windows.Interactivity.EventTrigger  5W         	EventName$ MouseDoubleClick��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5X      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6   �� $7 1AddSelectedEmployeeToEmployeesToBeAssignedCommand�5Y      5[      � 5\      $��3��6	    5]      �� 5^      $��2* �6   �� 5_      $��* �6   �� 5`      $��2* �6   .+�5b      �� $��0��5c      $	��8,5q�5d      $��Right=�5e      $��Center=�5f      
������ $��0q�5h      
������ ��5i      �� $ PrevPageEmployeeCommand�.��5k      (� $��20��6   $��20��6   .��5l      �� $��24��6   $��24��6   .+�5m      F� a���� -1�ا � -1@]� � z @]� �`�� �IрS� ��/� z?  � рS� �A> �`�� `�S � z �ا � -1�ا 	
���   �6[   5n      5o      5p      2   ,RC_Charter2WPF.Views.Parts.ListBoxItemScroll  5q      $��1��5r      $	��8,0q�5s      $��Center=�5t      $��Center=�5u      $��0q�5v      $��Hidden=�5x      ��5w      �� $ Pages�
 5y      �� $ SelectedEmployeePage� �� Mode$ TwoWay=���5z      �� 5{      .R�5|      �  � Orientation$ 
Horizontal=�6   5}      5      �� 5�      $��2��5�      $	��8,5q�5�      $
��Left=�5�      $��Center=�5�      
������ 
������ $��0q�5�      ��5�      �� $ NextPageEmployeeCommand�.��5�      (� $��20��6   $��20��6   .��5�      �� $��24��6   $��24��6   .+�5�      F� a���� 	= �ا � 	= @]� � $� @]� Ѡ7� �Iр� ��/� �. � р� �A> Ѡ7� `�S � $� �ا � 	= �ا 	
���   �6\   5�      5�      5�      5�      5�      5�      