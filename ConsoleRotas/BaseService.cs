using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RotasREST
{
    public abstract class BaseService : IDisposable
    {
        private string uri;
        private string serviceKey;

        public BaseService()
        {

        }

        public BaseService(string _uri, string _serviceKey)
        {
            this.uri = _uri;
            this.serviceKey = _serviceKey;
        }

        private void ConfigurarHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(this.uri);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", this.serviceKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T Get<T>(string request)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                this.ConfigurarHttpClient(client);
                response = client.GetAsync(request).Result;
            }

            string retorno = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                retorno = response.Content.ReadAsStringAsync().Result;
            }

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        public T Post<T>(string request, object parametro)
        {
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                this.ConfigurarHttpClient(client);
                StringContent conteudo = new StringContent(JsonConvert.SerializeObject(parametro), Encoding.UTF8, "application/json");
                response = client.PostAsync(request, conteudo).Result;
            }

            string retorno = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                retorno = response.Content.ReadAsStringAsync().Result;
            }

            return JsonConvert.DeserializeObject<T>(retorno);
        }

        #region Disposable

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion
    }
}
