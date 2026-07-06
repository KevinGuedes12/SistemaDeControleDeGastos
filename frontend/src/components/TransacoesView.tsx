import { useState } from 'react'
import type { FormEvent } from 'react'
import type { Pessoa, Transacao, TransacaoTipo } from '../types'
import { formatCurrency } from '../format'

interface TransacoesViewProps {
  pessoas: Pessoa[]
  transacoes: Transacao[]
  onCreate: (payload: {
    descricao: string
    valor: number
    tipo: TransacaoTipo
    pessoaId: string
  }) => Promise<void>
}

export function TransacoesView({ pessoas, transacoes, onCreate }: TransacoesViewProps) {
  const [descricao, setDescricao] = useState('')
  const [valor, setValor] = useState('')
  const [tipo, setTipo] = useState<TransacaoTipo>('Despesa')
  const [pessoaId, setPessoaId] = useState('')
  const [submitting, setSubmitting] = useState(false)

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setSubmitting(true)

    try {
      await onCreate({ descricao, valor: Number(valor), tipo, pessoaId })
      setDescricao('')
      setValor('')
      setTipo('Despesa')
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <section className="workspace-grid">
      <form className="panel form-panel" onSubmit={handleSubmit}>
        <div className="panel-header">
          <h2>Cadastrar transacao</h2>
        </div>

        <label>
          Pessoa
          <select value={pessoaId} onChange={(event) => setPessoaId(event.target.value)} required>
            <option value="">Selecione</option>
            {pessoas.map((pessoa) => (
              <option key={pessoa.id} value={pessoa.id}>
                {pessoa.nome}
              </option>
            ))}
          </select>
        </label>

        <label>
          Descricao
          <input
            value={descricao}
            onChange={(event) => setDescricao(event.target.value)}
            required
            maxLength={180}
          />
        </label>

        <label>
          Valor
          <input
            value={valor}
            onChange={(event) => setValor(event.target.value)}
            required
            min={0.01}
            step={0.01}
            type="number"
          />
        </label>

        <label>
          Tipo
          <select value={tipo} onChange={(event) => setTipo(event.target.value as TransacaoTipo)} required>
            <option value="Despesa">Despesa</option>
            <option value="Receita">Receita</option>
          </select>
        </label>

        <button className="primary-button" type="submit" disabled={submitting || !pessoas.length}>
          {submitting ? 'Salvando...' : 'Salvar transacao'}
        </button>
      </form>

      <section className="panel table-panel">
        <div className="panel-header">
          <h2>Transacoes</h2>
          <span>{transacoes.length}</span>
        </div>

        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>Descricao</th>
                <th>Pessoa</th>
                <th>Tipo</th>
                <th>Valor</th>
              </tr>
            </thead>
            <tbody>
              {transacoes.map((transacao) => (
                <tr key={transacao.id}>
                  <td>{transacao.descricao}</td>
                  <td>{transacao.pessoaNome}</td>
                  <td>
                    <span className={`status-pill ${transacao.tipo === 'Receita' ? 'income' : 'expense'}`}>
                      {transacao.tipo}
                    </span>
                  </td>
                  <td>{formatCurrency(transacao.valor)}</td>
                </tr>
              ))}
              {!transacoes.length && (
                <tr>
                  <td colSpan={4} className="empty-state">
                    Nenhuma transacao cadastrada.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </section>
    </section>
  )
}
