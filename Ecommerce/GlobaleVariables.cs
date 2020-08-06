using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Ecommerce
{
    public static class GlobaleVariables
    {
       
        public static HttpClient webApiClient = new HttpClient();

        static GlobaleVariables()
        {
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            webApiClient.BaseAddress = new Uri("https://localhost:44385/api/");
            webApiClient.DefaultRequestHeaders.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}