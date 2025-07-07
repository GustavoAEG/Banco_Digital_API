using System;

namespace ContaCorrente.Domain.Entities
{
    public class Movimento
    {
        public Guid Id { get; private set; }
        public Guid ContaCorrenteId { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public string TipoMovimento { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }
        public string Idempotencia { get; private set; } = string.Empty;

        public Movimento(Guid id, Guid contaCorrenteId, string tipoMovimento, decimal valor, string idempotencia, DateTime dataMovimento)
        {
            Id = id;
            ContaCorrenteId = contaCorrenteId;
            TipoMovimento = tipoMovimento;
            Valor = valor;
            Idempotencia = idempotencia;
            DataMovimento = dataMovimento;
        }

        protected Movimento() { }
    }
}
