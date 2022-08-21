using MediatR;

namespace CCT.IfDefensivo.App
{
    public class IncluirReservaCmdHandlerRefat : IRequestHandler<IncluirReservaCmd, string>
    {
        private IClienteApiClient ClienteCli { get; }
        private IReservaRepository Repo { get; }

        public IncluirReservaCmdHandlerRefat(IClienteApiClient cli, IReservaRepository repo)
        {
            ClienteCli = cli;
            Repo = repo;
        }

        public async Task<string> Handle(IncluirReservaCmd request, CancellationToken cancellationToken)
        {
            if (request.CpfCliente <= 0)
            {
                return "O CPF do cliente não foi informado.";
            }
            
            var dadosCli = await ClienteCli.ObterCliente(request.CpfCliente);
            if (dadosCli is null)
            {
                return "O CPF informado não está cadastrado.";
            }

            var novaReserva = Reserva.CriarNova(request.DataEntrada,
                                                request.DataSaida,
                                                request.CpfCliente,
                                                dadosCli.Nome);

            var sucesso = await Repo.Incluir(novaReserva);
            if (sucesso)
            {
                return "Reserva incluída com sucesso.";
            }

            return "Erro ao incluir a reserva.";
        }
    }
}
