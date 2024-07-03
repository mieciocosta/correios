@BuscaCep
Feature: Busca CEP nos Correios

@sucesso
Scenario: Procurar CEP inexistente
  Given que estou na página inicial dos correios
  When procuro pelo CEP "80700000"
  Then o resultado de erro deve ser "Dados não encontrado"
  When volto para a página inicial dos correios

@sucesso
Scenario: Procurar CEP existente
  Given que estou na página inicial dos correios
  When procuro pelo CEP "01013001"
  Then o resultado deve ser "Rua Quinze de Novembro - lado ímpar"

@verificacoes
Scenario: Verificar elementos na página inicial dos correios
  Given que estou na página inicial dos correios
  Then realizo verificações utilizando diferentes métodos de localização
