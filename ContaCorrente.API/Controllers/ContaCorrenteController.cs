using ContaCorrente.Application.Commands.CriarConta;
using ContaCorrente.Application.Commands.EfetuarLogin;
using ContaCorrente.Application.Commands.InativarConta;
using ContaCorrente.Application.Queries.ConsultarSaldo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrente.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] CriarContaCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] EfetuarLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("inativar")]
        public async Task<IActionResult> Inativar([FromBody] InativarContaCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("saldo")]
        [Authorize]
        public async Task<IActionResult> ConsultarSaldo()
        {
            var cpf = User.FindFirst("cpf")?.Value;
            if (string.IsNullOrEmpty(cpf))
                return Unauthorized("Token não contém o CPF.");

            var saldo = await _mediator.Send(new ConsultarSaldoQuery(cpf));
            return Ok(new { saldo });
        }

    }
}
