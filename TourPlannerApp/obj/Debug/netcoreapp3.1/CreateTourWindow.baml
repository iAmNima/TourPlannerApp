   M S B A M L   `   `   `  ����I  ETourPlannerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TourPlannerAppTourPlannerApp  5   	   E'clr-namespace:TourPlannerApp.ViewModelsTourPlannerApp.ViewModels  5   	   %    TourPlannerApp.CreateTourWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:TourPlannerApp     5   	   >
viewmodels'clr-namespace:TourPlannerApp.ViewModels     5   	     � Title$  CreateTourWindow��5	   	   $	��450��6"   $	��800��6/   .��5
      � $��10,30q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6   $��Right=�6-   $	��100��6I   $��5q�6U   .��6`   Name:6g   �� 5   
   $��0��6   $��1��6   $	��300��6/   $
��Left=�6;   $��5q�6V   ��6a   �� *
TourName+6~   �� 5   
   $��1��6   $��0��6   $��Right=�6-   $	��100��6I   $��5q�6U   .��6`   From:6g   �� 5   
   $��1��6   $��1��6   $	��300��6/   $
��Left=�6;   $��5q�6V   ��6a   �� *
TourFrom+6}   �� 5   
   $��2��6   $��0��6   $��Right=�6-   $	��100��6I   $��5q�6U   .��6`   To:6e   �� 5   
   $��2��6   $��1��6   $	��300��6/   $
��Left=�6;   $��5q�6V   ��6a   �� *TourTo+6{   �� 5   
   $��3��6   $��0��6   $��Right=�6-   $	��100��6I   $��5q�6U   .��6`   description:6n   �� 5    
   $��3��6   $��1��6   $	��300��6/   $
��Left=�6;   $��5q�6V   ��6a   �� *TourDescription+6�   �� 5!   
   $��4��6   $��0��6   $��Right=�6-   $	��100��6I   $��5q�6U   .��6`   	Distance:6k   �� 5"   
   $��4��6   $��1��6   $	��300��6/   $
��Left=�6;   $��5q�6V   ��6a   �� *TourDistance+6�   �� 5#   
   $��5��6   $��0��6   $��10q�6.   $��5q�6:   ��5$      �� *AddTourCommand+.��6@   Save6F   5%      5&      