using CsvHelper.Configuration;
using RotasREST.Entidade;

namespace RotasREST.Map
{
    public class RotasMap : ClassMap<RotasEntity>
    {
        public RotasMap() 
        {
            Map(r => r.Partida).Name("partida");
            Map(r => r.Parada1).Name("parada1");
            Map(r => r.Parada2).Name("parada2");
            Map(r => r.Parada3).Name("parada3");
            Map(r => r.Destino).Name("destino");
            Map(r => r.Valor).Name("valor");
        }
    }
}
