---
id: documentation
title: Documentação
sidebar_label: Documentação
---

A documentação deste site está disponível para contribuições, caso você queira contribuir segue os passos.

## NPM

### MacOS
- Caso você não possua o node, por favor baixe e instale através do https://nodejs.org/en/
- Abra o terminal e acesse a pasta neo-sharp/website
- Instale os componentes com o comando 'npm i'
- Para rodar localmente, rode o comando npm start

## Menu 
- Para cada página que você vai criar crie um arquivo .md na pasta neo-sharp/docs
	- Ao criar um arquivo adicione um id, title e o sidebar_label. O id deverá ser único no projeto, serve para ancorar e linkar a página 
	- Caso esteja criando um arquivo .md novo, adicione também nas pastas de outras linguas (neo-sharp/website/translated_docs/lingua/).
- Altere o arquivo neo-sharp/website/sidebars.json para adicionar ou remover do menu.
	- Utilize o id que você criou anteriormente para a sua página.

## Barra de navegação
- Altere o arquivo neo-sharp/website/siteConfig.js no tag headerLinks

## Tradução
- Para ativar ou inativar um idioma, edite o arquivo neo-sharp/website/languages.js e altere o atributo enabled.
- Crie uma pasta do idioma habilitado em neo-sharp/website/translated_docs e copie todos os arquivos da pasta neo-sharp/docs e traduza o conteúdo dos arquivos exceto o id 
- Copie o arquivo en.json que está na pasta neo-sharp/website/i18n e nomeie para o idioma que está sendo habilitado e traduza o conteúdo
- Ao rodar localmente o en.json será sempre atualizado de acordo com o conteúdo, então fique atento para que todos os idiomas tenham todas as chaves.


Para mais informações acesse o [docusaurus](https://docusaurus.io/en/)