using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRotas
{
    public interface IRotas
    {
        public RetornoRotasModel ConsultarRotaMaisBarata(FiltroRotasModel filtroRotas);
    }
}
