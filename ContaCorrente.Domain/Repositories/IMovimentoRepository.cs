using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContaCorrente.Domain.Entities;

namespace ContaCorrente.Domain.Repositories
{
    public interface IMovimentoRepository
    {
        Task RegistrarTransferenciaAsync(Transferencia transferencia);

    }
}
