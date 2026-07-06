import type { Totais } from '../types'
import { formatCurrency } from '../format'

interface TotaisViewProps {
  totais: Totais | null
}

export function TotaisView({ totais }: TotaisViewProps) {
  return (
    <section className="totals-layout">
      <section className="panel table-panel">
        <div className="panel-header">
          <h2>Totais por pessoa</h2>
          <span>{totais?.pessoas.length ?? 0}</span>
        </div>

        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>Nome</th>
                <th>Receitas</th>
                <th>Despesas</th>
                <th>Saldo</th>
              </tr>
            </thead>
            <tbody>
              {totais?.pessoas.map((pessoa) => (
                <tr key={pessoa.pessoaId}>
                  <td>{pessoa.nome}</td>
                  <td>{formatCurrency(pessoa.totalReceitas)}</td>
                  <td>{formatCurrency(pessoa.totalDespesas)}</td>
                  <td className={pessoa.saldo >= 0 ? 'positive-value' : 'negative-value'}>
                    {formatCurrency(pessoa.saldo)}
                  </td>
                </tr>
              ))}
              {!totais?.pessoas.length && (
                <tr>
                  <td colSpan={4} className="empty-state">
                    Nenhum total disponivel.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </section>

      <section className="summary-row">
        <div className="summary-tile income-tile">
          <span>Receitas</span>
          <strong>{formatCurrency(totais?.geral.totalReceitas ?? 0)}</strong>
        </div>
        <div className="summary-tile expense-tile">
          <span>Despesas</span>
          <strong>{formatCurrency(totais?.geral.totalDespesas ?? 0)}</strong>
        </div>
        <div className="summary-tile balance-tile">
          <span>Saldo liquido</span>
          <strong>{formatCurrency(totais?.geral.saldoLiquido ?? 0)}</strong>
        </div>
      </section>
    </section>
  )
}
