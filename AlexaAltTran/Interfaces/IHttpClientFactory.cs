using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AlexaAltTran.Interfaces
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}