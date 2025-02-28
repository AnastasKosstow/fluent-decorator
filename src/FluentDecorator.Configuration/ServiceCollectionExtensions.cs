using FluentDecorator.Configuration;
using FluentDecorator.Configuration.DisposeRegister.DisposeScopes;
using FluentDecorator.Configuration.DisposeRegister.Scoped;
using FluentDecorator.Configuration.DisposeRegister.Singleton;
using Microsoft.Extensions.DependencyInjection;

namespace FluentDecorator;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to register services with decorator pattern support.
/// These extensions enable a fluent API for configuring services with one or more decorators in the dependency injection container.
/// </summary>
/// <remarks>
/// The FluentDecorator library allows for easy implementation of the decorator pattern in a .NET application
/// using Microsoft's dependency injection container. It handles proper disposal of decorated components
/// based on their registered lifetime (singleton, scoped, or transient).
/// </remarks>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the core FluentDecorator services to the dependency injection container.
    /// This method must be called before using any other FluentDecorator extension methods.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The same service collection to enable method chaining.</returns>
    /// <example>
    /// <code>
    /// var services = new ServiceCollection();
    /// services.AddDecorator();
    /// </code>
    /// </example>
    public static IServiceCollection AddDecorator(this IServiceCollection services)
    {
        return services
            .AddSingleton<ISingletonDisposableRegistry, SingletonDisposableRegistry>()
            .AddScoped<IScopedDisposableRegistry, ScopedDisposableRegistry>();
    }

    /// <summary>
    /// Registers a singleton service with the dependency injection container using the decorator pattern.
    /// </summary>
    /// <typeparam name="TServiceInterface">The service interface type that will be used to resolve the service.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="configCallback">A callback to configure the service implementation and its decorators.</param>
    /// <returns>The same service collection to enable method chaining.</returns>
    /// <remarks>
    /// The singleton service and its decorators will be disposed when the application shuts down.
    /// Decorators are applied in reverse order of registration, with the first registered decorator being the outermost.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.AddSingletonService&lt;IMyService&gt;(config => {
    ///     config.AddDecorator&lt;LoggingDecorator&gt;()
    ///           .AddDecorator&lt;CachingDecorator&gt;()
    ///           .AddService&lt;MyServiceImplementation&gt;();
    /// });
    /// </code>
    /// </example>
    public static IServiceCollection AddSingletonService<TServiceInterface>(this IServiceCollection services, Action<IServiceDecoratorConfigurator<TServiceInterface>> configCallback)
        where TServiceInterface : class
    {
        return services.AddSingleton(serviceProvider => BuildDecoratedService(serviceProvider, configCallback, new LifetimeScope()));
    }

    /// <summary>
    /// Registers a scoped service with the dependency injection container using the decorator pattern.
    /// </summary>
    /// <typeparam name="TServiceInterface">The service interface type that will be used to resolve the service.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="configCallback">A callback to configure the service implementation and its decorators.</param>
    /// <returns>The same service collection to enable method chaining.</returns>
    /// <remarks>
    /// The scoped service and its decorators will be disposed when the scope is disposed.
    /// Decorators are applied in reverse order of registration, with the first registered decorator being the outermost.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.AddScopedService&lt;IMyService&gt;(config => {
    ///     config.AddDecorator&lt;LoggingDecorator&gt;()
    ///           .AddDecorator&lt;ValidationDecorator&gt;()
    ///           .AddService&lt;MyServiceImplementation&gt;();
    /// });
    /// </code>
    /// </example>
    public static IServiceCollection AddScopedService<TServiceInterface>(this IServiceCollection services, Action<IServiceDecoratorConfigurator<TServiceInterface>> configCallback)
        where TServiceInterface : class
    {
        return services.AddScoped(serviceProvider => BuildDecoratedService(serviceProvider, configCallback, new ShorterLivedScope()));
    }

    /// <summary>
    /// Registers a transient service with the dependency injection container using the decorator pattern.
    /// </summary>
    /// <typeparam name="TServiceInterface">The service interface type that will be used to resolve the service.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="configCallback">A callback to configure the service implementation and its decorators.</param>
    /// <returns>The same service collection to enable method chaining.</returns>
    /// <remarks>
    /// The transient service and its decorators will be disposed according to the scope in which they are resolved.
    /// Decorators are applied in reverse order of registration, with the first registered decorator being the outermost.
    /// </remarks>
    /// <example>
    /// <code>
    /// services.AddTransientService&lt;IMyService&gt;(config => {
    ///     config.AddDecorator&lt;LoggingDecorator&gt;()
    ///           .AddDecorator&lt;TransactionDecorator&gt;()
    ///           .AddService&lt;MyServiceImplementation&gt;();
    /// });
    /// </code>
    /// </example>
    public static IServiceCollection AddTransientService<TServiceInterface>(this IServiceCollection services, Action<IServiceDecoratorConfigurator<TServiceInterface>> configCallback)
        where TServiceInterface : class
    {
        return services.AddTransient(serviceProvider => BuildDecoratedService(serviceProvider, configCallback, new ShorterLivedScope()));
    }

    /// <summary>
    /// Builds a decorated service instance using the provided configuration.
    /// </summary>
    /// <typeparam name="TServiceInterface">The service interface type.</typeparam>
    /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
    /// <param name="configCallback">The configuration callback that defines the service and its decorators.</param>
    /// <param name="scope">The lifetime scope that determines how and when decorated services are disposed.</param>
    /// <returns>An instance of the service with all decorators applied.</returns>
    /// <remarks>
    private static TServiceInterface BuildDecoratedService<TServiceInterface>(IServiceProvider serviceProvider, Action<IServiceDecoratorConfigurator<TServiceInterface>> configCallback, IScope scope)
    {
        var builder = new DefaultDecoratorBuilder<TServiceInterface>(scope);
        configCallback(builder);
        return builder.Build(serviceProvider);
    }
}