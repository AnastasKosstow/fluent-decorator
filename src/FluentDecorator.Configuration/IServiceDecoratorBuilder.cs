namespace FluentDecorator.Configuration;

public interface IServiceDecoratorBuilder<out TServiceInterface>
{
    TServiceInterface Build(IServiceProvider serviceProvider);
}
