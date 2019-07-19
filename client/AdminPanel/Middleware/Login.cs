using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;
using Middleware;
using AdminPanel.Middleware;
using System.Threading.Tasks;

namespace Middleware
{
    public class Login
    {
        private static readonly HttpClient http = new HttpClient();
        public static async Task LoginWithCredAsync(string username, string password)
        {

        }
    }
}
