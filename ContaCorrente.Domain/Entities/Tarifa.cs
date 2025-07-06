using System;

namespace ContaCorrente.Domain.Entities
{
    public class Tarifa
    {
        public Guid IdTarifa { get; private set; }
        public Guid IdContaCorrente { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public decimal Valor { get; private set; }

        public Tarifa(Guid idTarifa, Guid idContaCorrente, DateTime dataMovimento, decimal valor)
        {
            IdTarifa = idTarifa;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            Valor = valor;
        }

        private Tarifa() { }
    }
}

