namespace FluentDecorator.Configuration.DisposeRegister.DisposeScopes;

public interface IScope
{
    IDisposableRegistry GetDisposableRegistry(IServiceProvider provider);
}
