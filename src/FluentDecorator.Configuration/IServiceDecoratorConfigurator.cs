﻿namespace FluentDecorator.Configuration;

public interface IServiceDecoratorConfigurator<in TServiceInterface>
{
    IServiceDecoratorConfigurator<TServiceInterface> AddDecorator<TDecorator>() where TDecorator : TServiceInterface;
    void AddService<TService>() where TService : TServiceInterface;
}
