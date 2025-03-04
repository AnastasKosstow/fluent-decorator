## Introduction
FluentDecorator is a lightweight and extensible library that simplifies the implementation of the Decorator Pattern in .NET applications.
<br>
It provides a fluent API for registering services with multiple decorators in Dependency Injection (DI) container while ensuring proper lifetime management.

<br>

With FluentDecorator, you can easily wrap services with logging, validation, caching, or other cross-cutting concerns without modifying the core service logic. The library supports singleton, scoped, and transient lifetimes, ensuring correct disposal of decorated services.

> [!IMPORTANT]
> Key Features:
> ✅ Fluent API – Easily register decorators and services in a readable, expressive manner.
> ✅ Lifetime Management – Supports Singleton, Scoped, and Transient services with correct disposal.
> ✅ Extensibility – Decorators are applied dynamically, allowing flexible composition of behaviors.
> ✅ Thread-Safe – Ensures safe concurrent execution with proper synchronization.

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
