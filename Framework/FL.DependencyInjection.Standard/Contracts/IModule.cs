using System;
using System.Collections.Generic;
using System.Text;

namespace FL.DependencyInjection.Standard.Contracts
{
    public interface IModule
    {
        void RegisterServices(IServiceCollection services);
    }
}
