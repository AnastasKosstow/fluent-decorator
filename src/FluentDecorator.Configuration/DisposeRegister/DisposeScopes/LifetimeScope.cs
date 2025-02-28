using FluentDecorator.Configuration.DisposeRegister.Scoped;
using Microsoft.Extensions.DependencyInjection;

namespace FluentDecorator.Configuration.DisposeRegister.DisposeScopes;

public class LifetimeScope : IScope
{
    public IDisposableRegistry GetDisposableRegistry(IServiceProvider provider)
    {
        return provider.GetService<IScopedDisposableRegistry>();
    }
}
