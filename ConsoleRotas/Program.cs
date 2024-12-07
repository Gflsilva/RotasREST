


using RotasREST;

namespace ConsoleRotas
{
    public class Program : BaseService
    {
        static void Main(string[] args)
        {
            FiltroRotasModel filtroRotas = new FiltroRotasModel();

            Console.WriteLine("Informe o local de partida:");
            filtroRotas.Partida = Console.ReadLine();

            Console.WriteLine("Informe o local de destino:");
            filtroRotas.Destino = Console.ReadLine();

            Rotas rotas = new Rotas();

            var retorno = rotas.ConsultarRotaMaisBarata(filtroRotas);

            string textRota = $"{retorno.Partida} - {retorno.Parada1} - {retorno.Parada2} - {retorno.Parada3} - {retorno.Destino} valor: {retorno.Valor}";

            Console.WriteLine("A rota com o menor custo é {0} sendo {1}", retorno.Valor, textRota);
        }
    }
}
