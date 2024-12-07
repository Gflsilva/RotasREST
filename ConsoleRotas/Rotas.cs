using RotasREST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRotas
{
    public class Rotas : BaseService, IRotas
    {
        public RetornoRotasModel ConsultarRotaMaisBarata(FiltroRotasModel filtroRotas)
        {
            string request = string.Format("Instrutor/{0}/{1}", filtroRotas.Partida, filtroRotas.Destino);

            var retorno = Get<FiltroRotasModel>(request);

            return new RetornoRotasModel();
        }
    }
}
