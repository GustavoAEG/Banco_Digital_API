using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Entities
{
    public class ContaCorrente
    {
        public Guid Id { get; private set; } 
        public int Numero { get; private set; }
        public string Cpf { get; private set; } = string.Empty;
        public string Nome { get; private set; } = string.Empty;
        public bool Ativo { get; private set; } = false;
        public string Senha { get; private set; } = string.Empty;
        public string Salt { get; private set; } = string.Empty;

        public ContaCorrente(Guid id,string cpf, int numero, string nome, string senha, string salt, bool ativo = false)
        {
            Id = id;
            Numero = numero;
            Cpf = cpf;
            Nome = nome;
            Senha = senha;
            Salt = salt;
            Ativo = ativo;
        }

        public void Ativar() => Ativo = true;
        public void Inativar() => Ativo = false;


    }
}
