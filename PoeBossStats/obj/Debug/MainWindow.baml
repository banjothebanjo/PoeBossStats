   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   G CPoeBossStats, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PoeBossStatsPoeBossStats 5   	      PoeBossStats.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   ,localclr-namespace:PoeBossStats     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)    � 
ResizeMode$ NoResize=�65   .��5
      � .+�5   
   4   .MahApps.Metro.Controls.MetroAnimatedTabControl  .@�5      �� $��	BossKills��6   .��5        .@�5      �� $��Shaper��6   .��5      � .+�5      �� -   5        $��ShaperListView��5   =   $��10,123,10,10q�6'    �� View 5   "   �� 5   &   .P�5   *   �� $��	Character��69    �� Width$
 auto��6L    �� DisplayMemberBinding 6Y   �� *	Character+�� 5   *   $��
Ascendancy��69    6M   �� *
Ascendancy+�� 5   *   $��Level��69    6H   �� *Level+�� 5   *   $��League��69    6I   �� *League+�� 5   *   $
��Boss��69    6G   �� *
BossType+�� 5   *   $��Entered��69    6J   �� *InstanceEntered+�� 5   *   $��Killed��69    6I   �� *	Created+�� 5   *   $��Items��69    6H   �� *Items+5   '   5      5      5      �� 5       $��Elder��6   .��5!      �� -   5        $��ElderListView��5!   9   $��10,123,10,10q�6#    5"      �� 5#   "   .P�5$   &   �� $��	Character��65   $
 auto��6H    6U   �� *	Character+�� 5%   &   $��
Ascendancy��65    6I   �� *
Ascendancy+�� 5&   &   $��Level��65    6D   �� *Level+�� 5'   &   $��League��65    6E   �� *League+�� 5(   &   $
��Boss��65    6C   �� *
BossType+�� 5)   &   $��Entered��65    6F   �� *InstanceEntered+�� 5*   &   $��Killed��65    6E   �� *	Created+�� 5+   &   $��Items��65    6D   �� *Items+5,   #   5.      5/      �� 50      $��Sirus��6   .��51      � 52      53      54      55      �� 58      $��Debug��6   .��59      � .+�5:      �� -   5        $��instance_IP��5:       $
��Left=�63   $��	17,75,0,0q�6N   $
��Wrap=�6a   $�� ��6u   $	��Top=�6}   �� 5;      -   5         ��Name$ Mapname��5;       $
��Left=�61   $��	17,38,0,0q�6L   $
��Wrap=�6_   $�� ��6s   $	��Top=�6{   �� 5<      -   5        $ 
IsBossRoom��5<       $
��Left=�64   $��
17,112,0,0q�6O   $
��Wrap=�6c   $�� ��6w   $	��Top=�6   �� 5=      -   5        $ TimeEntered��5=       $
��Left=�65   $��
17,149,0,0q�6P   $
��Wrap=�6d   $�� ��6x   $	��Top=�6�   �� 5>      -   5         ��Name$ EntryInv��5>      $��10,197,557,70q�61    5?      �� 5@      .P�5A   "   �� $
��Name��61    6?   �� *name+�� 5B   "   $��BaseType��61    6C   �� *
baseType+5C      5E      �� 5F      $
��Left=�6    $��	17,22,0,0q�6;   $
��Wrap=�6N   $��Map Name��6b   $	��Top=�6r   $
��Bold"�6�   �� 5G      $
��Left=�6    $��	17,59,0,0q�6;   $
��Wrap=�6N   $��Instance IP��6b   $	��Top=�6u   $
��Bold"�6�   �� 5H      $
��Left=�6    $��	17,96,0,0q�6;   $
��Wrap=�6N   $��Is Boss Map��6b   $	��Top=�6u   $
��Bold"�6�   �� 5I      $
��Left=�6    $��
17,133,0,0q�6;   $
��Wrap=�6O   $��Time Entered��6c   $	��Top=�6w   $
��Bold"�6�   �� 5J      $
��Left=�6    $��
10,176,0,0q�6;   $
��Wrap=�6O   $��Entry Inventory��6c   $	��Top=�6z   $
��Bold"�6�   �� 5K      -   5        $ ExitInv��5K      $��265,197,269,70q�60    5L      �� 5M      .P�5N   "   �� $
��Name��61    6?   �� *name+�� 5O   "   $��BaseType��61    6C   �� *
baseType+5P      5R      �� 5S      -	   5        $ FoundInv��5S      $��556,197,11,70q�61    5T      �� 5U      .P�5V   "   �� $
��Name��61    6?   �� *name+�� 5W   "   $��BaseType��61    6C   �� *
baseType+5X      5Z      �� 5[      $
��Left=�6    $��265,176,0,0q�6;   $
��Wrap=�6P   $��Exit Inventory��6d   $	��Top=�6z   $
��Bold"�6�   �� 5\      $
��Left=�6    $��556,176,0,0q�6;   $
��Wrap=�6P   $��Found Items��6d   $	��Top=�6w   $
��Bold"�6�   �� 5]      $
��Left=�6    $��
212,25,0,0q�6;   $
��Wrap=�6O   $��Boss ��6c   $	��Top=�6p   $
��Bold"�6�   �� 5^      -
   5        $��bossTypeName��5^       $
��Left=�64   $��
212,41,0,0q�6O   $
��Wrap=�6c   $�� ��6w   $	��Top=�6   �� 5_      -   5        $��	Character��5_       $
��Left=�61   $��
441,28,0,0q�6L   $
��Wrap=�6`   $�� ��6t   $	��Top=�6|   �� 5`      -   5        $��
Ascendancy��5`       $
��Left=�62   $��
441,54,0,0q�6M   $
��Wrap=�6a   $�� ��6u   $	��Top=�6}   �� 5a      -   5        $��Level��5a       $
��Left=�6-   $��
441,75,0,0q�6H   $
��Wrap=�6\   $�� ��6p   $	��Top=�6x   �� 5b      -   5        $��League��5b       $
��Left=�6.   $��
441,99,0,0q�6I   $
��Wrap=�6]   $�� ��6q   $	��Top=�6y   5c      5d      5e      5f      5g      