using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;

namespace AppConsumer.Controllers
{
    public class CursoController : Controller
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        List<Cursos> _oCursos = new List<Cursos>();


        public CursoController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<List<Cursos>> GetCursos()
        {
            _oCursos = new List<Cursos>();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44334/api/Cursos"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCursos = JsonConvert.DeserializeObject<List<Cursos>>(apiResponse);
                }
            }

            return _oCursos;
        }
    }
}