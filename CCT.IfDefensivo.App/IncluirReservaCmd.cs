using MediatR;

namespace CCT.IfDefensivo.App
{
    public class IncluirReservaCmd : IRequest<string>
    {
        public IncluirReservaCmd(long cpf, DateTime entrada, DateTime saida)
        {
            CpfCliente = cpf;
            DataEntrada = entrada;
            DataSaida = saida;
        }
        public long CpfCliente { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
