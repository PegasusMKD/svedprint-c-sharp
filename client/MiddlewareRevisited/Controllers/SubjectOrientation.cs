﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MiddlewareRevisited.Models;

namespace MiddlewareRevisited.Controllers
{
    public class SubjectOrientation
    {

        public static async Task<Models.SubjectOrientation> addSubjectOrientation(Models.SubjectOrientation subjectOrientation, Models.User currentUser)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Post, "http://34.107.121.20:8080/api/subjectOrientations");
            var json = JsonConvert.SerializeObject(subjectOrientation);
            data.Headers.Add("token", currentUser.token);
            data.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var ret = await httpClient.SendAsync(data);
            return JsonConvert.DeserializeObject<Models.SubjectOrientation>(await ret.Content.ReadAsStringAsync());
        }


        public static async Task removeSubjectOrientation(Models.SubjectOrientation subjectOrientation, Models.User currentUser)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Delete, "http://34.107.121.20:8080/api/subjectOrientations");
            var json = JsonConvert.SerializeObject(subjectOrientation);
            data.Headers.Add("token", currentUser.token);
            data.Content = new StringContent(json, Encoding.UTF8, "application/json");
            await httpClient.SendAsync(data);
            return;
        }

        public static async Task<List<Models.SubjectOrientation>> getSubjectOrientations(Models.User currentUser)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Get, "http://34.107.121.20:8080/api/subjectOrientations");
            data.Headers.Add("token", currentUser.token);
            var ret = await httpClient.SendAsync(data);
            return JsonConvert.DeserializeObject<List<Models.SubjectOrientation>>(await ret.Content.ReadAsStringAsync());
        }

        public static async Task<Models.SubjectOrientation> updateSubjectOrientation(Models.SubjectOrientation subjectOrientation, Models.User currentUser)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Put, "http://34.107.121.20:8080/api/subjectOrientations");
            var json = JsonConvert.SerializeObject(subjectOrientation); // This should be just the fields that should get updated and shortName, nothing more, nothing less
            data.Headers.Add("token", currentUser.token);
            data.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var ret = await httpClient.SendAsync(data);
            return JsonConvert.DeserializeObject<Models.SubjectOrientation>(await ret.Content.ReadAsStringAsync());
        }
    }
}