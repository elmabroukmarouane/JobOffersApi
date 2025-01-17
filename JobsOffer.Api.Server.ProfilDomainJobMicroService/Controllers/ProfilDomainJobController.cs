﻿using AutoMapper;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Business.Services.SendEmails.Interface;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Server.DtoModel.Models;
using JobsOffer.Api.Server.GenericController;
using JobsOffer.Api.Server.RealTime.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace JobsOffer.Api.Server.ProfilDomainJobMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilDomainJobController : GenericController<ProfilDomainJob, ProfilDomainJobViewModel>
    {
        public ProfilDomainJobController(
            
            IGenericService<ProfilDomainJob> genericService, 
            IMapper mapper, 
            ILogger<GenericController<ProfilDomainJob, ProfilDomainJobViewModel>> logger, 
            IHostEnvironment hostEnvironment,
            IHubContext<RealTimeHub> hubContext,
            IMemoryCache cache,
            ISendMailService sendMailService) : base(genericService, mapper, logger, hostEnvironment, hubContext, cache, sendMailService)
        {
        }
    }
}
