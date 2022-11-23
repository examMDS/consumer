using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using WebAPI.Models;


namespace AppConsumer.Controllers
{
    public class DetalleSolicitudController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        List<DetalleSolicitud> _oSolicitudes = new List<DetalleSolicitud>();
        List<Alumno> _oAlumnos = new List<Alumno>();
        List<Cursos> _oCursos = new List<Cursos>();

        public DetalleSolicitudController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<DetalleSolicitud>> GetSolicitudes()
        {
            _oSolicitudes = new List<DetalleSolicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/DetalleSolicitud"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<DetalleSolicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }

        

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<DetalleSolicitud>> GetDetalleSolicitudBySolicitud(int idSolicitud)
        {
            _oSolicitudes = new List<DetalleSolicitud>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/DetalleSolicitud/Solicitud/" + idSolicitud))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitudes = JsonConvert.DeserializeObject<List<DetalleSolicitud>>(apiResponse);
                }
            }

            return _oSolicitudes;
        }

        [HttpPost]
        public async Task<DetalleSolicitud> InsertSolicitud(DetalleSolicitud detalleSolicitud)
        {
   
            DetalleSolicitud _oSolicitud = new DetalleSolicitud();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(detalleSolicitud),
                    Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44334/api/DetalleSolicitud", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSolicitud = JsonConvert.DeserializeObject<DetalleSolicitud>(apiResponse);
                }
            }

            return _oSolicitud;
        }
    }
}
