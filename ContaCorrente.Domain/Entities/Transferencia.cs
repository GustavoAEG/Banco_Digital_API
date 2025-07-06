using System;

namespace ContaCorrente.Domain.Entities
{
    public class Transferencia
    {
        public Guid Id { get; private set; } 
        public Guid ContaOrigemId { get; private set; } 
        public Guid ContaDestinoId { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public decimal Valor { get; private set; }

        public Transferencia(Guid id, Guid contaOrigemId, Guid contaDestinoId, DateTime dataMovimento, decimal valor)
        {
            Id = id;
            ContaOrigemId = contaOrigemId;
            ContaDestinoId = contaDestinoId;
            DataMovimento = dataMovimento;
            Valor = valor;
        }
    }
}
