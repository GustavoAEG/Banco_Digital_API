using System;

namespace ContaCorrente.Domain.Entities
{
    public class Transferencia
    {
        public Guid Id { get; set; }
        public Guid ContaOrigemId { get; set; }
        public Guid ContaDestinoId { get; set; }
        public DateTime DataMovimento { get; set; }
        public decimal Valor { get; set; }

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
