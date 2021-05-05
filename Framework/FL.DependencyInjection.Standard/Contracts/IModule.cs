namespace FL.DependencyInjection.Standard.Contracts
{
    public interface IModule
    {
        void RegisterServices(IServiceCollection services);
    }
}
