using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingList.Shared.ApiService
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
        public string PostJSON { get; set; }

        public RestClient()
        {
            _endPoint = string.Empty;
            _httpMethod = httpVerb.GET;
        }

        public async Task<string> MakeRequest()
        {
            var strResponsValue = string.Empty;

            // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_endPoint);

            //request.Method = _httpMethod.ToString();

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //{
            //    if (response.StatusCode != HttpStatusCode.OK)
            //    {
            //        throw new ApplicationException("Error code: " + response.StatusCode.ToString());
            //    }

            //    //Process the response stream...

            //    using (Stream responseStream = response.GetResponseStream())
            //    {
            //        if (responseStream != null)
            //        {
            //            using (StreamReader reader = new StreamReader(responseStream))
            //            {
            //                strResponsValue = reader.ReadToEnd();
            //            }// End of StreamReader
            //        }
            //    }// End of ResponseStream
            //}// End of using Response

            // Discover endpoints from metadata.
            var discoveryResponse = await DiscoveryClient.GetAsync("http://192.168.1.225:3000");

            if (discoveryResponse.IsError)
            {
                Console.WriteLine(discoveryResponse.Error);
                return "ERROR";
            }

            // Request token.
            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, "shoppingList", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("shoppingListApi");

            if (tokenResponse.IsError)
            {
                return "ERROR";
            }

            // Call API.
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            switch (_httpMethod.ToString())
            {
                case "GET":
                    var response = await client.GetAsync(_endPoint);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                        return "ERROR";
                    }
                    else
                    {
                        Console.WriteLine(strResponsValue);
                        strResponsValue = await response.Content.ReadAsStringAsync();
                    }
                    break;

                case "POST":
                    if (_httpMethod.Equals("POST") && PostJSON != string.Empty)
                    {
                        var stringContent = new StringContent(PostJSON);
                        await client.PostAsync(_endPoint, stringContent);
                    }
                    break;

                case "DELETE":
                    await client.DeleteAsync(_endPoint);
                    break;

                case "UPDATE":

                    break;

                default:
                    break;
            }

            return strResponsValue;
        }
    }
}
