namespace ContaCorrente.Application.Queries.GetSaldo;

public class GetSaldoResult
{
    public int NumeroConta { get; set; }
    public string NomeTitular { get; set; } = string.Empty;
    public DateTime DataHoraConsulta { get; set; }
    public decimal SaldoAtual { get; set; }
}
