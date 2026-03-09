🕹️ Game Store Inventory API
Uma API robusta para gerenciamento de "cardápio" e estoque de uma loja de games, desenvolvida em C# e .NET. O sistema permite o controle total de produtos, desde a classificação indicativa até o status de disponibilidade comercial, protegendo operações sensíveis através de autenticação moderna.
O projeto foi planejado, desenvolvido e documentado em apenas uma semana.

🛡️ Diferencial: Segurança com JWT
A API utiliza JSON Web Token (JWT) para garantir que apenas usuários autorizados (administradores da loja) possam realizar alterações no catálogo.
Autenticação: Login seguro para geração de tokens.
Autorização: Proteção de endpoints de Escrita (POST, PUT, DELETE).
Integridade: Garante que o status de um jogo ("Disponível" vs "Descontinuado") não seja alterado por usuários não autenticados.

📋 Regras de Negócio e Entidades
A API organiza os jogos com foco em operação de venda:
Status de Estoque: Gerenciamento entre Disponível, Indisponível e Descontinuado.
Ficha Técnica: Cada jogo contém Faixa Etária, Gênero e Plataforma.
Filtros de Vitrine: Estrutura preparada para alimentar um front-end de e-commerce.

🛠️ Stack Técnica
Linguagem: C#
Framework: .NET (v8.0+)
Segurança: Microsoft.AspNetCore.Authentication.JwtBearer
Documentação: Swagger (Configurado para suportar o envio do Token Bearer).

🚀 Como Executar
Clone o repositório: git clone ...
Configure sua JwtKey no appsettings.json (ou utilize variáveis de ambiente).
Execute dotnet run.
No Swagger, utilize o botão "Authorize" para inserir seu token e testar os endpoints protegidos.
