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
    public class FavoriController : GenericController<Favori, FavoriViewModel>
    {
        public FavoriController(IGenericService<Favori> genericService, IMapper mapper, ILogger<GenericController<Favori, FavoriViewModel>> logger, IHostEnvironment hostEnvironment) : base(genericService, mapper, logger, hostEnvironment)
        {
        }
    }
}
