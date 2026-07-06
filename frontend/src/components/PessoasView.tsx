import { useState } from 'react'
import type { FormEvent } from 'react'
import type { Pessoa } from '../types'

interface PessoasViewProps {
  pessoas: Pessoa[]
  loading: boolean
  onCreate: (payload: { nome: string; idade: number }) => Promise<void>
  onDelete: (id: string) => Promise<void>
}

export function PessoasView({ pessoas, loading, onCreate, onDelete }: PessoasViewProps) {
  const [nome, setNome] = useState('')
  const [idade, setIdade] = useState('')
  const [submitting, setSubmitting] = useState(false)

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setSubmitting(true)

    try {
      await onCreate({ nome, idade: Number(idade) })
      setNome('')
      setIdade('')
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <section className="workspace-grid">
      <form className="panel form-panel" onSubmit={handleSubmit}>
        <div className="panel-header">
          <h2>Cadastrar pessoa</h2>
        </div>

        <label>
          Nome
          <input value={nome} onChange={(event) => setNome(event.target.value)} required maxLength={120} />
        </label>

        <label>
          Idade
          <input
            value={idade}
            onChange={(event) => setIdade(event.target.value)}
            required
            min={0}
            max={150}
            type="number"
          />
        </label>

        <button className="primary-button" type="submit" disabled={submitting}>
          {submitting ? 'Salvando...' : 'Salvar pessoa'}
        </button>
      </form>

      <section className="panel table-panel">
        <div className="panel-header">
          <h2>Pessoas</h2>
          <span>{pessoas.length}</span>
        </div>

        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>Nome</th>
                <th>Idade</th>
                <th>Acoes</th>
              </tr>
            </thead>
            <tbody>
              {pessoas.map((pessoa) => (
                <tr key={pessoa.id}>
                  <td>{pessoa.nome}</td>
                  <td>{pessoa.idade}</td>
                  <td>
                    <button
                      className="danger-button"
                      type="button"
                      onClick={() => onDelete(pessoa.id)}
                      disabled={loading}
                    >
                      Excluir
                    </button>
                  </td>
                </tr>
              ))}
              {!pessoas.length && (
                <tr>
                  <td colSpan={3} className="empty-state">
                    Nenhuma pessoa cadastrada.
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
