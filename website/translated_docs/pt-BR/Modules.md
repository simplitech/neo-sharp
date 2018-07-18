---
id: modules
title: Módulos
sidebar_label: Módulos
---

## Aplicação

```
NeoSharp.Application/DI/ClientModule.cs
```

## Configuração

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

## Serialização

```
NeoSharp.Application/DI/SerializationModule.cs
```

## Persistência

```
NeoSharp.Application/DI/PersistenceModule.cs
```

## Log

```
NeoSharp.Application/DI/LoggingModule.cs
```

## Carteira

```
NeoSharp.Application/DI/WalletModule.cs
```

## VM

## Criando novo módulo

Crie uma nova pasta no projeto NeoSharp.Core seguindo a seguinte estrutura de nome 'NomeModulo';

```
Exemplo:
NeoSharp.Core/Persistence
```

Crie uma interface contendo especificação do seu módulo

```
Exemplo:
NeoSharp.Core/Persistence/IRepository.cs
```

Crie implementações da sua interface. Em NEO-Sharp nós temos duas implementações para a mesma interface, ou seja, podemos facilmente trocar qual das implementações utilizar.

```
Exemplo:
NeoSharp.Persistence.RedisDB/RedisDbRepository.cs
NeoSharp.Persistence.RocksDB/RocksDbRepository.cs
```

Crie a classe para registrar sua injeção de dependência no projeto NeoSharp.Application/DI.

```
Exemplo:
NeoSharp.Application/DI/PersistenceModule.cs
```

Pode verificar o seu arquivo de configuração e escolher qual das implementações utilizar, use 'containerBuilder.RegisterSingleton' ou  'containerBuilder.Register' para registrar.

```
Exemplo:

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

Para registrar seu módulo dentro da aplicação adicione o método 'containerBuilder.RegisterModule' em 'NeoSharp.Application/Program.cs'

```
Exemplo:
containerBuilder.RegisterModule<PersistenceModule>();
```

Em NEO-Sharp, teste unitário e obrigatório, para mais informação [UNIT TEST](unit_test)