using AppConsumer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace AppConsumer.Controllers
{
    public class SolicitudController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        List<Solicitud> _oSolicitudes = new List<Solicitud>();
           

        public SolicitudController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Solicitud>> GetSolicitudes()
        {
            _oSolicitudes = new List<Solicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Solicitud"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<Solicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }

       

        [HttpGet]
        public async Task<List<Solicitud>> GetSolicitudByPeriodo(string periodo)
        {
            _oSolicitudes = new List<Solicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Solicitud/periodo/" + periodo))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<Solicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }


        [HttpGet]
        public async Task<List<Solicitud>> GetSolicitudByFecha(string fecha)
        {
            _oSolicitudes = new List<Solicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Solicitud/fecha/" + fecha))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<Solicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }

        [HttpGet]
        public async Task<List<Solicitud>> GetSolicitudByIdAlumno(int id)
        {
            _oSolicitudes = new List<Solicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Solicitud/alumno/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<Solicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }


        [HttpGet]
        public async Task<List<Solicitud>> GetSolicitudByAlumno(string nombres)
        {
            _oSolicitudes = new List<Solicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Solicitud/alumno/nombre/" + nombres))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<Solicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }

        [HttpGet]
        public async Task<List<Solicitud>> GetSolicitudByCurso(string curso)
        {
            _oSolicitudes = new List<Solicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Solicitud/cursos/" + curso))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<Solicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }

        


        public IActionResult Consulta()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Solicitud>> BuscarSolicitud(string nombre)
        {
            _oSolicitudes = new List<Solicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Solicitud/" + nombre))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<Solicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }

        [HttpPost]
        public async Task<Solicitud> InsertSolicitud(Solicitud solicitud)
        {
            solicitud.FechaSolicitud = DateTime.Now;
            Solicitud _oSolicitud = new Solicitud();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(solicitud),
                    Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44334/api/Solicitud", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                     _oSolicitud = JsonConvert.DeserializeObject<Solicitud>(apiResponse);
                }  
            }

            return _oSolicitud;
        }
    }
}

