using AutoMapper;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Server.DtoModel.Models;
using JobsOffer.Api.Server.GenericController;
using JobsOffer.Api.Server.RealTime.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace JobsOffer.Api.Server.JobMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : GenericController<Job, JobViewModel>
    {
        public JobController(
            IGenericService<Job> genericService, 
            IMapper mapper, 
            ILogger<GenericController<Job, JobViewModel>> logger, 
            IHostEnvironment hostEnvironment,
            IHubContext<RealTimeHub> hubContext,
            IMemoryCache cache) : base(genericService, mapper, logger, hostEnvironment, hubContext, cache)
        {
        }
    }
}
