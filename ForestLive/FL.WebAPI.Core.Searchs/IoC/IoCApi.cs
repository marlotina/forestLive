using FL.DependencyInjection.Standard.Contracts;
using FL.WebAPI.Core.Searchs.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Searchs.Api.Mappers.v1.Implementations;
using FL.WebAPI.Core.Searchs.Application.Services.Contracts;
using FL.WebAPI.Core.Searchs.Application.Services.Implementations;
using FL.WebAPI.Core.Searchs.Configuration.Contracts;
using FL.WebAPI.Core.Searchs.Configuration.Implementations;
using FL.WebAPI.Core.Searchs.Domain.Repositories;
using FL.WebAPI.Core.Searchs.Domain.Repository;
using FL.WebAPI.Core.Searchs.Infrastructure.Repositories;

namespace FL.WebAPI.Core.Searchs.IoC
{
    public class IoCApi: IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IPostMapper, PostMapper>();
            services.AddSingleton<IBirdsConfiguration, BirdsConfiguration>();

            services.AddTransient<ISpeciesService, SpeciesService>();
            services.AddTransient<ISearchMapService, SearchMapService>();
            services.AddTransient<IUserVoteService, UserVoteService>();
            services.AddTransient<IUserPostService, UserPostService>();
            services.AddTransient<IUserInfoService, UserInfoService>();
            services.AddTransient<ISpecieInfoService, SpecieInfoService>();

            services.AddTransient<ISpeciesRepository, SpeciesRepository>();
            services.AddTransient<ISearchMapRepository, SearchMapRepository>();
            services.AddTransient<IUserPostRepository, UserPostCosmosRepository>();

            services.AddTransient<IUserVotesRestRepository, UserVotesRestRepository>();
            services.AddTransient<IUserInfoRestRepository, UserInfoRestRepository>();
            services.AddTransient<ISpecieRestRepository, SpecieRestRepository>();
        }
    }
}
