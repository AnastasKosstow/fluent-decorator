namespace FluentDecorator.Configuration.DisposeRegister;

public interface IDisposableRegistry
{
    void Register(IDisposable disposable);
    void Dispose();
}
