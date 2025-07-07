using ContaCorrente.Domain.Entities;
using Xunit;

namespace ContaCorrente.Tests.Domain
{
    public class ContaCorrenteTests
    {
        [Fact]
        public void Inativar_DeveAlterarStatusParaInativo()
        {
            // Arrange
            var conta = new ContaCorrente.Domain.Entities.ContaCorrente(
                Guid.NewGuid(),
                cpf: "12345678901",
                nome: "Gustavo",
                numero: 1234,
                senha: "hash",
                salt: "salt"
            );

            // Act
            conta.Inativar();

            // Assert
            Assert.False(conta.Ativo);
        }
    }
}
