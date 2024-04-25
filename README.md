API de Cadastro, Autenticação e Redefinição de Senha por E-mail

Esta é uma API desenvolvida em C# que oferece funcionalidades robustas de cadastro, autenticação e redefinição de senha por e-mail para usuários. O projeto foi criado por Matheus Lauxen.
Requisitos

    .NET 8.0
    SQL Server

Funcionalidades

    Cadastro de Usuário: Permite que novos usuários se cadastrem fornecendo informações básicas como e-mail, senha, nome, etc.

    Autenticação: Oferece um mecanismo seguro para autenticar usuários, geralmente utilizando tokens JWT (JSON Web Tokens) para autorização.

    Redefinição de Senha por E-mail: Permite que usuários solicitem a redefinição de senha por e-mail. Uma nova senha é gerada e enviada para o usuário, e essa nova senha é automaticamente salva no banco de dados. Além disso, a senha armazenada no banco de dados é protegida usando hash para garantir a segurança das informações dos usuários.

Como Usar

    Configuração do Ambiente:
        Certifique-se de ter o .NET 8.0 instalado em sua máquina.
        Configure um servidor SQL Server para armazenar os dados dos usuários.

    Configuração da API:
        Clone este repositório em sua máquina.
        Configure as credenciais do SQL Server no arquivo de configuração da API.

    Executando a API:
        Compile e execute o projeto da API em seu ambiente de desenvolvimento.

    Testando as Funcionalidades:
        Utilize ferramentas como Postman ou cURL para enviar requisições HTTP para a API e testar as diferentes funcionalidades.

Contato

Para mais informações ou suporte, entre em contato com o autor:

    E-mail: matheusvitorlauxen6@gmail.com
    Telefone: 69 993811973
