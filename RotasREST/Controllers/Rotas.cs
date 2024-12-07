using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using RotasREST.Entidade;
using RotasREST.Map;
using System.Globalization;
using System.IO;
using System.Text;

namespace RotasREST.Controllers
{
    [ApiController]
    [Route("Rotas")]
    public class Rotas : ControllerBase
    {
        private readonly ILogger<Rotas> _logger;

        public Rotas(ILogger<Rotas> logger)
        {
            _logger = logger;
        }

        [HttpGet("/ConsultaRotas/{Partida}/{Destino}")]
        public RotasEntity ConsultaRotas(string Partida, string Destino)
        {
            List<RotasEntity> listRotas = Consulta();

            RotasEntity rotaSelecionada = listRotas.Where(r => r.Partida == Partida.ToUpper() && r.Destino == Destino.ToUpper()).OrderBy(r => r.Valor).FirstOrDefault();

            return rotaSelecionada;
        }

        [HttpPost("/CadastroRotas/")]
        public void CadastroRotas(RotasEntity rotasNovas)
        {
            List<RotasEntity> listRotas = Consulta();

            if (rotasNovas != null)
            {
                listRotas.Add(rotasNovas);
            }

            GravarRota(listRotas);
        }

        private List<RotasEntity> Consulta()
        {
            List<RotasEntity> listRotas = new List<RotasEntity>();

            RotasEntity retorno = new RotasEntity();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader("Rotas.csv"))
            {
                string arquivo = reader.ReadToEnd();
                var linhas = arquivo.Split("\r\n");

                for (int i = 1; i < linhas.Length; i++)
                {
                    if (linhas[i] == "") { break; }

                    var item = linhas[i].Split(',');

                    var rota = new RotasEntity()
                    {
                        Partida = item[0].ToUpper(),
                        Parada1 = item[1].ToUpper(),
                        Parada2 = item[2].ToUpper(),
                        Parada3 = item[3].ToUpper(),
                        Destino = item[4].ToUpper(),
                        Valor = int.Parse(item[5])
                    };

                    listRotas.Add(rota);
                }
            }

            return listRotas;
        }

        private void GravarRota(List<RotasEntity> listRotas)
        {
            using (var writer = new StreamWriter("Rotas.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<RotasMap>();
                    csv.WriteHeader<RotasEntity>();
                    csv.NextRecord();
                    csv.WriteRecords(listRotas);
                }
            }
        }
    }
}
