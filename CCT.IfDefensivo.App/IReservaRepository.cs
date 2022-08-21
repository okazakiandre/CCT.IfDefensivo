namespace CCT.IfDefensivo.App
{
    public interface IReservaRepository
    {
        Task<bool> Incluir(Reserva rsv);
        Task<long> Atualizar(Reserva rsv);
        Task<bool> Excluir(string numeroReserva);
        Task<Reserva> Obter(string numeroReserva);
        Task<bool> Cancelar(string numeroReserva);
    }

}
