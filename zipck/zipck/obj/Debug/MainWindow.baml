   M S B A M L   `   `   `  ����	  zipck    zipck.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  mainFrm��5   	    � Title$! Mass File Extraction Wizard��5   	   $	��308��6-   $	��539��6:   $��UltraCondensed&�6F    � 
ResizeMode$ NoResize=�6{    � WindowStartupLocation$ CenterScreen=�6�    � Cursor$ Arrow��6�   $
��Left=�6�   $	��Top=�6�   .��5      � $��	0,0,2,-21q�6    � ColumnDefinitions 5   
   �� 5      $
��254* �6   �� 5      $
��120* �6   �� 5	      $
��154* �6   �� 5
      $��3* �6   .+�5   
   �� -   5         ��Name$ zipLbl��5      $��Files to be Extracted:��6    $
��Left=�6A   $	��Top=�6\   $��10,0,0,0q�6t   �� 5   
   -   5         ��Name$ AddFileButton��5      $	��Add��6(   $
��Left=�66   $	��Top=�6Q   $��86��6i   $��	37,40,0,0q�6t   $��20��6�   $��2��6�   �� 5   
   -   5        $ extractButton��5      $��Extract��6(   $
��Left=�6:   $��
37,226,0,0q�6U   $	��Top=�6i   $	��109��6�   $��31��6�    J� RenderTransformOrigin$ 0.688,1.484&�6�   $��2��6�   �� 5   
   -   5        $ completedLbl��5      $�� ��6&   $
��Left=�61   $��
15,105,0,0q�6L   $	��Top=�6`   $ 0.5,0.5&�6x   z�5      V� 5      .~�5      �� �� 5      �� 5      	 �� Angle$
	 0.47Q�6&   T� 5      5      5      �� 5   
   -   5        
 ��Name$
 filePathsListBox��5      $
��Left=�6,   $	��226��6G   $	��Top=�6T   $	��386��6l   $��	10,31,0,0q�6x   $ 	0.35,0.45&�6�   $��3��6�   �� 5   
   -   5        $ RemoveButton��5      $��Remove��6'   $
��Left=�68   $	��Top=�6S   $��86��6k   $��	37,65,0,0q�6v   $��21��6�   $��2��6�   6�   �� 5   
   -   5        $ optionsButton��5      $��Options��6(   $
��Left=�6:   $��
38,188,0,0q�6U   $	��Top=�6i   $	��109��6�   $��31��6�   $��2��6�   6�   �� 5   
   -	   5        $ ClearButton��5      $��Clear��6&   $
��Left=�66   $	��Top=�6Q   $��86��6i   $��	37,91,0,0q�6t   $��21��6�   $��2��6�   6�   5      5      