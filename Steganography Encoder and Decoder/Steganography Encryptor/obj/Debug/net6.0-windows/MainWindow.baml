   M S B A M L   `   `   `  ????R  NSteganography Encryptor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Steganography_EncryptorSteganography_Encryptor  5   	   (    "Steganography_Encryptor.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Steganography_Encryptor     5   	     ? Title$#  Steganography Encoder/Decoder??5   	   $	??550??6/   $	??875??6<   $	??300??6H   $	??400??6X    ? WindowState$ Normal=?5	   	    ? 
ResizeMode$ NoResize=?6   .??5
      ? .+?5   
   ]? .+?5      o? $	??Top=?6   $??Stretch=?6(   $	??Top=?6F   $
??auto??6^   $
??auto??6l   $??20??6y   $	??800??6?   .@?5      m? -   5        $??fileTab??5      $
??File??6*   .@?5      m? -   5        $??saveTab??5      $??Save As??6.   6?   m? 5      -   5        $??exitTab??5      $
??Exit??6.   6<   5      m? 5      -   5        $??helpTab??5      $
??Help??6*   .@?5      m? -   5        $??aboutTab??5      $??About??6/   6>   5      5      ?? 5      $??Bottom=?6   .@?5      ?? $??Encrypt??6   $??16(?6+   $??Arial)?69   $??Normal"?6L   .??5      ]? .+?5      A?  A? Columns$ 2??6&   .+?5      ?  ? ColumnDefinitions 5   "   ?? 5   &   $	??43* ?67   ?? 5   &   $
??379* ?67   .+?5!   "   ??    WetAsphaltGradient#??[  6)   $??2??6Z   ._?5#   &   ?? -   5        $??
imgDisplay??5#   ,   5&   #   5'      ? 5(        Innuendo#??[ 6#   .+?5*   "   ?? $??Enter message here??6(   $
??Left=?6E   $	??Top=?6`   $
??Bold"?6x   $??	24,27,0,0q?6?   $??14(?6?   ?? 5+   "   -   5         ??Name$ secretMsgBox??5+   *    ?? 	MaxLength$	 256??6@   $ ?? HorizontalScrollBarVisibility$ Visible=?6P   " ?? VerticalScrollBarVisibility$
 Auto=?6x   	 ?? TextWrapping$
	 Wrap=?6?   $
??Left=?6?   $?? ??6?   $	??200??6?   $??27??6?   $	??Top=?6?   $??	30,58,0,0q?60  $	??345??6C  $	??123??6O  ?? 5-   "   -   5        $ 	textCount??5-   *   $
??Left=?6>   $?? ??6Y   $??21??6a   $	??Top=?6m   $??27??6?   $??
49,270,0,0q?6?   ?? 51   "   -	   5        $??hideBtn??51   )   $??Encrypt??68   $??45??6K   $	??105??6W   $??214,294,103,70q?6y   $??13(?6?   ?? 52   "   $??Count??6(   $
??Left=?68   $	??Top=?6S   $??
34,231,0,0q?6k   $??16(?6   ?? 53   "   $??/ 256??6(   $
??Left=?68   $	??Top=?6S   $??
81,266,0,0q?6k   $??14(?6   ?? 54   "   -
   5        $??encodeBrowser??54   )   $??Upload??6>   $??45??6P   $	??105??6\   $??13(?6i   $??215,217,102,147q?6w   6?   57      58      59      5:      ?? 5<      $??Decrypt??6   $??16(?6+   $??Arial)?69   $??22??6L   $	??Top=?6X   .??5=      ]? .+?5>      A? $ 2??6&   .+?5?      ? .+?5@   "   ?? ???#3bb123._?5A   &   ?? -   5        $??imgDisplay2??5A   ,   5C   #   5D      ? 5E      ???#2a7e19.+?5G   "   ?? $??Enter message here??6(   $
??Left=?6E   $	??Top=?6`   $
??Bold"?6x   $??	24,27,0,0q?6?   $??14(?6?   ?? 5H   "   -   5        $??	revealBox??5H   *   $	 256??6;   $ Visible=?6K   $
 Auto=?6s   $
	 Wrap=?6?   $??Center=?6?   $?? ??6?   $	??200??6?   $??27??6?   $	??Top=?6?   $??0,56,0,0q?6-  $	??332??6?  $	??158??6K  ?? 5I   "   -   5        $??decodeBrowser??5I   )   $??Upload??6>   $??45??6P   $	??105??6\   $??49,276,280,103q?6h   $??13(?6?   6?   ?? 5J   "   -   5        $??	decodeBtn??5J   )   $??Extract??6:   $??45??6L   $	??105??6X   $??231,243,74,68q?6|   $??13(?6?   5K      5L      5M      5N      5O      5P      5Y      5Z      