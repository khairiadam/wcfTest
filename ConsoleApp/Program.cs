using Newtonsoft.Json;
using Shared;
using SoapHttpClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp
{
    class Program
    {
        //public static HttpClient httpClient = new HttpClient();
        public static SoapClient httpClient = new SoapClient();

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.ShowAllUsers();
            //program.ShowUsers();
            Console.ReadLine();
            using (WebClient proxy = new WebClient())
            {
                
                var response = proxy.DownloadString("https://localhost:44361/Auth.asmx/GeAll");
                Console.WriteLine(response);
                Console.ReadKey(true);

            }
        }


        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request    
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://localhost:44361/Auth.asmx");
            //SOAPAction    
            Req.Headers.Add(@"SOAPAction:http://tempuri.org/Addition");
            //Content_type    
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method    
            Req.Method = "POST";
            //return HttpWebRequest    
            return Req;
        }
        //private void ShowUsers()
        //{
        //    IEnumerable<UserModel> users;
        //    var req = CreateSOAPWebRequest();
        //    HttpContent sc = new StringContent(JsonConvert.SerializeObject(""), UnicodeEncoding.UTF8, "application/json");

        //    HttpResponseMessage webResp = httpClient.PostAsync($"https://localhost:44361/Auth.asmx/GetAll",sc).Result;

        //    var st = webResp.Content.ReadAsStringAsync().Result;
        //    Console.WriteLine(st);

        //}

        private void ShowAll()
        {

        }



        public async Task<int> AddNumbersAsync(Uri uri, int a, int b)
        {
            var soapString = this.ConstructSoapRequest(a, b);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("SOAPAction", "http://CalculatorService/ICalculatorService/Add");
                var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                using (var response = await client.PostAsync(uri, content))
                {
                    var soapResponse = await response.Content.ReadAsStringAsync();
                    return this.ParseSoapResponse(soapResponse);
                }
            }
        }

        private string ConstructSoapRequest(string email, string password)
        {
            return String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                    <s:Body>
                        <Add xmlns=""http://CalculatorService/"">
                            <email>{0}</email>
                            <password>{1}</password>
                        </Add>
                    </s:Body>
                </s:Envelope>", email, password);
        }

        private int ParseSoapResponse(string response)
        {
            var soap = XDocument.Parse(response);
            XNamespace ns = "http://CalculatorService/";
            var result = soap.Descendants(ns + "AddResponse").First().Element(ns + "AddResult").Value;
            return Int32.Parse(result);
        }


        //private void ShowAllUsers()
        //{
        //    HttpWebRequest req = CreateSOAPWebRequest();
        //    XmlDocument body = new XmlDocument();

        //    body.LoadXml(@"ReqBodyGetAll.xml");
        //    using (Stream stream = req.GetRequestStream())
        //    {
        //        body.Save(stream);
        //    }
        //    //Geting response from request    
        //    using (WebResponse Serviceres = req.GetResponse())
        //    {
        //        using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
        //        {
        //            //reading stream    
        //            var ServiceResult = rd.ReadToEnd();
        //            //writting stream result on console    
        //            Console.WriteLine(ServiceResult);
        //            Console.ReadLine();
        //        }
        //    }
        //}
    }
}
