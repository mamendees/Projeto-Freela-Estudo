# Objetivo
 -  A Plataforma Freelancer oferece a ligação entre solicitante e freelancers. O Objetivo deste projeto é puramente estudo, de rever tecnologias conhecidas e conhecer algumas novas, como NSubstitute (vide problemas do Moq).
 -  OBS*: Projeto principal, seguindo os patterns é o Freelancer.Api, o projeto vinculado Freelancer.Payment.Api foi vinculado apenas para firmar e testar os conceitos de mensageria (fila, topicos, pub/sub) entre serviços diferentes e deixar centralizado em um mesmo repositorio do git.

# Domínio
 - Clientes (podendo contratar freelancers para seus projetos)
 - Freelancers (com n skills, podendo ser contratado para n projetos)

# Requisitos
### Freelancer
 - Deve visualizar projetos gerais
 - Deve se vincular a x projetos
 - Deve atualizar os projetos em que atua
 - Deve finalizar projeto
 - Deve cadastrar skills
 - Deve vender seus serviços
 - Deve adicionar comentarios aos projetos
 - Deve receber pelo projeto

### Cliente
 - Deve contratar serviços
 - Deve adicionar comentarios aos projetos
 - Deve criar e definir um projeto
 - Deve atualizar projeto
 - Deve finalizar projeto
 - Deve pagar pelo projeto

# Tecnologias e padrões utilizados
 - ASP.NET Core 7.0
 - C# 11
 - Arquitetura Limpa
 - CQRS
 - Repository, Unit of Work e Imediator
 - FluentValidation
 - Xunit com NSubstitute
 - RabbitMQ (filas e tópicos)
 - Autenticação e Autorização (JWT)
 - EF e Dapper

# Futuro
 - Subir em container (Docker)
 - Add front End (angular)
