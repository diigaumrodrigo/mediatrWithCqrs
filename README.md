<div align="center">
  <h1>CQRS com MediatR</h1>
</div>

Simples implementação do pattern mediator em .Net Core

## Comandos Docker

```
docker build -t my-database --build-arg SERVER_HOST=<server> --build-arg USER=<user> --build-arg SA_PASSWORD=<password> --build-arg DATABASE=Vendas .
```

```
docker run --name temp-db -d --rm -p 1433:1433 my-database
```

## Code Coverage

Executar o script `test_and_cover.ps1`, será gerado o arquivo `index.html` na pasta .coverageReport