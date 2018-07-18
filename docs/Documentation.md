---
id: documentation
title: Documentation
sidebar_label: Documentation
---

You can contribute this documentions, following the step:

## NPM

### MacOS
- If you don't have a node, please download and install following https://nodejs.org/en/
- Open terminal and access your neo-sharp/website folder
- Install modules using 'npm i'
- To build and serve the website from a local server use 'npm start'

## SideBar
- For each page you need to create .md file on neo-sharp/docs folder
	- When creating a file add 'id', 'title', 'sidebar_label'. The id need to be unique on project.
	- Please add .md files to another languages folder. (neo-sharp/website/translated_docs/languageCode).
- Change neo-sharp/website/sidebars.json file for add or remove a new item on sidebar.
	- Use the id of your page.

## Navigation bar
- Change neo-sharp/website/siteConfig.js file, add new item in tag 'headerLinks'

## Translator
- For activate or inactivate, edit neo-sharp/website/languages.js file and change attribute 'enabled'.
- Create a folder for new language into neo-sharp/website/translated_docs and copy all files from neo-sharp/docs and translate the content of files except the id 
- Copy en.json file to neo-sharp/website/i18n and change file name to language being enabled and translate the contets.
- When you build, its always file en.json updated the content, so be careful to all language have the same keys.


For more information, please access [docusaurus](https://docusaurus.io/en/)