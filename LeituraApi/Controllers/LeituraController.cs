using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LeituraArquivo.Entidades;

namespace LeituraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeituraController : ControllerBase
    {
        private Leitura leitura = new Leitura();
        private string caminho = @"c:\teste";

        // GET: api/Leitura
        [HttpGet]
        public string Get()
        {
            return "Leitura API - API para Retornar informações dos arquivos de texto a serem encaminhadas para a API de Corte";
        }

        // GET: api/Leitura/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Leitura
        [HttpPost]
        public IEnumerable<InfoCorteDado> Post([FromBody] Arquivo CaminhoDosArquivos)
        {
            List<InfoCorteDado> dadosCorte = leitura.GeraDadosParaCorte(CaminhoDosArquivos.caminho);

            return dadosCorte;
        }

        // PUT: api/Leitura/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
