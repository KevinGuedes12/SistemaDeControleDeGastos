import type { ApiError, Pessoa, Totais, Transacao, TransacaoTipo } from './types'

const API_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5085/api'

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${API_URL}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...init?.headers,
    },
    ...init,
  })

  if (!response.ok) {
    let apiError: ApiError | null = null

    try {
      apiError = (await response.json()) as ApiError
    } catch {
      apiError = null
    }

    const detalhes = apiError?.detalhes?.length ? ` ${apiError.detalhes.join(' ')}` : ''
    throw new Error(apiError?.mensagem ? `${apiError.mensagem}${detalhes}` : 'Erro ao acessar a API.')
  }

  if (response.status === 204) {
    return undefined as T
  }

  return response.json() as Promise<T>
}

export const api = {
  listarPessoas: () => request<Pessoa[]>('/pessoas'),
  criarPessoa: (payload: { nome: string; idade: number }) =>
    request<Pessoa>('/pessoas', {
      method: 'POST',
      body: JSON.stringify(payload),
    }),
  deletarPessoa: (id: string) =>
    request<void>(`/pessoas/${id}`, {
      method: 'DELETE',
    }),
  listarTransacoes: () => request<Transacao[]>('/transacoes'),
  criarTransacao: (payload: {
    descricao: string
    valor: number
    tipo: TransacaoTipo
    pessoaId: string
  }) =>
    request<Transacao>('/transacoes', {
      method: 'POST',
      body: JSON.stringify(payload),
    }),
  obterTotais: () => request<Totais>('/totais'),
}
