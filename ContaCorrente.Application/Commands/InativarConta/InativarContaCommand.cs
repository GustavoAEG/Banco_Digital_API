using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Commands.InativarConta
{
    public class InativarContaCommand : IRequest<Unit>//Não retorna nada, pois estamos inativando o usuario, usamos o unit..
    {
        public string Senha { get; set; } = string.Empty;
    }
}
