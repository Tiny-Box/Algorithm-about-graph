   M S B A M L   `   `   `  ����
  Graphy    Graphy.MainWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006    5   	   &ignorehttp://www.ignore.com    5   	   $	��600��5   	   $	��900��5	   	     � Title$  Graphy��5
   	    � DataContext 5   	   �� *Main+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Skins/MainSkin.xaml?�6%   %    5      .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5   
   �� -   5         ��Name$ origin��5      $
��Left=�6=   $	��960��6X   $	��720��6e   $��	61,61,0,0q�6q   $	��Top=�6�   ��6    �� *	Originpic+6�   �� 5   
   -   5        $ modify��5      $
��Left=�6=   $	��960��6X   $	��720��6e   $��786.84,61,-614.84,-452q�6q   $	��Top=�6�   ��6    �� *	Modifyimg+6�   o� 5   
   $
��Left=�6   $��22.719��6*   $	��Top=�6:   $��444.628��6R   .@�5      m� $��16(�6   $��SimSun)�6%   $��文件��69   .@�5      m� $��打开��6    m� Command 6'   �� *Open+�� 5      m� 5      $��测试��6    6'   �� *Pop+m� 5      $��退出��6   5      m� 5       $��16(�6   $��SimSun)�6%   $��编辑��69   .@�5!      m� $��生成灰度图��6   .@�5"      m� $��浮点计算法��6    6.   �� *	GetGray+	 m� CommandParameter	 6J   �� *
FileName+m� 5#      $��Bayer抖动法��6    61   �� *Bayer+	 6K   �� *
FileName+m� 5$      $��Floyd-Steinberg抖动法��6    6;   �� *Floyd+	 6U   �� *
FileName+m� 5%      $��灰度统计��6    6-   �� *	GraySum+	 6I   �� *
FileName+m� 5&      $��灰度阈值��6    6-   �� *
GrayThre+	 6J   �� *
FileName+m� 5'      $��反色��6    6+   �� *
LinerTrans+	 6J   �� *
FileName+m� 5(      $��灰度均衡��6    6-   �� *Inte+	 6F   �� *
FileName+5)      �� 5*      m� 5+      5,      5-      5.      5/      