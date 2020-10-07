using System;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace DotNet.Core.TestSSL
{
    class Program
    {


        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {

            try
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                {
                    Console.WriteLine("    ooo Certificate OK");
                    return true;
                }
                else
                {
                    Console.WriteLine("    --- Certificate ERROR");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("    --- Certificate ERROR 2");
                return false;

            }

        }

        private static void TestCert(string url2)
        {
            try
            {
                string url = url2.ToLower().Replace("https://", "").Replace("http://", "").Replace("www.", "");

                if (url.Split(".").Count() <= 2) {
                    url = "https://www." + url;
                } else {
                    url = "https://" + url;
                }
              
                

                Console.WriteLine("testing {0}", url);
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) { }

            }
            catch { Console.WriteLine("   --- Fail!"); }

        }
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Hello World!  ");


            }
            else {
                foreach (string arg in args)
                {
                    Console.WriteLine("========  {0} ", arg);
                    TestCert(arg);
                }
               


            }


      
             
            
            
        }
    }
}
