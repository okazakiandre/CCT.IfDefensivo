namespace CCT.IfDefensivo.App
{
    public interface IClienteApiClient
    {
        Task<Cliente> ObterCliente(long cpfCliente);
    }
}
