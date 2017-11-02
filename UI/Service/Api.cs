using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
//using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace UI.Service
{
    public class Api : IDisposable
    {
        public string BaseUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["WebApiUrl"];
            }
        }

        public async Task<T> Request<T>(HttpMethod httpMethod, string apiUrl, string queryString = null, List<KeyValuePair<string, string>> formData = null, object rawData = null)
        {
            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl + (BaseUrl.Substring(BaseUrl.Length - 1) == "/" ? "" : "/"));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = new System.Net.Http.HttpResponseMessage();

                switch (httpMethod.ToString())
                {
                    case "POST":
                        if (formData != null)
                            response = Task.Run(() => client.PostAsync(BaseUrl + apiUrl, formData == null ? null : new FormUrlEncodedContent(formData.ToArray()))).Result;
                        else
                            response = Task.Run(() => client.PostAsync(BaseUrl + apiUrl, new ObjectContent(rawData.GetType(), rawData, new JsonMediaTypeFormatter(), "application/json"))).Result;
                        break;
                    case "PUT":
                        if (formData != null)
                            response = Task.Run(() => client.PutAsync(BaseUrl + apiUrl, formData == null ? null : new FormUrlEncodedContent(formData.ToArray()))).Result;
                        else
                            response = Task.Run(() => client.PutAsync(BaseUrl + apiUrl, new ObjectContent(rawData.GetType(), rawData, new JsonMediaTypeFormatter(), "application/json"))).Result;
                        break;
                    case "DELETE":
                        response = Task.Run(() => client.DeleteAsync(BaseUrl + apiUrl + "?" + queryString)).Result;
                        break;
                    default: //GET
                        response = Task.Run(() => client.GetAsync(BaseUrl + apiUrl + "?" + queryString)).Result;
                        break;
                }

                var data = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(
                        data, new Newtonsoft.Json.JsonSerializerSettings
                        {
                            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                        });


                }
                else if (httpMethod == HttpMethod.Get && response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new System.Exception(response.StatusCode.ToString());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return default(T);
                }
                else
                {
                    throw new System.Exception(data);
                }
            }

        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}