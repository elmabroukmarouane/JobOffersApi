using JobsOffer.Api.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using JobsOffer.Api.Business.Redis.Interface;
using JobsOffer.Api.Business.Redis.Class;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Business.Services.Classes;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Interface;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Class;
using JobsOffer.Api.Business.Cqrs.Commands.Interfaces;
using JobsOffer.Api.Business.Cqrs.Commands.Classes;
using JobsOffer.Api.Business.Cqrs.Queries.Classes;
using JobsOffer.Api.Business.Cqrs.Queries.Interfaces;

namespace JobsOffer.Api.Server.Extensions.Add;
public static class AddServices
{
    public static void AddSERVICES(this IServiceCollection self, IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        self.AddTransient<IUnitOfWork<DbContextJobsOffer>, UnitOfWork<DbContextJobsOffer>>();

        self.AddTransient<IGenericCreateCommand<DomainJob>, GenericCreateCommand<DomainJob>>();
        self.AddTransient<IGenericUpdateCommand<DomainJob>, GenericUpdateCommand<DomainJob>>();
        self.AddTransient<IGenericGetEntitiesQuery<DomainJob>, GenericGetEntitiesQuery<DomainJob>>();
        self.AddTransient<IGenericDeleteQuery<DomainJob>, GenericDeleteQuery<DomainJob>>();
        self.AddTransient<IGenericService<DomainJob>, GenericService<DomainJob>>();

        self.AddTransient<IGenericCreateCommand<Favori>, GenericCreateCommand<Favori>>();
        self.AddTransient<IGenericUpdateCommand<Favori>, GenericUpdateCommand<Favori>>();
        self.AddTransient<IGenericGetEntitiesQuery<Favori>, GenericGetEntitiesQuery<Favori>>();
        self.AddTransient<IGenericDeleteQuery<Favori>, GenericDeleteQuery<Favori>>();
        self.AddTransient<IGenericService<Favori>, GenericService<Favori>>();

        self.AddTransient<IGenericCreateCommand<Job>, GenericCreateCommand<Job>>();
        self.AddTransient<IGenericUpdateCommand<Job>, GenericUpdateCommand<Job>>();
        self.AddTransient<IGenericGetEntitiesQuery<Job>, GenericGetEntitiesQuery<Job>>();
        self.AddTransient<IGenericDeleteQuery<Job>, GenericDeleteQuery<Job>>();
        self.AddTransient<IGenericService<Job>, GenericService<Job>>();

        self.AddTransient<IGenericCreateCommand<Profil>, GenericCreateCommand<Profil>>();
        self.AddTransient<IGenericUpdateCommand<Profil>, GenericUpdateCommand<Profil>>();
        self.AddTransient<IGenericGetEntitiesQuery<Profil>, GenericGetEntitiesQuery<Profil>>();
        self.AddTransient<IGenericDeleteQuery<Profil>, GenericDeleteQuery<Profil>>();
        self.AddTransient<IGenericService<Profil>, GenericService<Profil>>();

        self.AddTransient<IGenericCreateCommand<ProfilDomainJob>, GenericCreateCommand<ProfilDomainJob>>();
        self.AddTransient<IGenericUpdateCommand<ProfilDomainJob>, GenericUpdateCommand<ProfilDomainJob>>();
        self.AddTransient<IGenericGetEntitiesQuery<ProfilDomainJob>, GenericGetEntitiesQuery<ProfilDomainJob>>();
        self.AddTransient<IGenericDeleteQuery<ProfilDomainJob>, GenericDeleteQuery<ProfilDomainJob>>();
        self.AddTransient<IGenericService<ProfilDomainJob>, GenericService<ProfilDomainJob>>();

        self.AddTransient<IUserCreateCommand, UserCreateCommand>();
        self.AddTransient<IUserUpdateCommand, UserUpdateCommand>();
        self.AddTransient<IGenericGetEntitiesQuery<User>, GenericGetEntitiesQuery<User>>();
        self.AddTransient<IGenericDeleteQuery<User>, GenericDeleteQuery<User>>();
        self.AddTransient<IUserService, UserService>();

        self.AddTransient<IGenericCreateCommand<WebSite>, GenericCreateCommand<WebSite>>();
        self.AddTransient<IGenericUpdateCommand<WebSite>, GenericUpdateCommand<WebSite>>();
        self.AddTransient<IGenericGetEntitiesQuery<WebSite>, GenericGetEntitiesQuery<WebSite>>();
        self.AddTransient<IGenericDeleteQuery<WebSite>, GenericDeleteQuery<WebSite>>();
        self.AddTransient<IGenericService<WebSite>, GenericService<WebSite>>();

        self.AddTransient<IRedisService, RedisService>();

        self.AddSingleton(configuration);
        self.AddSingleton(hostEnvironment);
        self.AddSingleton<IRedisConnectionFactory>(new RedisConnectionFactory(configuration.GetConnectionString("RedisConnection")!));
    }
}
