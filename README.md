## Introduction
FluentDecorator is a lightweight and extensible library that simplifies the implementation of the Decorator Pattern in .NET applications.
<br>
It provides a fluent API for registering services with multiple decorators in Dependency Injection (DI) container while ensuring proper lifetime management.
<br>
With FluentDecorator, you can easily wrap services with logging, validation, caching, or other cross-cutting concerns without modifying the core service logic.
<br>

𝚃𝚑𝚎 𝚕𝚒𝚋𝚛𝚊𝚛𝚢 𝚜𝚞𝚙𝚙𝚘𝚛𝚝𝚜 𝚜𝚒𝚗𝚐𝚕𝚎𝚝𝚘𝚗, 𝚜𝚌𝚘𝚙𝚎𝚍, 𝚊𝚗𝚍 𝚝𝚛𝚊𝚗𝚜𝚒𝚎𝚗𝚝 𝚕𝚒𝚏𝚎𝚝𝚒𝚖𝚎𝚜, 𝚎𝚗𝚜𝚞𝚛𝚒𝚗𝚐 𝚌𝚘𝚛𝚛𝚎𝚌𝚝 𝚍𝚒𝚜𝚙𝚘𝚜𝚊𝚕 𝚘𝚏 𝚍𝚎𝚌𝚘𝚛𝚊𝚝𝚎𝚍 𝚜𝚎𝚛𝚟𝚒𝚌𝚎𝚜.

> [!IMPORTANT]
> Key Features:<br>
> &nbsp;&nbsp;&nbsp;✅ Fluent API – Easily register decorators and services in a readable, expressive manner.<br>
> &nbsp;&nbsp;&nbsp;✅ Lifetime Management – Supports Singleton, Scoped, and Transient services with correct disposal.<br>
> &nbsp;&nbsp;&nbsp;✅ Extensibility – Decorators are applied dynamically, allowing flexible composition of behaviors.<br>
> &nbsp;&nbsp;&nbsp;✅ Thread-Safe – Ensures safe concurrent execution with proper synchronization.<br>

🛠️ How to Register and Use FluentDecorator

1️⃣ Add `FluentDecorator` services<br>
To use FluentDecorator, you need to add `AddDecorator` extension method to IServiceCollection from `FluentDecorator` namespace.
```C#
// Program.cs

using FluentDecorator;

...
services.AddDecorator();
```

2️⃣ Add Services with Decorators for specific lifetime scope.

📌Singleton service
```C#
services.AddSingletonService<IWeatherService>(
    configurator =>
    {
        configurator
            .AddDecorator<WeatherServiceLoggingDecorator>()
            .AddService<WeatherService>();
    });
```

📌Scoped service
```C#
services.AddScopedService<IWeatherService>(
    configurator =>
    {
        ...
    });
```

📌Transient service
```C#
services.AddTransientService<IWeatherService>(
    configurator =>
    {
        ...
    });
```

3️⃣ How It Works<br>
`WeatherService` is the actual implementation that fetches weather data.<br>
`WeatherServiceLoggingDecorator` wraps around `WeatherService` to add logging functionality.<br>
`FluentDecorator` dynamically applies the decorator at runtime.
