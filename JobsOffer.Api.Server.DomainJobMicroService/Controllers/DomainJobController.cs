using AutoMapper;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Server.DtoModel.Models;
using JobsOffer.Api.Server.GenericController;
using JobsOffer.Api.Server.RealTime.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace JobsOffer.Api.Server.DomainJobMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainJobController : GenericController<DomainJob, DomainJobViewModel>
    {
        public DomainJobController(
            IGenericService<DomainJob> genericService, 
            IMapper mapper, ILogger<GenericController<DomainJob, DomainJobViewModel>> logger,
            IHostEnvironment hostEnvironment, 
            IHubContext<RealTimeHub> hubContext,
            IMemoryCache cache) : base(genericService, mapper, logger, hostEnvironment, hubContext, cache)
        {
        }
    }
}
