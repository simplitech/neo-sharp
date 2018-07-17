---
id: dependency_injection
title: Dependency Injection
sidebar_label: Dependency Injection
---

## About

Dependency Injection gives a project the flexibility add an replace code in a project. Since the code does not rely on a concrete implementation, but rather a Interface, it's much easier to replace a module when necessary.

If we use Interfaces in our projects, we already reduce the dependency between classes, 
But the problem is that somewhere in the code, you need to instantiate the concrete class, and if this is not done correctly, your classes will still depend on the concrete implementation.

The best way to avoid this high coupling scenario (the need of knowing the concrete class in advance), we add another actor that is responsible for instantiating the concrete classes, so the modules don't have any association with the concrete class, thus eliminating any dependency from one module from another.

We can resume Dependency Injection as the class/object responsible for managing the dependencies of other classes.

This promotes reusability, testability and maintainability of a project, because you no longer require concrete implementations, only Interfaces.

In neo-sharp, we use our own dependency injection mechanism, but most of the programming languages already have some sort of 'native' dependency injection.

In neo-sharp, the dependencies are inserted in the constructor of an object, and to this work, it’s necessary to register the modules, so the DI class know which implementations are available for each interface.

For more information about DI, please refer to [wiki](https://en.wikipedia.org/wiki/Dependency_injection).

## Create a new module

* Create a new folder in the NeoSharp.Core project following the naming structure 'ModuleName';

```
Example:
NeoSharp.Core/Persistence
```

* Create an Interface containing your module specifications. 

```
Example:
NeoSharp.Core/Persistence/IRepository.cs
```

* Create an implementation for your Interface. In neo-sharp we have two implementations for the same interface, which means that we can easily change the desired implementation.

```
Example:
NeoSharp.Persistence.RedisDB/RedisDbRepository.cs
NeoSharp.Persistence.RocksDB/RocksDbRepository.cs
```

* Create a class to register your dependency injection in the NeoSharp.Application/DI project.

```
Example:
NeoSharp.Application/DI/PersistenceModule.cs
```

* Check your configuration file and choose which one implementation will be use, to register use 'containerBuilder.RegisterSingleton' or  'containerBuilder.Register'.

```
Example:

var cfg = PersistenceConfig.Instance();

switch (cfg.Provider)
{
    case RedisDbConfig.Provider:
        {
            containerBuilder.RegisterSingleton<RedisDbConfig>();
            containerBuilder.RegisterSingleton<IRepository, RedisDbRepository>();
            containerBuilder.RegisterSingleton<IRedisDbContext, RedisDbContext>();
            break;
        }

    case RocksDbConfig.Provider:
        {
            containerBuilder.RegisterSingleton<RocksDbConfig>();
            containerBuilder.RegisterSingleton<IRepository, RocksDbRepository>();
            containerBuilder.RegisterSingleton<IRocksDbContext, RocksDbContext>();
            break;
        }

    default:
        throw new Exception($"The persistence configuration contains unknown provider \"{cfg.Provider}\"");
}
```

* To register your module onto application, add method 'containerBuilder.RegisterModule' in 'NeoSharp.Application/Program.cs'

```
Example:
containerBuilder.RegisterModule<PersistenceModule>();
```

* In neo-sharp, unit test its mandatory, for more information [UNIT TEST](doc1)


