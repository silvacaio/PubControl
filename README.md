# PubControl

Primeiro passo para o desenvolvimento do teste foi entender as solicitações e os requisitos que estavam descritos na documentação.
Para inicio de desenvolvimento, foi escolhido o framework .net 2.1. 
Seguindo uma arquitetura que já tenho o domínio, inicialmente foi realizada toda a estruração da sln, a fim de se organizar de maneira satisfatória a solução.
Com base nas orientações do DDD, o projeto foi iniciado através da modelagem dos domínios ricos. Foi utilizado o padrão de Factories nos dominios, para garantir a criação de modelos  validos.
O segundo projeto a ser desenvolvido, foi o Application. Este projeto contém algumas regras de negócios especificas deste projeto. Exemplo das promoções, onde tendo isto nesta camada, ficaria flexivel o desenvolvimento de promoções de outros estabelecimentos.
Após, foram desenvolvidos os projetos auxilires (Ioc, Identity e Data).
Para autentcação, foi utilizado o Identity junto com o JWT Token.
Em seguida, foi desenvolvida a API. Tendo a base do backend pronto, foi realizada a implementação dos testes untários.

Para o front-end, foi escolhido o Angular. Onde foi estrutura um novo projeto de front-end.
Este é um breve resumo do processo de desenvolvimento aplicado neste teste.

Pontos de evolução:

Utilização de um micro-orm no back end (Dapper):
  Para as operações de consultas, isto seria uma ótima evolução, visto que o EF Core escreve algumas queries não tão interessantes.
  
Criação de um sistema de gerenciamento de várias comandas:
  Hoje, só é possível ter uma comanda ativa por vez. Não foi desenvolvido este gerenciador devido ao tempo limitado.
  
 Evolução do front-end:
  Seria possível implementar mais validações no front end, realizar a formatação dos valores númericos e talvez utilizar o material design para deixar mais "bonito".
  
  Testes: 
    Possível aplicar mais testes unitários (o tempo limitado não possibilitou isto).
  
  
