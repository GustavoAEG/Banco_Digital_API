using System;

namespace ContaCorrente.Domain.Entities
{
    public class Movimento
    {
        public Guid Id { get; private set; }         
        public Guid ContaCorrenteId { get; private set; }
        public string DataMovimento { get; private set; } = string.Empty;
        public string TipoMovimento { get; private set; } = string.Empty;
        public decimal Valor { get; private set; }

        public Movimento(Guid id, Guid contaCorrenteId, string dataMovimento, string tipoMovimento, decimal valor)
        {
            Id = id;
            ContaCorrenteId = contaCorrenteId;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        protected Movimento() { }
    }
}
