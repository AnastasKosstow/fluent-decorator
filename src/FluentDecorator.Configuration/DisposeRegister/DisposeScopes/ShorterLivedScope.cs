using FluentDecorator.Configuration.DisposeRegister.Singleton;
using Microsoft.Extensions.DependencyInjection;

namespace FluentDecorator.Configuration.DisposeRegister.DisposeScopes;

public class ShorterLivedScope : IScope
{
    public IDisposableRegistry GetDisposableRegistry(IServiceProvider provider)
    {
        return provider.GetService<ISingletonDisposableRegistry>();
    }
}
