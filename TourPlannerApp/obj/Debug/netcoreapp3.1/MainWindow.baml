   M S B A M L   `   `   `  ����I  ETourPlannerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TourPlannerApp.ViewModelsTourPlannerApp.ViewModels  5   	       TourPlannerApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >
viewmodels'clr-namespace:TourPlannerApp.ViewModels     5   	     � Title$  Tour Planner��5      $	��750��6   $	��800��6&   .��5	      � .+�5
   
   �� 
�������.@�5      �� $
��Home��6   $��15(�6$   .��5      �  � ShowGridLines$ False��6    � ColumnDefinitions 5      �� 5      $��75 �6+   �� 5      $
��10.8 �6+   �� 5      $��184.2 �6+   �� 5      $��1* �6+    � RowDefinitions 5      �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   �� 5      $
��Auto �6(   .+�5      � $��0��6   $��0��6(   $	��300��5      $	��250��5      $��10,10,10,10q�5      $��3��64    5      �� 5      $
��auto �6,   �� 5      $��1* �6,    5       �� 5!      $
��auto �6/   �� 5"      $��1* �6/   .+�5$      �� $��0��6"   $��0��6/   $	��175��6?   $��0,2,0,2q�6K   $
��Left=�6\   ��5%   "   �� *
SearchName+ �� Mode$ TwoWay=�6M   �� 5&      $��1��6!   $��0��61   $��70��6>   $��5,2,0,2q�6I   $��Right=�6Z   ��5'   !   �� *SearchCommand+.��6C   Search6K   �� 5(      $��1��6#   $��0��60   $��2��6@   $	��400��5+   #   ��5)   #   �� *Tours+ �� SelectedItem 5*   #   �� *CurrentTour+��5,      �� 5-   "   .R�5.   &   �� ��60   �� *Name+5/   #   51      52      � 54      $��0��6   $��3��6)   $��10,10,10,10q�69    55      �� 56      $
��Auto �6,   �� 57      $��1* �6,    59      �� 5:      $��1* �6/   �� 5;      $��1* �6/   �� 5<      $��1* �6/   �� 5=      $��1* �6/   .+�5@      �� $��0��6$   $��0��61   ��6A   �� *CurrentTour.Name+6e   �� 5A      $��0��6!   $��3��6.   $	��110��6>   $��25��6J   $��2q�6V   ��5B   !   �� *EditTourCommand+.��6F   Create Tour5D      �� 5E      $��0��6!   $��2��6.   $	��110��6>   $��25��6J   $��2q�6V   ��5F   !   �� *DeleteTourCommand+.��6H   Delete Tour5G      �� 5H      $��0��6!   $��1��6.   $	��110��6>   $��25��6J   $��2q�6V   ��5I   1   �� *GenerateReportCommand+.��6[   Create Report5J      �� 5K      $��1��6%   $��0��62   $��5��6B   .@�5L      �� $��Route��6&   .��5M   "   �� ��6(   �� *CurrentRouteImgPath+$ TwoWay=�5N      �� 5O      $��Description��6&   .��5P   "   �� ��6,   �� *CurrentTour.Description+6W   5Q      �� 5R      $��Distance��6&   .��5S   "   �� ��6,   �� *CurrentTour.Distance+6T   5T      5V      5W      �� 5X      $��1��6   $��10,10,184,10q�6*   $��3��6@   ��5Y      �� *DeleteLogCommand+.��6B   
Delete Log6N   �� 5Z      
�������$��2��68   $��0��6E   $��4��6U   $��10,10,10,10q�6i   .@�5[      �� $
��Logs��6"   .��5\      &   System.Windows.Controls.DataGrid  -   5        $��1��5\   '   $��2��64   ��5]   '   �� *Logs+ 6D   �� *
CurrentLog+5^      5_      �� 5`      $��
Update log��6"   .��5a      �  5b   "   �� 5c   &   $	��5.6 �67   �� 5d   &   $
��14.4 �67   �� 5e   &   $��58.063 �67   �� 5f   &   $
��Auto �67   �� 5g   &   $
��Auto �67   �� 5h   &   $
��Auto �67    5j   "   �� 5k   &   $
��auto �64   �� 5l   &   $
��Auto �64   �� 5m   &   $
��Auto �64   �� 5n   &   $
��Auto �64   .+�5p   "   �� $��0��6(   $��2��65   $��10,5,0,5q�6E   $
��Left=�6W   .��6r   date:6y   �� 5q   "   $��0��6*   $��3��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *	LogDate+6�   �� 5r   "   $��0��6(   $��4��65   $��28,5,0,5q�6E   $
��Left=�6W   .��6r   	report:6{   �� 5s   "   $��0��6*   $��5��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *	LogReport+6�   �� 5t   "   $��1��6(   $��0��65   $��5,5,0,5q�6E   $
��Left=�6V   $��3��6q   .��6�   	distance:6�   �� 5u   "   $��1��6*   $��3��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *LogDistance+6�   �� 5v   "   $��1��6(   $��4��65   $��0,5,0,5q�6E   $��Center=�6V   .��6s   total time:6�   �� 5w   "   $��1��6*   $��5��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *LogTotalTime+6�   �� 5x   "   $��2��6(   $��2��65   $��1,5,0,5q�6E   $
��Left=�6V   .��6q   	rating:6z   �� 5y   "   $��2��6*   $��3��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *	LogRating+6�   �� 5z   "   $��3��6)   $��5��66   $	��100��6F   $��30��6U   $��5,0,0,0q�5{   N   ��6)   �� *UpdateLogCommand+.��6_   Update6g   5|      5}      �� 5~      $��
Create log��6"   .��5      �  5�   "   �� 5�   &   $	��5.6 �67   �� 5�   &   $
��14.4 �67   �� 5�   &   $��58.063 �67   �� 5�   &   $
��Auto �67   �� 5�   &   $
��Auto �67   �� 5�   &   $
��Auto �67    5�   "   �� 5�   &   $
��auto �64   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64   .+�5�   "   �� $��0��6(   $��2��65   $��10,5,0,5q�6E   $
��Left=�6W   .��6r   date:6y   �� 5�   "   $��0��6*   $��3��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *	LogDate+6�   �� 5�   "   $��0��6(   $��4��65   $��28,5,0,5q�6E   $
��Left=�6W   .��6r   	report:6{   �� 5�   "   $��0��6*   $��5��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *	LogReport+6�   �� 5�   "   $��1��6(   $��0��65   $��5,5,0,5q�6E   $
��Left=�6V   $��3��6q   .��6�   	distance:6�   �� 5�   "   $��1��6*   $��3��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *LogDistance+6�   �� 5�   "   $��1��6(   $��4��65   $��0,5,0,5q�6E   $��Center=�6V   .��6s   total time:6�   �� 5�   "   $��1��6*   $��5��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *LogTotalTime+6�   �� 5�   "   $��2��6(   $��2��65   $��1,5,0,5q�6E   $
��Left=�6V   .��6q   	rating:6z   �� 5�   "   $��2��6*   $��3��67   $	��200��6G   $��5,5,5,5q�6V   ��6g   �� *	LogRating+6�   �� 5�   "   $��3��6)   $��5��66   $	��100��6F   $��30��6U   $��5,0,0,0q�5�   K   ��6)   �� *AddLogCommand+.��6\   Save6b   5�      5�      5�      5�      5�      �� 5�      $��Update Tour��6   $��15(�6+   $
��10,5q�69   .��5�      � $
��10,3q�6    5�      �� 5�      $
��Auto �6+   �� 5�      $
��auto �6+    5�      �� 5�      $
��auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6   $��0��6)   $��10q�69   .��6E   
Tour Name:6Q   �� 5�      $��0��6    $��1��6-   $��10q�6=   ��6I   �� *CurrentTour.Name+6m   �� 5�      $��1��6   $��0��6)   $��Right=�69   $	��100��6U   $��5q�6a   .��6l   Name:6s   �� 5�      $��1��6   $��1��6+   $	��300��6;   $
��Left=�6G   $��5q�6b   ��6m   �� *
TourName+6�   �� 5�      $��3��6   $��0��6)   $��Right=�69   $	��100��6U   $��5q�6a   .��6l   description:6z   �� 5�      $��3��6   $��1��6+   $	��300��6;   $
��Left=�6G   $��5q�6b   ��6m   �� *TourDescription+6�   �� 5�      $��4��6   $��0��6)   $��Right=�69   $	��100��6U   $��5q�6a   .��6l   	Distance:6w   �� 5�      $��4��6   $��1��6+   $	��300��6;   $
��Left=�6G   $��5q�6b   ��6m   �� *TourDistance+6�   �� 5�      $��5��6   $��0��6*   $��10q�6:   $��5q�6F   ��5�      �� *UpdateTourCommand+.��6C   Update6K   5�      5�      5�      5�      5�      