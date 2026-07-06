export type TransacaoTipo = 'Despesa' | 'Receita'

export interface Pessoa {
  id: string
  nome: string
  idade: number
}

export interface Transacao {
  id: string
  descricao: string
  valor: number
  tipo: TransacaoTipo
  pessoaId: string
  pessoaNome: string
}

export interface PessoaTotais {
  pessoaId: string
  nome: string
  totalReceitas: number
  totalDespesas: number
  saldo: number
}

export interface ResumoTotais {
  totalReceitas: number
  totalDespesas: number
  saldoLiquido: number
}

export interface Totais {
  pessoas: PessoaTotais[]
  geral: ResumoTotais
}

export interface ApiError {
  mensagem?: string
  detalhes?: string[]
}
