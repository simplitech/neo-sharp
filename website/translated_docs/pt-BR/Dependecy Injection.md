---
id: dependency_injection
title: Injeção de Dependência
sidebar_label: Injeção de Dependência
---

Injeção de dependência dá ao projeto flexibilidade para adicionar e alterar código em seu projeto. O código não depende da implementação concreta, mas da interface, isso torna fácil de mudar o módulo caso seja necessário.

Se nós usamos interfaces em nossos projetos, reduziremos dependências entre classes, mas o problema que algum lugar do seu código precisa instânciar a classe concreta, e se isso não for feito corretamente, suas classes continuará dependendo da implementação concreta.

Melhor modo para evitar o alto acoplamento (precisa saber classe concreta), nós adicionar outro ator com responsabilidade para instanciar classes concretas, com isso os módulos não terá nenhuma assossiação com classe concreta, isso elmina qualquer dependência entre um módulo para outro.

Nós podemos resumir que injeção de dependência, é a responsabilidade de gerenciar as classes/objetos com outras classes.

Isso promove reusabilidade, testabilidade e a manutenção no projeto, porque você não precisa mais requerer implementação concreta, apenas de interfaces.

Em NEO-Sharp, nós usamos nosso próprio mecanismo de injeção de dependência, mas maioria das linguages de programação tem algo 'nativo' para isso.

Em NEO-Sharp, as dependências são inseridas no construtor do objeto, para isso funcionar é necessário registrar seus módulos, para saber qual classe de DI saber qual das implementações está disponível para cada interface.

Para mais informações sobre DI, por favor acessar [wiki](https://en.wikipedia.org/wiki/Dependency_injection).