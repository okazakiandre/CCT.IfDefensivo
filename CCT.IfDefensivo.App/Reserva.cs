namespace CCT.IfDefensivo.App
{
    public class Reserva
    {
        public string NumeroReserva { get; private set; }
        public long CpfCliente { get; private set; }
        public string NomeCliente { get; private set; }
        public DateTime DataEntrada { get; private set; }
        public DateTime DataSaida { get; private set; }
        public short StatusReserva { get; private set; }
        public DateTime UltimaAtualizacao { get; private set; }

        public static Reserva CriarNova(DateTime entrada,
                                        DateTime saida,
                                        long cpfCliente,
                                        string nomeCliente)
        {
            var reserva = new Reserva()
            {
                DataEntrada = entrada,
                DataSaida = saida,
                CpfCliente = cpfCliente,
                NomeCliente = nomeCliente,
                StatusReserva = 1,
                UltimaAtualizacao = DateTime.Now
            };

            return reserva;
        }
    }
}
