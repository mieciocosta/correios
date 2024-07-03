@Rastreio
Feature: Rastreio de Código nos Correios

@sucesso
Scenario: Rastrear código inexistente
  Given que estou na página inicial de rastreamento
  When procuro pelo código de rastreamento "SS987654321BR"
  Then o resultado do rastreamento deve ser "O rastreamento não está disponível no momento"

@verificacoes
Scenario: Verificar elementos na página inicial de rastreamento
  Given que estou na página inicial de rastreamento
  Then realizo verificações utilizando diferentes métodos de localização para rastreamento
