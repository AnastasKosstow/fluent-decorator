using Microsoft.Extensions.Logging;

namespace FluentDecorator.Configuration.DisposeRegister.Singleton;

internal class SingletonDisposableRegistry(ILogger<DisposableRegistry> logger) : DisposableRegistry(logger), ISingletonDisposableRegistry
{
}
