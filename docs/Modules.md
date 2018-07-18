---
id: modules
title: Modules
sidebar_label: Modules
---

## Application

```
NeoSharp.Application/DI/ClientModule.cs
```

## Configuration

```
NeoSharp.Application/DI/ConfigurationModule.cs
```

## Core

```
NeoSharp.Core/DI/CoreModule.cs
NeoSharp.Core/DI/Modules/BlockchainModule.cs
NeoSharp.Core/DI/Modules/HelpersModule.cs
NeoSharp.Core/DI/Modules/NetworkModule.cs
```

## Serialization

```
NeoSharp.Application/DI/SerializationModule.cs
```

## Persistence

```
NeoSharp.Application/DI/PersistenceModule.cs
```

## Logging

```
NeoSharp.Application/DI/LoggingModule.cs
```

## Wallet

```
NeoSharp.Application/DI/WalletModule.cs
```

## VM

## Create a new module

Create a new folder in the NeoSharp.Core project following the naming structure 'ModuleName';

```
Example:
NeoSharp.Core/Persistence
```

Create an Interfaces containing your module specifications. 

```
Example:
NeoSharp.Core/Persistence/IRepository.cs
```

Create an implementations for your Interface. In neo-sharp we have two implementations for the same interface, which means that we can easily change the desired implementation.

```
Example:
NeoSharp.Persistence.RedisDB/RedisDbRepository.cs
NeoSharp.Persistence.RocksDB/RocksDbRepository.cs
```

Create a class to register your dependency injection in the NeoSharp.Application/DI project.

```
Example:
NeoSharp.Application/DI/PersistenceModule.cs
```

Check your configuration file and choose which one implementation will be use, to register use 'containerBuilder.RegisterSingleton' or  'containerBuilder.Register'.

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

To register your module onto application, add method 'containerBuilder.RegisterModule' in 'NeoSharp.Application/Program.cs'

```
Example:
containerBuilder.RegisterModule<PersistenceModule>();
```

In neo-sharp, unit test its mandatory, for more information [UNIT TEST](unit_test)