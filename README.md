# ConsultersIT

## Descrição
O projeto **ConsultersIT** é uma aplicação modular desenvolvida em .NET, composta por várias camadas, incluindo API, Core, Common e Infra. Ele foi projetado para fornecer uma arquitetura limpa e escalável, facilitando a manutenção e a expansão do sistema.

## Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

- **ConsultersIT.API**: Contém a API principal do projeto, incluindo os controladores e a configuração do aplicativo.
  - Arquivos principais:
    - `Program.cs`: Configuração inicial da aplicação.
    - `appsettings.json`: Configurações gerais do aplicativo.
    - `Controllers/`: Contém os controladores da API, como o `TesteController.cs`.

- **ConsultersIT.Common**: Contém interfaces e modelos comuns que podem ser compartilhados entre diferentes camadas do projeto.
  - Subpastas:
    - `Interfaces/`: Define contratos como `ICepResponse` e `ITesteRepository`.
    - `ViewModels/`: Contém modelos de visualização, como `CargoViewModel.cs`.

- **ConsultersIT.Core**: Implementa a lógica de negócios principal do sistema.
  - Subpastas:
    - `Services/`: Contém serviços como `CepService` e `TesteService`.
    - `Interfaces/`: Define contratos específicos da lógica de negócios, como `ITesteService`.

- **ConsultersIT.Infra**: Gerencia a camada de infraestrutura, incluindo acesso a dados e repositórios.
  - Subpastas:
    - `Data/`: Contém o contexto de dados.
    - `Repositories/`: Implementa repositórios como `CargoRepository`.
    - `Interfaces/`: Define contratos de infraestrutura, como `IDbConnectionFactory`.

## Pré-requisitos

Certifique-se de ter os seguintes itens instalados no seu ambiente:

- .NET SDK (versão especificada no `global.json`)
- Um editor de código, como Visual Studio Code ou Rider
- Banco de dados configurado (se necessário)

## Configuração do Ambiente

1. Clone o repositório:
   ```bash
   git clone <url-do-repositorio>
   ```

2. Navegue até o diretório do projeto:
   ```bash
   cd ConsultersIT
   ```

3. Restaure as dependências:
   ```bash
   dotnet restore
   ```

4. Configure o arquivo `appsettings.Development.json` com as informações necessárias para o ambiente de desenvolvimento.

5. Execute o projeto:
   ```bash
   dotnet run --project src/ConsultersIT.API/ConsultersIT.API.csproj
   ```

## Estrutura de Pastas

```plaintext
ConsultersIT/
├── src/
│   ├── ConsultersIT.API/
│   │   ├── Controllers/
│   │   ├── Properties/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   ├── ConsultersIT.Common/
│   │   ├── Interfaces/
│   │   ├── ViewModels/
│   ├── ConsultersIT.Core/
│   │   ├── Services/
│   │   ├── Interfaces/
│   ├── ConsultersIT.Infra/
│   │   ├── Data/
│   │   ├── Repositories/
│   │   ├── Interfaces/
```

## Contribuição

1. Faça um fork do repositório.
2. Crie uma branch para sua feature ou correção:
   ```bash
   git checkout -b minha-feature
   ```
3. Faça commit das suas alterações:
   ```bash
   git commit -m "Minha nova feature"
   ```
4. Envie para o repositório remoto:
   ```bash
   git push origin minha-feature
   ```
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).