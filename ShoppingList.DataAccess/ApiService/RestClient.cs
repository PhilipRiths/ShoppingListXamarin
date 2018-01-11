using System;
using System.IO;
using System.Net;

namespace ShoppingList.DataAccess.ApiService
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RestClient
    {
        public string _endPoint { get; set; }
        public httpVerb _httpMethod { get; set; }

        public RestClient()
        {
            _endPoint = string.Empty;
            _httpMethod = httpVerb.GET;
        }

        public string MakeRequest()
        {
            var strResponsValue = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_endPoint);

            request.Method = _httpMethod.ToString();

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("Error code: " + response.StatusCode.ToString());
                }

                //Process the response stream...

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponsValue = reader.ReadToEnd();
                        }// End of StreamReader
                    }
                }// End of ResponseStream
            }// End of using Response

            return strResponsValue;
        }
    }
}