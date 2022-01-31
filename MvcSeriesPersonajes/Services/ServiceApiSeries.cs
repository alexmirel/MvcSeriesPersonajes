using MvcSeriesPersonajes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcSeriesPersonajes.Services
{
    public class ServiceApiSeries
    {
        private string Url;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiSeries(string url)
        {
            this.Url = url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Series/GetPersonajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);

            return personajes;
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "api/Series/GetSeries";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);

            return series;
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "api/Series/FindPersonaje/" + id;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);

            return personaje;
        }

        public async Task<Serie> FindSerieAsync(int id)
        {
            string request = "api/Series/FindSerie/" + id;
            Serie serie = await this.CallApiAsync<Serie>(request);

            return serie;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(int id)
        {
            string request = "api/Series/GetPersonajesSerie/" + id;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);

            return personajes;
        }

        public async Task UpdatePersonajeSerie(int idPersonaje, int idSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Series";
                client.BaseAddress = new Uri(this.Url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Personaje personaje = await this.FindPersonajeAsync(idPersonaje);
                personaje.IdSerie = idSerie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
    }
}
