# Projeto de Gerenciamento Escolar - Teste Ponto ID

**Versão do ASP.NET**: 5.0.5

## Projetos

Api: Projeto no qual contém a API em si, para que realize a comunicação do sistema.

Biblioteca: Contém os migrations, models, Repositório e o BancoContext.

MVC: Contém a aplicação WEB, onde podemos navegar por meio das Views e controllers.

## Orientações

1. Baixe o repositório.
2. Abra a solução do projeto que está na pasta.
3. Configure a string de connection no arquivo 'appsettings.json', no projeto API e MVC.
4. Abra o terminal de pacotes NuGet.
5. Selecione o projeto 'Biblioteca'.
6. Execute o comando 'add-migration' e informe o nome.
7. Execute o comando 'update-database' para criar o banco.
8. Defina os projetos de inicialização clicando com o botão direito do mouse na solução e encontrando a opção "Definir projetos de inicialização".
9. Clique em Iniciar.
10. Adicione as informações de escola, turmas e alunos.