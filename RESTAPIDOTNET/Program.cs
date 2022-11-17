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
                System.Collections.Specialized.NameValueCollection postData =
                    new System.Collections.Specialized.NameValueCollection()
                   {{ "encrypted", json }};
                string pagesource = Encoding.UTF8.GetString(client.UploadValues(URI, postData));
                Console.WriteLine("pagesource:::::::::::::::::::" + pagesource);

               /// return pagesource;
            }
        }

        public static string Encryption(string strText)
        {
            var publicKey = "-----BEGIN RSA PRIVATE KEY-----\r\nMIIEowIBAAKCAQEAyGVfRqJ1KGkLh/cG6qiKKexZSTEcVEk3t3UkYdkfgWFd4Vyl\r\nNd36V7ckE+qCNhM8piXoVZX4WZFFs8S4ByzWPn3wK5lSpZDwDlqFb0fG2mnNyGGp\r\nM5Equ0rB6XqiPkS2Bre5gSSxu5agyVog5wyPYAwklLpi7EFWpsRi3atEhqzWS+de\r\nopNkmmu5+dmhzOTs+eO/LctrqShqoIMLWCY0k4NMczhHXxuc8PMM/UnbTbm3hwQ5\r\nVDjDMHz3qJksBpGRd8RzLz2D/YOL5ZQAj/IP9iZjf1WV4b3Gr6ZTLNhB5kfX6xEF\r\nZm9vXHCDhKY/A1oWiEIc6AthlDMhoyqpszWGIwIDAQABAoIBAQCvWAQFyiC5OzDb\r\nx0pGCTLbe/KkGFSxa19VJyquc00d6BQ9HNNyTsi994GZ65gDuNFYj9K7EH494N8c\r\nbQSZQqI0C/4aKA4o0Kk75fjbZfKZKfrlWo6ykFLTjpYdtTBBS936AshON81MVoBZ\r\nroMbp+HMKNPZTz0/e+xV/4CCdVmAfC511RM6aUFjDi55o2UPhuhFCCapa5G2nI/K\r\nT7BxafrYc493k6sYOCJgRc7HHMBotX2U9psk9lDkzlbrmNEMIdifznvnk9mNkoCp\r\nYa4rdTE0+t24CJxOBianwRcnoSGwuHhQOa2kbk93am1ifyfN8+MZ4oc8qOGZKMeI\r\n7zcOoAABAoGBAOmqzXmpzf1gZD+ZwtFwH4TUSUbYVfaPu4ONlEQYlfIgRKkf6ceG\r\naAJ0zXmXWtUSVy2+QjMzGy2twv+4B47kUS5YoF2dkpnSG3XavyLHUq3kIUrM/fhu\r\nuoYgtNn7X1MePHSvayVRS/T2EklRGTxr/BGuQdkhoolNhSFtuk+N1WJXAoGBANuM\r\ng5F5W7Ur9a97uT+QSgPO0NivF3qpOARUOIQZuzRwd1EiAcXvr+nN96bE4dkQkqcq\r\nhmFvUsWs32qJyMWKT12viazBmOjgo3N9P+dwdvgq90x4K89ZJQRaeugngb+PEg/D\r\n/wcXTKzNiccANYkarBXu1UJ+MdpEWP9qVlon2BMVAoGAScsoMwj+RcugPTm4/d2U\r\nBqXoMlh15XUE+gnHTi4ZhKrOJD8w3FNFG6l5jrHO/MVbBNY2H+c+6REcKRgcQvM1\r\n9BYHJylxN+TTyUd432nlYYdPQEk217NYcq13j/PD6gL9grg3dUSUYVso8UCMYLmV\r\nlJvcrc7ifswdhC4cDa20OucCgYB27MpoBsDyhnlzMSXLlLFDFshyV4X0X62ESsrC\r\nY93QLguz2yPywD8d+v/nSka8egm2m4ZnSRIhGd97ql8jAiSzrt54wfW+T9C6QmJX\r\nT8hh7YQ/2+h/TN6MWSaykpPp2+oAuD5w7OIPyNaVATv+pIAK8XB7sDf474T19fgI\r\npgSZcQKBgCIs2bkfxVRbUW/lX+uHp8xXNZ+UokAsRUzQ9wHS/vwIPEoXmWEACRsA\r\n9IYquofMQeslAKLWs++ZmkzzLBMi5Uq0JLEQ2fls1JUyTMMcWdvDfOaRAi++NjL8\r\nbmR8QOyuN+uCnlolbnnU3uZViAdbVRnNTWEDWDOoKU8r6skS70vq\r\n-----END RSA PRIVATE KEY-----";

            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    // client encrypting data with public key issued by server                    
                    rsa.FromXmlString(publicKey.ToString());
                    var encryptedData = rsa.Encrypt(testData, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }



        public static string Decryption(string strText)
        {
            var privateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";
            var testData = Encoding.UTF8.GetBytes(strText);
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64Encrypted = strText;
                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKey);
                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
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
