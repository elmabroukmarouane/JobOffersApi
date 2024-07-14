using AutoMapper;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Server.DtoModel.Models;

namespace JobsOffer.Api.Server.DtoModel.Profiles
{
    public class Profil : Profile
    {
        public Profil()
        {
            CreateMap<DomainJob, DomainJobViewModel>();
            CreateMap<DomainJobViewModel, DomainJob>();
            CreateMap<Favori, FavoriViewModel>();
            CreateMap<FavoriViewModel, Favori>();
            CreateMap<Job, JobViewModel>();
            CreateMap<JobViewModel, Job>();
            CreateMap<Profil, ProfilViewModel>();
            CreateMap<ProfilViewModel, Profil>();
            CreateMap<ProfilDomainJob, ProfilDomainJobViewModel>();
            CreateMap<ProfilDomainJobViewModel, ProfilDomainJob>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<WebSite, WebSiteViewModel>();
            CreateMap<WebSiteViewModel, WebSite>();
        }
    }
}
