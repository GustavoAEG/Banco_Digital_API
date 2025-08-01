﻿using System;

namespace ContaCorrente.Domain.Entities
{
    public class Idempotencia
    {
        public string ChaveIdempotencia { get; set; } = string.Empty;
        public string? Requisicao { get; set; }
        public string? Resultado { get; set; }

        public Idempotencia(string chaveIdempotencia, string? requisicao, string? resultado)
        {
            ChaveIdempotencia = chaveIdempotencia;
            Requisicao = requisicao;
            Resultado = resultado;
        }

        public Idempotencia() { }
    }
}
