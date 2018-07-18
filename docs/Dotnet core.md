---
id: dotnetcore
title: .NET Core
sidebar_label: .NET Core
---

## About
NEO-Sharp project is built using C# on top of .NET Core. C# is the programming language, while .NET Core is the default collection of classes that are ready for you to use.

C# can also be used with .NET Framework (different from .NET Core), but this is only compatible with Windows, and for that reason, .NET Core is used instead.

## IDE

It's better to use a Integrated Development Environment to have features like debugging and auto-complete. 

For C#, the most recommended are Visual Studio and Visual Code. You can [download it here](https://visualstudio.microsoft.com/)

## Attributes (Annotations)

For more information [Microsoft Doc](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes)

## Extensions
Extension methods enable you to "add" methods to existing types. 

In NEO-Sharp, its in the NeoSharp.Core/Extensions folder

```
Example:
NeoSharp.Core/Extensions/BigIntegerExtensions.cs
NeoSharp.Core/Extensions/BitExtensions.cs
NeoSharp.Core/Extensions/ByteArrayExtensions.cs
NeoSharp.Core/Extensions/DateTimeExtensions.cs
NeoSharp.Core/Extensions/EnumerableExtensions.cs
NeoSharp.Core/Extensions/Fixed8Extensions.cs
NeoSharp.Core/Extensions/IntExtensions.cs
NeoSharp.Core/Extensions/MessageExtensions.cs
NeoSharp.Core/Extensions/SerializableExtensions.cs
NeoSharp.Core/Extensions/StringExtensions.cs
NeoSharp.Core/Extensions/TypeExtensions.cs
```

For more information about extensions: [Microsoft Doc](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)

## LINQ

Linq its extension methods for IEnumerable, you can use when you need interact with list

For more information [Microsoft Doc](https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries)

## Partial Class

In NEO-Sharp, we are using for implementation of NeoSharp.Application/Client/IPrompt.cs

```
Example:
NeoSharp.Application/Client/Prompt.cs
NeoSharp.Application/Client/Prompt[Blockchain].cs
NeoSharp.Application/Client/Prompt[Contract].cs
NeoSharp.Application/Client/Prompt[Invokes].cs
NeoSharp.Application/Client/Prompt[Network].cs
NeoSharp.Application/Client/Prompt[Usability].cs
NeoSharp.Application/Client/Prompt[Wallet].cs
```

For more information [Microsoft Doc](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods)