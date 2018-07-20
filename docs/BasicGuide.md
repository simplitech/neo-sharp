---
id: basic_guide
title: Basic Guide
sidebar_label: Basic Guide
---

### Introduction

This guide is intended to help people with little or no development knowledge. If you have already contributed with open source projects in GitHub, you can skip to [intermediate guide](intermediate_guide).

This guide covers:
1. Required software;
2. GitHub basics;
3. How to communicate with the team;

The steps below cover all these 3 topics.

### Steps

This is an open source project hosted in GitHub. All source code, test and documentation can be found in the project [GitHub page](https://github.com/CityOfZion/neo-sharp). It's necessary that you download and install .NET Core 2.1.


Follow the steps below to install and configure the project in your computer.
1. [Sign Up](https://github.com/join?source=header-home) for a GitHub Account;
2. [Download](https://git-scm.com/downloads) and install Git;
3. (Optional) [Download](https://git-scm.com/downloads/guis) and install Git Desktop, GitKraken or SourceTree;
4. [Configure](https://git-scm.com/book/en/v2/Getting-Started-First-Time-Git-Setup) Git to use your GitHub account;
5. [Fork](git#fork) the project and clone it in your computer.
6. [Download](https://www.microsoft.com/net/download/windows) and install .NET Core 2.1. Remember to install the correct version depending on your OS.
7. [Download](IDE) a IDE compatible with C#;
8. [Clone](git#clone) the forked project in your machine;
9. [Add another remote](git#new_remote) using the official project URL;
10. Create a new [branch](git#branch) in your repository. If you are just exploring the project and does not intent to send any changes, you can skip this step.
11. (For documentation only) To change the project documentation, you need to [download](https://nodejs.org/en/download/) and install Node;
12. (For documentation only) Using the command line, access the `website` folder inside neo-sharp and run `npm i`. This step may take a few minutes since it will install several project dependencies;
13. (For documentation only) Using the command line, access the `website` folder inside neo-sharp and run `npm start`;
14. Create an issue in [GitHub](https://github.com/CityOfZion/neo-sharp/issues) to propose the changes you want to make;
15. [Propose your changes](git) with a PullRequest.


### MacOS
For MacOS users, it's necessary to [install RocksDB](https://github.com/facebook/rocksdb/blob/master/INSTALL.md).
