---
id: git
title: GIT / GITHUB
sidebar_label: GIT / GITHUB
---

NEO-Sharp usa Git para controle de versão, para interagir com o projeto, você precisa instalar primeiro.

O próximo passo para contribuir com o projeto de código aberto é fazer o fork do repositório que você quer colaborar. Quando faz o fork do projeto, você cria sua própria cópia do projeto, significa que você pode fazer alterações sem afetar o projeto original.

Fazer o fork é necessário para propor alterações no projeto original.
Se você é novo no Git, nós sugerimos para utilizar ferramente visual do que linhas de comando.

## Instalação de Git
Para instalar o Git, por favor, seguir as seguintes instruções descritas em: https://help.github.com/articles/set-up-git/

## Fork
Fork é o primeiro passo para começar a colaborar. Fazer o fork é necessário para propor alterações no projeto original.

After forking it, you will notice that your new project is related to the original one https://help.github.com/articles/fork-a-repo/

## Clone
Cloning is similar to forking, but the main difference is that cloning does not create a new project, it just copy the files instead. 
Clone your repository using the url provided in the "Clone or Download"  button.
Remember to clone your version of the project, so you can make changes to it. 
Cloning someone else project can be useful if you use it for referencing, but if you plan to make any changes, fork it and clone your version of the project.

## Adding a new remote
When you collaborate with open source project, you usually have to deal with at least two copies of the project: the one you own and the original project. 

To do this, you must add another 'remote'  to your local git.
You use remotes to reference the sibling of your project, so don't forget to add https://github.com/CityOfZion/neo-sharp.git as one of your remotes! 

Adding a remote can vary depending on the tool you are using, but they all only require two inputs: the URL of the remote, and how do you want to call it in your machine. Usually 'origin' is used for the project original remote.

In this example, we have 3 remotes: CoZ, origin and shargon. They are related repositories, but are not the same.

## Branchs
Branchs are variation of the code in the same repository. In NEO-Sharp we work with two branchs: the master branch, and the development branch. 

This is useful because you can have one branch for the production code (usually 'master'), and another for the the development version (usually 'development'). 

When you propose changes, you should always propose  changes to the development branch. This is done using a pull request.

Changing the version of your code (to another branch) is called Checkout.

For more information about good practices using git, refer to https://danielkummer.github.io/git-flow-cheatsheet/

## Commit
After the project and branchs are setup, you can make changes to it. When you are done and want to persist the changes you made, you commit your code. Creating a commit can be understood as taking a snapshot of the current code. You can return to this commit later if you need to. https://developer.github.com/v3/git/commits/

## Push
When you commit some code, you are only creating a snapshot. To update the code in your remote, you have to push your changes to it. If you are getting a permission error when pushing, it's probably because you are trying to push to the wrong remote.

## Pull and Rebase
If you are working in a project with other developers, like NEO-Sharp, it's necessary to have your code updated whenever someone else changes the code (usually by merging a pull request).

You can update your code using  Pull or Rebase, the main difference is "where do you want to put the changes that you just received". 
When you use Pull, you are adding the changes into your latest commit, and Rebase when you want to add these changes before your first commit (that is why the 'Rebase' in the name)

You must use Rebase instead of Pull because you want to make your changes 'derive'  from latest version of the original project.
Using rebase changes the history of your branch, because you are litteraly changing the 'past' of your project, so to send this changes, you have to make a 'forced push' (git push --force)

Pull and Rebase can be tricky, so extra research on this subject might be required.

## Pull Request
A Pull Request is used whenever you want to send your changes to the original project. 

To create a pull request, go to your GitHub project page,  open the Pull Request tab and click "New Pull Request Button".

In the next screen, select the Development branch in NEO-Sharp, and in the right, the branch you added your changes.

If you get a message saying that this branch cannot be merged, remember to rebase your repository.

## How to use Git to collaborate with NEO-Sharp
- Fork the project 
- Clone the forked project in your machine
- Add a new remote using the oficial project URL
- Create a new branch in your repository using the name of the feature you want to implement, for example:
feature/open-wallet 
	- It's a good practice to create a branch for every new feature you want to add, instead of working in a single branch
- Make the desired changes and commit it
	- Remember, [unit testing](unit_test) is highly enforced in this project, so avoid sending any pull request without proper unit testing.
- Rebase your repository using the official project remote
- Push your changes to your repository (a force push might be required)
- Open GitHub website, click PullRequests and Create  Pull Request
- Select the Development branch from the official project (in the left), and your feature branch in the right and click "Create Pull Request".
- Follow the discussions on your pull request and perform changes required by the team. The required changes can vary from very simple changes, to more complex ones. 

Every time you push changes to the branch you want to merge, the pull request will be automatically updated.