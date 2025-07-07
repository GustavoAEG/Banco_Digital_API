using ContaCorrente.Application.Commands.CriarConta;
using ContaCorrente.Application.Commands.EfetuarLogin;
using ContaCorrente.Application.Commands.EfetuarTransferencia;
using ContaCorrente.Application.Commands.InativarConta;
using ContaCorrente.Application.Queries.ConsultarSaldo;
using ContaCorrente.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrente.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteRepository _contaRepository;
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IMediator _mediator;

        public ContaCorrenteController(
               IContaCorrenteRepository contaRepository,
               IMovimentoRepository movimentoRepository,
               IMediator mediator)
        {
            _contaRepository = contaRepository;
            _movimentoRepository = movimentoRepository;
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

        [HttpPost("transferir")]
        [Authorize]
        public async Task<IActionResult> Transferir([FromBody] EfetuarTransferenciaCommand command)
        {
            await _mediator.Send(command);
            return Ok("Transferência efetuada com sucesso.");
        }

        [HttpGet("transferencias")]
        [Authorize]
        public async Task<IActionResult> ListarTransferencias()
        {
            var cpf = User.FindFirst("cpf")?.Value;
            if (string.IsNullOrEmpty(cpf))
                return Unauthorized();

            var conta = await _contaRepository.ObterContaPorCpfAsync(cpf);
            var transferencias = await _movimentoRepository.ObterTransferenciasPorContaAsync(conta.Id);

            return Ok(transferencias);
        }

        [HttpGet("saldo")]
        public async Task<IActionResult> ConsultarSaldo()
        {
            var resultado = await _mediator.Send(new ConsultarSaldoQuery());
            return Ok(resultado);
        }
    }
}
