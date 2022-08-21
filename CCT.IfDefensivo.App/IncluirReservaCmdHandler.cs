using MediatR;

namespace CCT.IfDefensivo.App
{
    public class IncluirReservaCmdHandler : IRequestHandler<IncluirReservaCmd, string>
    {
        private IClienteApiClient ClienteCli { get; }
        private IReservaRepository Repo { get; }

        public IncluirReservaCmdHandler(IClienteApiClient cli, IReservaRepository repo)
        {
            ClienteCli = cli;
            Repo = repo;
        }

        public async Task<string> Handle(IncluirReservaCmd request, CancellationToken cancellationToken)
        {
            var nome = "";
            if (request.CpfCliente > 0)
            {
                var dadosCli = await ClienteCli.ObterCliente(request.CpfCliente);
                if (dadosCli is not null)
                {
                    nome = dadosCli.Nome;
                }
            }

            var novaReserva = Reserva.CriarNova(request.DataEntrada,
                                                request.DataSaida,
                                                request.CpfCliente,
                                                nome);

            var sucesso = await Repo.Incluir(novaReserva);
            if (sucesso)
            {
                return "Reserva incluída com sucesso.";
            }

            return "Erro ao incluir a reserva.";
        }
    }
}
