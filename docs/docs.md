---
id: docs
title: Introduction
sidebar_label: Introduction
---

## NEO Technology

NEO is the use of blockchain technology and digital identity to digitize assets, the use of smart contracts for digital assets to be self-managed, to achieve "smart economy" with a distributed network.
Neo-sharp is a new core and node implementation of NEO with three main project goals:

* Break interdependencies in NEO and allow plug-able modular design to be used by node implementations;
* Create testing infrastructure for both specification behavior and benchmark of modifications;
* Develop a fully compatible version and new experimental high performance module for each component;
* This project is not about adding new features to NEO, it’s about establishing and adhering coding best practices, implementing proper design patters and ensuring the code is testable with a focus on unit test code coverage.

The project runs on .NET Core and it's compatible with Windows, Linux and MacOS. This documentation is related to neo-sharp development, if you are looking for Smart Contract development, please refer [here](http://docs.neo.org/en-us/sc/introduction.html).

### You are invited to contribute

Neo-sharp is community driven project, meaning that your contribution is more than welcome.
You can build new code, help with unit testing and improving this documentation. We are waiting for you in [Discord](http://discord.com) and [GitHub](https://github.com/CityOfZion/neo-sharp)!

This project aims to be accessible to all kind of people, so if you are new to development, please follow the [basic guide](/basic_guide).

If you are an experienced developer but new to Blockchain, we recommend you to start from the [intermediate guide](/intermediate_guide).

For those who are already used to Blockchain, you can go directly to the [advanced guide](/advanced_guide).

[Best Patterns and Practices](/best_practices) is recommended for all developers!

## Best Patterns and Practices

Another goal of the neo-sharp team is to grow together and help everyone become better developers than when they started working on the project. We will continue to curate a list of resources on best patterns and practices, anti-patterns, bad habits, or any other quick reads that could be beneficial to increase the quality of code contributors of the project:

### General Coding Practices and Conventions:
Neo-sharp aims to adhere to Microsft C# .NET best practices and industry standard design patterns. Please reference the following resources and familiarize yourself with recommended best practices.

[C# Gettting Started](https://github.com/dotnet/training-tutorials/tree/master/content/csharp/getting-started) This resource was a good crash course on some of the basics in C# if you are new to the language or if you could use a quick refresher.

[C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions) This is a quick resource to demonstrate code commenting, general formatting, and a quick overview.

[.NET Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/) This is a comprehensive resource that enumerates everything from naming conventions, to proper exception handling, all the way to when to use certain collections and types.

[Patterns and Anti-patterns](https://github.com/dotnet/training-tutorials/blob/master/content/csharp/getting-started/patterns-antipatterns.md) This is a good overview of some of the more popular design patterns (and anti-patterns) with examples.

### Anti-patterns and bad habits

We've all done it at some point. Here you can find some links to resources on patterns and practices that could get you in trouble as a developer.

[9 Anti-patterns Every Programmer Should Be Aware Of](https://sahandsaba.com/nine-anti-patterns-every-programmer-should-be-aware-of-with-examples.html) This is a good quick list of some more general concepts that often hang developers and teams up from making good progress.

[Code Smells](https://blog.codinghorror.com/code-smells/) This was a nice concise list of some common code smells that developers will encounter.

## Basic Guide

### Introduction

This guide is intended to help people with little or no development knowledge. If you have already contributed with open source projects in GitHub, you can skip to [intermediate guide](/intermediate_guide).

This guide covers:
1. Required software;
2. GitHub basics;
3. How to communicate with the team;

The steps below cover all these 3 topics.

### Steps

This is an open source project hosted in GitHub. All source code, test and documentation can be found in the project [GitHub page](/https://github.com/CityOfZion/neo-sharp).

Follow the steps below to install and configure the project in your computer.
1. [Sign Up](https://github.com/join?source=header-home) for a GitHub Account;
2. [Download](https://git-scm.com/downloads) and install Git;
3. (Optional) [Download](https://git-scm.com/downloads/guis) and install Git Desktop, GitKraken or SourceTree;
4. [Configure](https://git-scm.com/book/en/v2/Getting-Started-First-Time-Git-Setup) Git to use your GitHub account;
5. [Fork](/git_basics#fork) the project and clone it in your computer.
6. [Download](https://www.microsoft.com/net/download/windows) and install .NET Core 2.1. Remember to install the correct version depending on your OS.
7. [Download](/IDE) a IDE compatible with C#;
8. [Clone](/git_basics#clone) the forked project in your machine;
9. [Add another remote](/git_basics#new_remote) using the official project URL;
10. Create a new [branch](/git_basics#branch) in your repository. If you are just exploring the project and does not intent to send any changes, you can skip this step.
11. (For documentation only) To change the project documentation, you need to [download](https://nodejs.org/en/download/) and install Node;
12. (For documentation only) Using the command line, access the `website` folder inside neo-sharp and run `npm i`. This step may take a few minutes since it will install several project dependencies;
13. (For documentation only) Using the command line, access the `website` folder inside neo-sharp and run `npm start`;
14. After you made the changes you want, commit, rebase, push and create a pull request, as described [here](/git_basics#commit);
15. Create an issue in [GitHub](https://github.com/CityOfZion/neo-sharp/issues) to propose the changes you want to make;
16. Discuss your issue in [Discord](discord_link) with other team members.
17. It's possible to run the `Application` module, but it's recommended that you test new code using Unit Tests.


You are now ready to contribute to the project. It's now time to learn about more advanced concepts. We suggest you reading the full documentation to understand how Blockchain and NEO works.

## Intermediate Guide

This guide is intended to help people with little or no Blockchain knowledge. If you have already contributed with Blockchain technologies before, you can skip to [advanced guide](/advanced_guide).

This guide covers:
1. Blockchain basics;
2. Development concepts used by this project;
3. Unit Testing;

### Blockchain Basics.

To contribute with this project, it's recommended that you have some basic knowledge about Blockchain and related technologies. Below is a list of what we consider the most basic knowledge in this area. Follow the links provided in this article to ensure you have a solid understanding about it.
If you find any mistakes or unclear instruction, please [contact the team or submit a pull request](/basic_guide).


#### Block and BlockHeader.

 The blocks are used to store the data of the whole network, such as transactions or assets, and every block has it's own header to store meta-data about the block itself. A more detailed explanation can be found [here](/block).

#### Genesis block and the Blockchain.

The blockchain is the logical structure, connecting Blocks in a one-way linked list, and every new block depends on it's previous block. The first block is called Genesis block. It's in this block that NEO and GAS is born. A more detailed explanation can be found [here](/Blockchain).

#### Cryptographic Hashing functions - SHA256 and RIPMED160.

A hashing function can be applied to data resulting in a fixed size output. In NEO, we use 2 known algorithms to create hashes: SHA256 and RIPMED160. The first will result in a 256 bits (32 bytes) message, while the second will generate a 160 bits (20 bytes) message.
In NEO, we use 32-byte hashes for computing Block and Transaction Hashes, and 20-bytes hashes for address generation. In code these types are represented in UInt256 and UInt160 respectively. A more detailed information can be found [here](/Hash).

#### Unspent Transaction Output (UTXO).

NEO uses the UTXO model, meaning that, to make a transaction, you must reference a transaction that you received in the past, that was not used yet. An address balance is the sum of all these 'unspent' transactions. A more detailed explanation can be found [here](/UTXO).

#### Development concepts used by this project;

This project relies on a few particular C# directives. A brief description can be found [here](/Development).
