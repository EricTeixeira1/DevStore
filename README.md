# DevStore - WEB API Para Controle de Produtos e Pedidos

Aplicação desenvolvida em .NET 9 com o intuito de assimilar conceitos sobre boas práticas e padrões de design na construção de sistemas que facilitam a manutenção e adição de novas funcionalidades.

---

## Principais tecnologias utilizadas

- .Net 9
- Entity Framework Core
- MediatR
- SQL Server
- Docker
- Swagger

## Abordagens utilizadas que moldam o design da aplicação

- Separação em camadas
- DDD
- Unit of work
- Repositórios
- CQRS

---

## Decisões de arquitetura

O projeto foi pensado inicialmente como um monolito modular visando a simplicidade ao dividir os módulos em serviços independentes, caso seja necessário futuramente.

A divisão dos módulos está organizada em 3 camadas, seguindo a ordem de dependência infra -> aplicação -> domínio, mantendo as regras de negócio centralizadas e isoladas na camada de domínio. Essa abordagem desacopla o domínio das implementações que o consomem, dando liberdade para que funcionalidades e arquitetura evoluam de forma independente, tendo em vista que constantemente novas tecnologias surgem para melhorar o desempenho de aplicações e que esses aperfeiçoamentos geralmente estão relacionados à arquitetura, havendo pouca ou nenhuma necessidade de alteração no núcleo/domínio.

Há apenas um módulo englobando as agregações Produto e Pedido para efeito de simplicidade, mas cada uma delas poderia ter o seu prórpio módulo.

- Aplicação
  - Orquestra os objetos de domínio para executar as funcionalidades do sistema.
- Domínio
  - Contém as regras de negócio que definem as operações realizadas pela aplicação ao executar uma funcionalidade.
- Infra
  - Agrega a comunicação com recursos externos que dão suporte às funcionalidades da aplicação.
- Núcleo compartilhado
  - Centraliza recursos que são necessários em múltiplos módulos ou camadas.

![image](https://github.com/user-attachments/assets/21538b8d-5ef2-4e27-b5b7-31191c5130a9)


---

## Como executar o projeto

A API pode ser executada pelo Visual Studio ou Docker.

### Visual Studio

Requisitos:
- Visual Studio 2022
- .Net 9
- EF Core CLI
- Banco de dados SQL Server 2022
- Selecionar o projeto DevStore.Web como Startup.

Executar migration:
```
dotnet ef database update --startup-project .\src\DevStore.Web\DevStore.Web.csproj -p .\src\DevStore.Infra\DevStore.Infra.csproj --context=AppDbContext --verbose
```

Executar o projeto pressionando F5.

### Docker

Requisitos:
- Docker Desktop
- EF Core CLI

Navegar para a pasta /docker na raíz do projeto e executar o seguinte comando:

```
docker-compose up
```

Executar migration:
```
dotnet ef database update --startup-project .\src\DevStore.Web\DevStore.Web.csproj -p .\src\DevStore.Infra\DevStore.Infra.csproj --context=AppDbContext --connection "Server=localhost,1433;Database=DevStore;MultipleActiveResultSets=true;User Id=sa;Password=MyDB@12345678;TrustServerCertificate=True;" --verbose
```

---

## Potenciais melhorias

A lista abaixo apenas representa algumas implementações que seriam interessantes para ter uma solução básica completa. Em termos de um sistema candidato a ir para produção, mais recursos precisam ser adicionados.

- Separação de Produtos e Pedidos em seus respectivos módulos contendo App, Domínio e Infra.
- Validação de entradas no contexto de Pedidos. Apenas Produtos foi validado.
- Refatoração do módulo de Pedidos com serviços ou comandos e queries na camada de aplicação, assim como foi feito com Produtos.
- Tratamento global de exceções.
- Testes.
