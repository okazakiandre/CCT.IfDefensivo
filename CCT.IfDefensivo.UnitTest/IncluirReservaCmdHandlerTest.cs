using CCT.IfDefensivo.App;
using Moq;

namespace CCT.IfDefensivo.UnitTest
{
    //[TestClass]
    public class IncluirReservaCmdHandlerTest
    {
        [TestMethod]
        public async Task DeveriaIncluirReserva()
        {
            var mockCli = new Mock<IClienteApiClient>();
            var mockRepo = new Mock<IReservaRepository>();
            mockRepo.Setup(m => m.Incluir(It.IsAny<Reserva>())).ReturnsAsync(true);
            var hdl = new IncluirReservaCmdHandler(mockCli.Object, mockRepo.Object);
            var req = new IncluirReservaCmd(11111, DateTime.Today, DateTime.Today);

            var res = await hdl.Handle(req, CancellationToken.None);

            Assert.AreEqual("Reserva incluída com sucesso.", res);
        }

        [TestMethod]
        public async Task NaoDeveriaIncluirReserva()
        {
            var mockCli = new Mock<IClienteApiClient>();
            var mockRepo = new Mock<IReservaRepository>();
            mockRepo.Setup(m => m.Incluir(It.IsAny<Reserva>())).ReturnsAsync(false);
            var hdl = new IncluirReservaCmdHandler(mockCli.Object, mockRepo.Object);
            var req = new IncluirReservaCmd(11111, DateTime.Today, DateTime.Today);

            var res = await hdl.Handle(req, CancellationToken.None);

            Assert.AreEqual("Erro ao incluir a reserva.", res);
        }
    }
}