using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;

namespace AppConsumer.Controllers
{
    public class AlumnoController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
   
        List<Alumno> _oAlumnos = new List<Alumno>();
       

        public AlumnoController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Alumno>> GetAlumnos()
        {
            _oAlumnos = new List<Alumno>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Alumno"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oAlumnos = JsonConvert.DeserializeObject<List<Alumno>>(apiResponse);
                }
            }

            return _oAlumnos;
        }
    }
}