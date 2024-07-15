using AutoMapper;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Server.DtoModel.Models;
using JobsOffer.Api.Server.GenericController;
using JobsOffer.Api.Server.RealTime.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace JobsOffer.Api.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriController : GenericController<Favori, FavoriViewModel>
    {
        public FavoriController(
            IGenericService<Favori> genericService, 
            IMapper mapper, 
            ILogger<GenericController<Favori, FavoriViewModel>> logger, 
            IHostEnvironment hostEnvironment,
            IHubContext<RealTimeHub> hubContext) : base(genericService, mapper, logger, hostEnvironment, hubContext)
        {
        }
    }
}
