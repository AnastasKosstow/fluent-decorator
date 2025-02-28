using Microsoft.Extensions.Logging;

namespace FluentDecorator.Configuration.DisposeRegister;

public class DisposableRegistry(ILogger<DisposableRegistry> logger) : IDisposableRegistry, IDisposable
{
    private readonly ILogger<DisposableRegistry> _logger = logger;

    private readonly List<IDisposable> _disposables = [];
    private readonly object _lock = new();

    public void Register(IDisposable disposable)
    {
        lock (_lock)
        {
            _disposables.Add(disposable);
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            foreach (var disposable in _disposables)
            {
                try
                {
                    disposable.Dispose();
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error disposing object of type {Type}. Message: {Message}", disposable.GetType().FullName, ex.Message);
                }
            }
            _disposables.Clear();
        }
    }
}
