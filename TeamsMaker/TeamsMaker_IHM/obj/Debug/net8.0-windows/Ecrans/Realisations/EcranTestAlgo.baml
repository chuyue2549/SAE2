   M S B A M L   `   `   `  ����I  ETeamsMaker_IHM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:TeamsMaker_IHM.EcransTeamsMaker_IHM.Ecrans  5      W0clr-namespace:TeamsMaker_IHM.Ecrans.Realisations"TeamsMaker_IHM.Ecrans.Realisations  5      6    0TeamsMaker_IHM.Ecrans.Realisations.EcranTestAlgo   5      .ecrans#clr-namespace:TeamsMaker_IHM.Ecrans  W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Blocal0clr-namespace:TeamsMaker_IHM.Ecrans.Realisations     5      .��5
      (� .��5   
   � $	��900��6   $
��1600��6   ���#f6f6f6  � ColumnDefinitions  5      �� 5      $��3* �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ ZoneAffichage��5      .��5      �� .+�5      �� -   5         ��Name$ StackEquipes��5   !   5      � 5      -   5         �Name$ WrapPersonnage��5       $��Center=�68   5      5      5      �� 5      $��1��6   ���#a6a6a6._�5      ]� .+�5      �� -   5        $��Bottom=�5      $��35(�65   $��20q�6C   {�6O   �� *IsAlgorithmeSelectionne+.��6�   Lancer l'algorithme6�   �� 5      $��20q�6!   .+�5      ]� .+�5      �� -   5         ��Name$ 	ImageMode��5   $   $��55��67   $
��Left=�6C   �� 5        �� TextAlignment$
 Left=�6(   $��35(�6=   $��
20,10,0,10q�6K   Problème :6k   5!      �� 5"      -   5         ��Name$ ComboProbleme��5"   #   $��35��6:   $��
0,10,10,50q�6F   $��20(�6Z   ��6h   �� *ListNomProblemes+6�   ]� 5$      .+�5%      �� -   5        $ ImageJeuDeTest��5%   $   $��55��6<   $
��Left=�6H   �� 5&      $
 Left=�6(   $��35(�6=   $��
20,10,0,10q�6K   Jeu de test :6n   5'      �� 5(      -   5        $��35��5(   #   $��
0,10,10,50q�6/   $��20(�6C   ��6Q   �� *ListNomFichiersJeuTest+ �� SelectedItem 6�   �� *NomFichierJeuTest+ �� Mode$ TwoWay=�{�6�   �� *IsProblemeSelectionne+6
  ]� 5*      .+�5+      �� -	   5        $ ImageAlgorithme��5+   $   $��55��6=   $
��Left=�6I   �� 5,      $
 Left=�6)   $��35(�6>   $��
20,10,0,10q�6L   Algorithme :6n   5-      �� 5.      -
   5        $ ComboAlgorithme��5.   #   $��35��6<   $��
0,10,10,50q�6H   $��20(�6\   ��6j   �� *ListeAlgorithmes+{�6�   �� *IsJeuTestSelectionne+6�   �� 50      -   5        $ 	DockScore��50   %   $y�Hidden=�68   .+�51      ]� $��0,40,0,0q�6(   .+�52   "   �� -   5        $ 
ImageScore��52   (   $��55��6<   $
��Left=�6H   �� 53   "   $
 Left=�6,   $��35(�6A   $��
20,10,0,10q�6O   Score répartition6v   54      �� 55      -   5        	 ��Name$	 Score��55   (   $��75(�67   $ Center=�6E   6^   56      58      59      5:      5;      5<      5=      