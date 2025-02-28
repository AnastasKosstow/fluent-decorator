using Microsoft.Extensions.Logging;

namespace FluentDecorator.Configuration.DisposeRegister.Scoped;

internal class ScopedDisposableRegistry(ILogger<DisposableRegistry> logger) : DisposableRegistry(logger), IScopedDisposableRegistry
{
}
