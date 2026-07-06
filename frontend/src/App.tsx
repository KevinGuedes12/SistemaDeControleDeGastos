import { useCallback, useEffect, useState } from 'react'
import { api } from './api'
import { PessoasView } from './components/PessoasView'
import { TotaisView } from './components/TotaisView'
import { TransacoesView } from './components/TransacoesView'
import type { Pessoa, Totais, Transacao, TransacaoTipo } from './types'
import './App.css'

type ActiveView = 'pessoas' | 'transacoes' | 'totais'

function App() {
  const [activeView, setActiveView] = useState<ActiveView>('pessoas')
  const [pessoas, setPessoas] = useState<Pessoa[]>([])
  const [transacoes, setTransacoes] = useState<Transacao[]>([])
  const [totais, setTotais] = useState<Totais | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  const loadData = useCallback(async () => {
    setLoading(true)
    setError(null)

    try {
      const [pessoasData, transacoesData, totaisData] = await Promise.all([
        api.listarPessoas(),
        api.listarTransacoes(),
        api.obterTotais(),
      ])

      setPessoas(pessoasData)
      setTransacoes(transacoesData)
      setTotais(totaisData)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Nao foi possivel carregar os dados.')
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    void loadData()
  }, [loadData])

  async function handleCreatePessoa(payload: { nome: string; idade: number }) {
    setError(null)

    try {
      await api.criarPessoa(payload)
      await loadData()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Nao foi possivel cadastrar a pessoa.')
    }
  }

  async function handleDeletePessoa(id: string) {
    const confirmed = window.confirm('Excluir esta pessoa e suas transacoes?')
    if (!confirmed) {
      return
    }

    setError(null)

    try {
      await api.deletarPessoa(id)
      await loadData()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Nao foi possivel excluir a pessoa.')
    }
  }

  async function handleCreateTransacao(payload: {
    descricao: string
    valor: number
    tipo: TransacaoTipo
    pessoaId: string
  }) {
    setError(null)

    try {
      await api.criarTransacao(payload)
      await loadData()
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Nao foi possivel cadastrar a transacao.')
    }
  }

  return (
    <div className="app-shell">
      <header className="app-header">
        <div className="header-inner">
          <div className="brand-block">
            <h1>Controle de gastos residenciais</h1>
            <p>{loading ? 'Carregando...' : `${pessoas.length} pessoas, ${transacoes.length} transacoes`}</p>
          </div>

          <nav className="tabs" aria-label="Navegacao principal">
            <button
              className={`tab-button ${activeView === 'pessoas' ? 'active' : ''}`}
              type="button"
              onClick={() => setActiveView('pessoas')}
            >
              Pessoas
            </button>
            <button
              className={`tab-button ${activeView === 'transacoes' ? 'active' : ''}`}
              type="button"
              onClick={() => setActiveView('transacoes')}
            >
              Transacoes
            </button>
            <button
              className={`tab-button ${activeView === 'totais' ? 'active' : ''}`}
              type="button"
              onClick={() => setActiveView('totais')}
            >
              Totais
            </button>
          </nav>
        </div>
      </header>

      <main className="app-main">
        {error && <div className="notice">{error}</div>}

        {activeView === 'pessoas' && (
          <PessoasView
            pessoas={pessoas}
            loading={loading}
            onCreate={handleCreatePessoa}
            onDelete={handleDeletePessoa}
          />
        )}
        {activeView === 'transacoes' && (
          <TransacoesView pessoas={pessoas} transacoes={transacoes} onCreate={handleCreateTransacao} />
        )}
        {activeView === 'totais' && <TotaisView totais={totais} />}
      </main>
    </div>
  )
}

export default App
