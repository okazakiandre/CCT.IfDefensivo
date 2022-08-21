using CCT.IfDefensivo.App;
using Moq;

namespace CCT.IfDefensivo.UnitTest
{
    [TestClass]
    public class IncluirReservaCmdHandlerRefatTest
    {
        [TestMethod]
        public async Task DeveriaIncluirReserva()
        {
            var mockCli = CriarMockClienteApi();
            var mockRepo = CriarMockRepo(true);
            var hdl = new IncluirReservaCmdHandlerRefat(mockCli.Object, mockRepo.Object);
            var req = new IncluirReservaCmd(11111, DateTime.Today, DateTime.Today);

            var res = await hdl.Handle(req, CancellationToken.None);

            Assert.AreEqual("Reserva incluída com sucesso.", res);
            mockCli.Verify(m => m.ObterCliente(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public async Task NaoDeveriaIncluirReservaSemCpfDoCliente()
        {
            var mockCli = CriarMockClienteApi();
            var mockRepo = CriarMockRepo(true);
            var hdl = new IncluirReservaCmdHandlerRefat(mockCli.Object, mockRepo.Object);
            var req = new IncluirReservaCmd(0, DateTime.Today, DateTime.Today);

            var res = await hdl.Handle(req, CancellationToken.None);

            Assert.AreEqual("O CPF do cliente não foi informado.", res);
            mockCli.Verify(m => m.ObterCliente(It.IsAny<long>()), Times.Never);
        }

        [TestMethod]
        public async Task NaoDeveriaIncluirReservaParaClienteNaoCadastrado()
        {
            var mockCli = new Mock<IClienteApiClient>();
            var mockRepo = CriarMockRepo(true);
            var hdl = new IncluirReservaCmdHandlerRefat(mockCli.Object, mockRepo.Object);
            var req = new IncluirReservaCmd(11111, DateTime.Today, DateTime.Today);

            var res = await hdl.Handle(req, CancellationToken.None);

            Assert.AreEqual("O CPF informado não está cadastrado.", res);
            mockCli.Verify(m => m.ObterCliente(It.IsAny<long>()), Times.Once);
        }

        [TestMethod]
        public async Task NaoDeveriaIncluirReservaSeRepositorioNaoRetornarSucesso()
        {
            var mockCli = CriarMockClienteApi();
            var mockRepo = CriarMockRepo(false);
            var hdl = new IncluirReservaCmdHandlerRefat(mockCli.Object, mockRepo.Object);
            var req = new IncluirReservaCmd(11111, DateTime.Today, DateTime.Today);

            var res = await hdl.Handle(req, CancellationToken.None);

            Assert.AreEqual("Erro ao incluir a reserva.", res);
            mockCli.Verify(m => m.ObterCliente(It.IsAny<long>()), Times.Once);
        }

        private Mock<IClienteApiClient> CriarMockClienteApi()
        {
            var cliente = new Cliente { Nome = "cliente teste" };
            var mockCli = new Mock<IClienteApiClient>();
            mockCli.Setup(m => m.ObterCliente(It.IsAny<long>())).ReturnsAsync(cliente);
            return mockCli;
        }

        private Mock<IReservaRepository> CriarMockRepo(bool sucesso)
        {
            var mockRepo = new Mock<IReservaRepository>();
            mockRepo.Setup(m => m.Incluir(It.IsAny<Reserva>())).ReturnsAsync(sucesso);
            return mockRepo;
        }
    }
}