using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaCorrente.Application.Services
{
    public interface ITokenService
    {
        string GerarToken(ContaCorrente.Domain.Entities.ContaCorrente conta);
    }

}
