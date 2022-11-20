using Owin;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace RESTAPIDOTNET
{

     class Program
    {
       
        public void  POST_DATA()
        {
            string URI = "http://localhost:5000/sendme";
            using (WebClient client = new WebClient())
            {
                Dictionary<string, string>  map = new Dictionary<string, string>();
                map.Add("machine_name", Environment.MachineName);
                map.Add("machine_type", "1");
                map.Add("product_key", "6LRE-AKDD-H62S-YO6K");
               string json =  JsonConvert.SerializeObject(map);
                Console.WriteLine("json::::::::::::::" + json);
                RSA enc = new RSA();
                string encripted = enc.Encryption(json);
                Console.WriteLine("encripted::::::::::::::::::" + encripted);

                System.Collections.Specialized.NameValueCollection postData =
                    new System.Collections.Specialized.NameValueCollection()
                   {{ "encrypted", json }};

               
                string pagesource = Encoding.UTF8.GetString(client.UploadValues(URI, postData));
                Console.WriteLine("pagesource:::::::::::::::::::" + pagesource);

               /// return pagesource;
            }
        }


        static void Main(string[] args)
        {
            Program p = new Program();
            p.POST_DATA();
            Console.ReadLine();
        }
    }
}
