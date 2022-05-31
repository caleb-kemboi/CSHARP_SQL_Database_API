using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DbConsole
{
    internal class ApiHelper
    {
        private static HttpClient client;


        public static ApiBody SendRequest(string type, string endpoint, HttpContent data = null)
        {
            client = new HttpClient();
            //client.BaseAddress = new Uri(GeneralSetup.BASE_API_URL);
            client.BaseAddress = new Uri("https://webhook.site");

            ApiBody apiBody = new ApiBody();

            try
            {
                var responseTask = type.ToLower().Equals("post") ? client.PostAsync(endpoint, data) : client.GetAsync(endpoint);
                responseTask.Wait();

                var result = responseTask.Result;

                apiBody.code = CodeMapper.SUCCESS;

                if (!result.IsSuccessStatusCode)
                {
                    apiBody.code = CodeMapper.FAIL;
                    apiBody.message = "Request failed!";
                }

                apiBody.content = apiBody.code == 1 ? result.Content : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                apiBody.code = CodeMapper.FAIL;
            }

            return apiBody;

        }

        public struct ApiBody

        {
            public int code { get; set; }
            public HttpContent content { get; set; }
            public string message { get; set; }
            

        }

        public struct CodeMapper
        {
            public const int FAIL = 0;
            public const int SUCCESS = 1;
            public const int SESSION_NOK = 2;
        }

    }
}

