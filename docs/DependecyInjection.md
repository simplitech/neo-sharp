---
id: dependency_injection
title: Dependency Injection
sidebar_label: Dependency Injection
---

### Overview

Dependency Injection gives a project the flexibility add an replace code in a project. Since the code does not rely on a concrete implementation, but rather a Interface, it's much easier to replace a module when necessary.

If we use Interfaces in our projects, we already reduce the dependency between classes,
but the problem is that somewhere in the code, you need to instantiate the concrete class, and if this is not done correctly, your classes will be high coupled with the implementation.

The best way to avoid this high coupling scenario, it's to add another actor that is responsible for handling the dependencies between the classes. If we use Dependency Injection correctly, the modules don't have any association with the concrete class, therefore it's possible to eliminate any dependency from one module from another.
This promotes reusability, testability and maintainability of a project, because you no longer require concrete implementations, only Interfaces.

We can resume Dependency Injection as the class/object responsible for managing the dependencies of other classes.

In neo-sharp, we use our own dependency injection mechanism, but most of the programming languages already have some sort of 'native' dependency injection.

In neo-sharp, the dependencies are inserted in the constructor of an object, and to this work, it’s necessary to register the modules, so the DI class know which implementations are available for each interface.

For more information about DI, please refer to [wiki](https://en.wikipedia.org/wiki/Dependency_injection).

### Neo-sharp Dependency Injection
