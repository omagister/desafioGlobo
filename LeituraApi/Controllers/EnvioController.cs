using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LeituraArquivo.Entidades;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace LeituraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        // GET: api/Envio
        [HttpGet]
        public string Get()
        {
            return "LEITURA API (/Envio) - Envio de informações para API de Corte";
        }

        // POST: api/Envio
        [HttpPost]
        public async Task<JobCorte> Post([FromBody] InfoCorteDado DadosParaCorte)
        {
            JobCorte jobCorte = new JobCorte();

            InfoJob infoJob = new InfoJob();

            infoJob.StartTime = DadosParaCorte.StartTime.Extenso();
            infoJob.EndTime = DadosParaCorte.EndTime.Extenso();
            infoJob.NomeArquivo = DadosParaCorte.NomeArquivo;
            infoJob.Path = DadosParaCorte.Path;

            string json = JsonConvert.SerializeObject(infoJob);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync (
                    "http://yourUrl",
                     new StringContent(json, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                jobCorte = JsonConvert.DeserializeObject<JobCorte>(responseBody);
            }

            return jobCorte;
        }

        
    }

    public class InfoJob
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string NomeArquivo { get; set; }
        public string Path { get; set; }
    }

    public class JobCorte
    {
        public string JobId { get; set; }
        public string Status { get; set; }
    }

}
