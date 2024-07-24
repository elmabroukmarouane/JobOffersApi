using AutoMapper;
using JobsOffer.Api.Business.Helpers;
using JobsOffer.Api.Business.Helpers.LambdaManagement.Helper;
using JobsOffer.Api.Business.Helpers.LambdaManagement.Models;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.Server.DtoModel.Models;
using JobsOffer.Api.Server.Extensions.Logging;
using JobsOffer.Api.Server.RealTime.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace JobsOffer.Api.Server.UserMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region ATTRIBUTES
        protected readonly IUserService _userService;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly IHostEnvironment _hostEnvironment;
        protected readonly IConfiguration _configuration;
        protected readonly IHubContext<RealTimeHub> _realTimeHub;
        protected readonly IMemoryCache _cache;
        #endregion

        #region CONSTRUCTOR
        public UserController(
            IUserService userService,
            IMapper mapper,
            ILogger<GenericController.GenericController<User, UserViewModel>> logger,
            IHostEnvironment hostEnvironment,
            IConfiguration configuration,
            IHubContext<RealTimeHub> realTimeHub,
            IMemoryCache cache)
        {
            _userService = userService ?? throw new ArgumentException(null, nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(null, nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(null, nameof(logger));
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException(null, nameof(hostEnvironment));
            _configuration = configuration ?? throw new ArgumentNullException(null, nameof(configuration));
            _realTimeHub = realTimeHub ?? throw new ArgumentNullException(null, nameof(realTimeHub));
            _cache = cache ?? throw new ArgumentNullException(null, nameof(cache));

        }
        #endregion

        #region READ
        [HttpGet]
        public virtual async Task<IActionResult> Get(string? includes = null)
        {
            try
            {
                var list = await _userService.GetEntitiesAsync(includes: includes).ToListAsync();
                if (list == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Get()", _hostEnvironment.ContentRootPath);
                    return NotFound(new
                    {
                        Message = "List not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var mappedList = _mapper.Map<IList<UserViewModel>>(list);
                return Ok(mappedList);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Get()", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }

        [HttpGet("{id:int}")]
        public virtual async Task<IActionResult> Get(int id, string? includes = null)
        {
            try
            {
                var row = await _userService.GetEntitiesAsync(expression: x => x.Id == id, includes: includes).SingleOrDefaultAsync();
                if (row == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Get(int id)", _hostEnvironment.ContentRootPath);
                    return NotFound(new
                    {
                        Message = "Item not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var mappedRow = _mapper.Map<UserViewModel>(row);
                return Ok(mappedRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Get(int id)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }

        // TODO : Make the same for orderBy like LambdaExpressionModel
        [HttpPost("filter")]
        public virtual async Task<IActionResult> Get(FilterDataModel filterDataModel)
        {
            try
            {
                var lambdaExpression = ExpressionBuilder.BuildLambda<User>(filterDataModel.LambdaExpressionModel);
                var filteredRows = await _userService.GetEntitiesAsync(lambdaExpression, includes: filterDataModel.Includes, splitChar: filterDataModel.SplitChar, disableTracking: filterDataModel.DisableTracking, take: filterDataModel.Take, offset: filterDataModel.Offset).ToListAsync();
                if (filteredRows == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(filter)", _hostEnvironment.ContentRootPath);
                    return NotFound(new
                    {
                        Message = "List not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var mappedFilteredRows = _mapper.Map<IList<UserViewModel>>(filteredRows);
                return Ok(mappedFilteredRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Post(filter)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }
        #endregion

        #region CREATE
        [HttpPost]
        public virtual async Task<IActionResult> Post(UserViewModel? entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(UserViewModel entity)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntity = _mapper.Map<User>(entity);
                var row = await _userService.CreateAsync(reverseMapEntity);
                if (row == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "ROW IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(UserViewModel entity)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Added Row is null !"
                    });
                }
                HelperCache.AddCache(row, row.GetType().Name, _cache);
                var mapperRow = _mapper.Map<UserViewModel>(row);
                await _realTimeHub.Clients.All.SendAsync("Row Created !", mapperRow);
                return Ok(mapperRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Post(UserViewModel entity)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }

        [HttpPost("AddRange")]
        public virtual async Task<IActionResult> Post(IList<UserViewModel> entities)
        {
            try
            {
                if (entities == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(IList<UserViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntities = _mapper.Map<IList<User>>(entities);
                var rows = await _userService.CreateAsync(reverseMapEntities);
                if (rows == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "ROWS IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Post(IList<UserViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Added Rows is null !"
                    });
                }
                HelperCache.AddCache(rows, rows.GetType().Name, _cache);
                var mapperRows = _mapper.Map<IList<UserViewModel>>(rows);
                await _realTimeHub.Clients.All.SendAsync("Rows Created !", mapperRows);
                return Ok(mapperRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Post(IList<UserViewModel> entities)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }
        #endregion

        #region UPDATE
        [HttpPut("id:int")]
        public virtual async Task<IActionResult> Put(UserViewModel? entity, int id)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(UserViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                HelperCache.DeleteCache(entity, entity.GetType().Name, _cache);
                var reverseMapEntity = _mapper.Map<User>(entity);
                var row = await _userService.UpdateAsync(reverseMapEntity);
                if (row == null)
                {
                    HelperCache.AddCache(entity, entity.GetType().Name, _cache);
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "ROW IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(UserViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Updated Row is null !"
                    });
                }
                HelperCache.AddCache(row, row.GetType().Name, _cache);
                var mapperRow = _mapper.Map<UserViewModel>(row);
                await _realTimeHub.Clients.All.SendAsync("Row Updated !", mapperRow);
                return Ok(mapperRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Put(UserViewModel entity, int id)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }

        [HttpPut("UpdateRange")]
        public virtual async Task<IActionResult> Put(IList<UserViewModel> entities)
        {
            try
            {
                if (entities == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(IList<UserViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                HelperCache.DeleteCache(entities, entities.GetType().Name, _cache);
                var reverseMapEntities = _mapper.Map<IList<User>>(entities);
                var rows = await _userService.UpdateAsync(reverseMapEntities);
                if (rows == null)
                {
                    HelperCache.AddCache(entities, entities.GetType().Name, _cache);
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "ROWS IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Put(IList<UserViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Updated Rows is null !"
                    });
                }
                HelperCache.AddCache(rows, rows.GetType().Name, _cache);
                var mapperRows = _mapper.Map<IList<UserViewModel>>(rows);
                await _realTimeHub.Clients.All.SendAsync("Rows Updated !", mapperRows);
                return Ok(mapperRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Put(IList<UserViewModel> entities)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(UserViewModel? entity, int id)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(UserViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntity = _mapper.Map<User>(entity);
                var row = await _userService.DeleteAsync(reverseMapEntity);
                if (row == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "ROW IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(UserViewModel entity, int id)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Deleted Row is null !"
                    });
                }
                HelperCache.DeleteCache(row, row.GetType().Name, _cache);
                var mapperRow = _mapper.Map<UserViewModel>(row);
                await _realTimeHub.Clients.All.SendAsync("Row Deleted !", mapperRow);
                return Ok(mapperRow);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Delete(UserViewModel entity, int id)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }

        [HttpDelete("DeleteRange")]
        public virtual async Task<IActionResult> Delete(IList<UserViewModel> entities)
        {
            try
            {
                if (entities == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "Entity IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(IList<UserViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Entity received is null !"
                    });
                }
                var reverseMapEntities = _mapper.Map<IList<User>>(entities);
                var rows = await _userService.DeleteAsync(reverseMapEntities);
                if (rows == null)
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "ROWS IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Delete(IList<UserViewModel> entities)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "Deleted Rows is null !"
                    });
                }
                HelperCache.DeleteCache(rows, rows.GetType().Name, _cache);
                var mapperRows = _mapper.Map<IList<UserViewModel>>(rows);
                await _realTimeHub.Clients.All.SendAsync("Rows Deleted !", mapperRows);
                return Ok(mapperRows);
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "" + " - Delete(IList<UserViewModel> entities)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Get failed !"
                });
            }
        }
        #endregion

        #region AUTHENTICATION
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(string? email, string? password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    _logger.LoggingMessageWarning("JobOffer.API", (int)HttpStatusCode.InternalServerError, "USER IS NULL !", HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() ?? "", " - Authenticate(UserViewModel _user)", _hostEnvironment.ContentRootPath);
                    return StatusCode(500,
                    new
                    {
                        Message = "User received is null !"
                    });
                }
                var _user = new User()
                {
                    Email = email,
                    Password = password
                };
                var user = await _userService.Authenticate(_user);
                if (user == null)
                {
                    return Unauthorized("Incorrect email or password");
                }
                var userViewModel = _mapper.Map<UserViewModel>(user);
                userViewModel.Password = string.Empty;
                var token = _userService.CreateToken(
                    userViewModel,
                    _configuration.GetSection("Jwt").GetSection("Key").Value ?? "",
                    _configuration.GetSection("Jwt").GetSection("Issuer").Value ?? "",
                    _configuration.GetSection("Jwt").GetSection("Audience").Value ?? "",
                    2);
                return Ok(new
                {
                    Token = token,
                });
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() + " - Authenticate(UserViewModel _user)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Authentication failed !"
                });
            }
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout(int id)
        {
            try
            {
                var user = await _userService.GetEntitiesAsync(x => x.Id == id).SingleOrDefaultAsync();
                if (user == null)
                {
                    return NotFound(new
                    {
                        Message = "User not found !",
                        StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                    });
                }
                var logout = await _userService.Logout(user);
                if (!logout)
                {
                    return StatusCode(400, new
                    {
                        Message = "Something happened when trying to logout !"
                    });
                }
                return Ok(new
                {
                    Message = "Hope to see you soon :) !"
                });
            }
            catch (Exception ex)
            {
                _logger.LoggingMessageError("JobOffer.API", (int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), HttpContext.Request.Method, ControllerContext?.RouteData?.Values["controller"]?.ToString() ?? "", ControllerContext?.RouteData?.Values["action"]?.ToString() + " - Logout(int id)", ex, _hostEnvironment.ContentRootPath);
                return StatusCode(500, new
                {
                    Message = "Logout failed !"
                });
            }
        }
        #endregion
    }
}
