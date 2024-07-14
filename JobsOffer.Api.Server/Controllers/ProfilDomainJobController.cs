using AutoMapper;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Server.DtoModel.Models;
using JobsOffer.Api.Server.GenericController;
using Microsoft.AspNetCore.Mvc;

namespace JobsOffer.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilDomainJobController : GenericController<ProfilDomainJob, ProfilDomainJobViewModel>
    {
        public ProfilDomainJobController(IGenericService<ProfilDomainJob> genericService, IMapper mapper, ILogger<GenericController<ProfilDomainJob, ProfilDomainJobViewModel>> logger, IHostEnvironment hostEnvironment) : base(genericService, mapper, logger, hostEnvironment)
        {
        }
    }
}
