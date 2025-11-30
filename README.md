# datadog-metrics
Criando métricas Datadog a partir de uma aplicação .net8

## Registre a API Key como variável de ambiente
A sua conta no Datadog tem uma API Key, que pode ser consultada em https://app.datadoghq.com/organization-settings/api-keys

Ela é imprescindível para o funcionamento correto do Datadog agent.

Grave-a como variável de ambiente, evitando o vazamento de dados sensíveis:

```
// Linux ou git bash
$> export DD_API_KEY=aaaabbbbccccddddeeeeffffgggghhhh

// cmd no Windows
c:\> set DD_API_KEY=aaaabbbbccccddddeeeeffffgggghhhh
```
Pra consultar:

```
// Linux ou git bash
$> echo $DD_API_KEY

// cmd no Windows
c:\> echo %DD_API_KEY%
```

<!-- ## Provisionando o Datadog agent via Docker
```
cd datadog-agent
docker-compose up --build
```

## Provisionando a aplicação via Docker
```
cd ConsoleMetrics
docker-compose up --build
``` -->

## Provisionando o Datadog agent + aplicação
```
docker-compose up --build
```
O console deverá escrever um ponto "." a cada 1 segundo, indicando a gravação das métricas no Datadog.

---



Problemas ao rodar docker-compose?
```
docker system prune -a
```