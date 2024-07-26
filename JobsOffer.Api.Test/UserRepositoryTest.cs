using JobsOffer.Api.Business.Cqrs.Commands.Interfaces;
using JobsOffer.Api.Business.Cqrs.Queries.Interfaces;
using JobsOffer.Api.Business.Services.Interfaces;
using JobsOffer.Api.Domain.GenericRepository.Interface;
using JobsOffer.Api.Infrastructure.DatabaseContext.DbContextJobsOffer;
using JobsOffer.Api.Infrastructure.Models.Classes;
using JobsOffer.Api.UnitOfWork.UnitOfWork.Interface;
using JobsOffer.Api.Business.Services.Classes;
using JobsOffer.Api.Business.Cqrs.Commands.Classes;
using JobsOffer.Api.Business.Cqrs.Queries.Classes;
using Bogus;
using JobsOffer.Api.Infrastructure.DatabaseContext.Seed.FakeData;
using System.Linq.Expressions;
using JobsOffer.Api.Business.Helpers;
using JobsOffer.Api.Business.SendEmails.Interface;
using JobsOffer.Api.Business.Services.SendEmails.Interface;
using JobsOffer.Api.Business.Services.SendEmails.Models.Classes;
using MailKit.Net.Smtp;
using JobsOffer.Api.Business.SendEmails.Classe;
using JobsOffer.Api.Business.Services.SendEmails.Classe;
using MailKit;
using MimeKit;

namespace JobsOffer.Api.Test
{
    public class UserRepositoryTest
    {
        #region ATTRIBUTES
        protected readonly Mock<IGenericRepository<User>> _mockGenericRepository;
        protected readonly Mock<IUnitOfWork<DbContextJobsOffer>> _mockUnitOfWork;
        protected readonly IUserCreateCommand _genericCreateCommand;
        protected readonly Mock<IUserCreateCommand> _mockGenericCreateCommand;
        protected readonly IUserUpdateCommand _genericUpdateCommand;
        protected readonly Mock<IUserUpdateCommand> _mockGenericUpdateCommand;
        protected readonly IGenericDeleteQuery<User> _genericDeleteQuery;
        protected readonly Mock<IGenericDeleteQuery<User>> _mockGenericDeleteQuery;
        protected readonly IGenericGetEntitiesQuery<User> _genericGetEntitiesQuery;
        protected readonly Mock<IGenericGetEntitiesQuery<User>> _mockGenericGetEntitiesQuery;
        protected readonly IUserService _genericService;
        protected readonly Mock<ISmtpClient> _mockSmtpClient;
        protected readonly IEmailConfigurationFactory _emailConfigurationFactory;
        protected readonly Mock<IEmailConfigurationFactory> _mockEmailConfigurationFactory;
        protected readonly ISendMailService _sendMailService;
        private EmailConfiguration _emailConfiguration;
        private IList<User>? _entities { get; set; }
        #endregion

        #region CONSTRUCTOR
        public UserRepositoryTest()
        {
            _mockGenericRepository = new Mock<IGenericRepository<User>>();
            _mockUnitOfWork = new Mock<IUnitOfWork<DbContextJobsOffer>>();
            _genericCreateCommand = new UserCreateCommand(_mockUnitOfWork.Object);
            _mockGenericCreateCommand = new Mock<IUserCreateCommand>();
            _genericUpdateCommand = new UserUpdateCommand(_mockUnitOfWork.Object);
            _mockGenericUpdateCommand = new Mock<IUserUpdateCommand>();
            _genericDeleteQuery = new GenericDeleteQuery<User>(_mockUnitOfWork.Object);
            _mockGenericDeleteQuery = new Mock<IGenericDeleteQuery<User>>();
            _genericGetEntitiesQuery = new GenericGetEntitiesQuery<User>(_mockUnitOfWork.Object);
            _mockGenericGetEntitiesQuery = new Mock<IGenericGetEntitiesQuery<User>>();
            _genericService = new UserService(_mockGenericCreateCommand.Object, _mockGenericUpdateCommand.Object, _mockGenericGetEntitiesQuery.Object, _mockGenericDeleteQuery.Object);
            _mockSmtpClient = new Mock<ISmtpClient>();
            _emailConfiguration = new EmailConfiguration()
            {
                SmtpServer = "192.168.1.98",
                SmtpPort = 1025,
                SmtpUsername = string.Empty,
                SmtpPassword = string.Empty
            };
            _emailConfigurationFactory = new EmailConfigurationFactory(_emailConfiguration);
            _mockEmailConfigurationFactory = new Mock<IEmailConfigurationFactory>();
            _sendMailService = new SendMailService(_mockSmtpClient.Object, _mockEmailConfigurationFactory.Object);
            _entities = UserFakeDataSeed.FakeDataUserSeed(300);
        }
        #endregion

        #region COMMANDS

        #region CREATE
        [Fact]
        public async Task CreateEntity_ShouldAddAndReturnEntity_WhenEntityIsNotNull()
        {
            try
            {
                // Arrange
                var entityMock = new Faker<User>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.ProfilId, f => 1)
                    .RuleFor(x => x.Email, f =>
                    {
                        return "user1@test.com";
                    })
                    .RuleFor(x => x.Password, f =>
                    {
                        return Helper.EncryptPassword(new User() { Email = "user1@test.com", Password = "123456" }).Password;
                    })
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.IsOnLine, f => true)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.CreateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericCreateCommand.Setup(x => x.Handle(entityMock!)).ReturnsAsync(entityMock);

                // Act
                var entity = await _genericService.CreateAsync(entityMock!);

                // Assert
                Assert.Equal(entityMock, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        [Fact]
        public async Task CreateEntity_ShouldReturnNull_WhenEntityIsIsNull()
        {
            try
            {
                // Arrange
                User? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.CreateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericCreateCommand.Setup(x => x.Handle(entityMock!)).ReturnsAsync(entityMock);

                // Act
                var entity = await _genericService.CreateAsync(entityMock!);

                // Assert
                Assert.Null(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region UPDATE
        [Fact]
        public async Task UpdateEntity_ShouldUpdateAndReturnEntity_WhenEntityIsNotNull()
        {
            try
            {
                // Arrange
                var entityMock = new Faker<User>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.ProfilId, f => 1)
                    .RuleFor(x => x.Email, f =>
                    {
                        return "user1@test.com";
                    })
                    .RuleFor(x => x.Password, f =>
                    {
                        return Helper.EncryptPassword(new User() { Email = "user1@test.com", Password = "123456" }).Password;
                    })
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.IsOnLine, f => true)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.UpdateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericUpdateCommand.Setup(x => x.Handle(entityMock!, false)).ReturnsAsync(entityMock);

                // Act
                var entity = await _genericService.UpdateAsync(entityMock!);

                // Assert
                Assert.Equal(entityMock, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        [Fact]
        public async Task UpdateEntity_ShouldReturnNull_WhenEntityIsIsNull()
        {
            try
            {
                // Arrange
                User? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.UpdateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericUpdateCommand.Setup(x => x.Handle(entityMock!, false)).ReturnsAsync(entityMock);

                // Act
                var entity = await _genericService.UpdateAsync(entityMock!);

                // Assert
                Assert.Null(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region DELETE
        [Fact]
        public async Task DeleteTEntity_ShouldDelete_WhenEntityIsNotNull()
        {
            try
            {
                // Arrange
                var entityMock = new Faker<User>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.ProfilId, f => 1)
                    .RuleFor(x => x.Email, f =>
                    {
                        return "user1@test.com";
                    })
                    .RuleFor(x => x.Password, f =>
                    {
                        return Helper.EncryptPassword(new User() { Email = "user1@test.com", Password = "123456" }).Password;
                    })
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.IsOnLine, f => true)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.DeleteAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericDeleteQuery.Setup(x => x.Handle(entityMock!)).ReturnsAsync(entityMock);

                // Act
                var entity = await _genericService.DeleteAsync(entityMock!);

                // Assert
                Assert.Equal(entityMock, entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task DeleteTEntity_ShouldReturnNull_WhenEntityIsNull()
        {
            try
            {
                // Arrange
                User? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.DeleteAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericDeleteQuery.Setup(x => x.Handle(entityMock!)).ReturnsAsync(entityMock);

                // Act
                var entity = await _genericService.DeleteAsync(entityMock!);

                // Assert
                Assert.Null(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #endregion

        #region QUERIES

        #region GET ALL
        [Fact]
        public void GetEntities_ShouldReturnListOfEntites_WhenDatabaseConnectionIsSet()
        {
            try
            {
                var entitiesMock = _entities!.AsQueryable();
                // Arrange
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.GetEntitiesAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0)).Returns(entitiesMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0,
                true)).Returns(entitiesMock);

                // Act
                var entities = (IList<User>)_genericService.GetEntitiesAsync(includes: "Jobs", inDatabase: true).ToList();

                // Assert
                var entitiesMockAsync = (IList<User>)entitiesMock.ToList();
                Assert.Equal(_entities, entitiesMockAsync);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region GET BY ID
        [Fact]
        public void GetEntityById_ShouldReturnEntity_WhenDatabaseConnectionIsSet()
        {
            try
            {
                var entityMock = _entities!.Where(x => x.Id == 1).AsQueryable();
                // Arrange
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.GetEntitiesAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0)).Returns(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0,
                true)).Returns(entityMock);

                // Act
                var entity = _genericService.GetEntitiesAsync(expression: x => x.Id == 1, inDatabase: true).SingleOrDefault();

                // Assert
                var entityMockAsync = entityMock.SingleOrDefault();
                Assert.Equal(entity, entityMockAsync);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #endregion

        #region AUTHENTICATION

        #region LOGIN
        [Fact]
        public async Task Authentication_ShouldReturnNull_WhenUserIsNull()
        {
            try
            {
                // Arrange
                User? entityMock = null;

                // Act
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var entity = await _genericService.Authenticate(entityMock, true);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

                // Assert
                Assert.Null(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task Authentication_ShouldReturnNull_WhenEmailOrPasswordIsNullOrStringEmpty()
        {
            try
            {
                // Arrange
                var entityMock = new User() { Email = string.Empty, Password = null };

                // Act
                var entity = await _genericService.Authenticate(entityMock, true);

                // Assert
                Assert.Null(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task Authentication_ShouldReturnNull_WhenEmailOrPasswordIsIncorrect()
        {
            try
            {
                // Arrange
                var entityMockQueryable = new List<User>() { new User() { Email = "user1@test.com", Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413" } }.AsQueryable();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.GetEntitiesAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0)).Returns(entityMockQueryable);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0,
                true)).Returns(entityMockQueryable);

                // Act
                var entity = await _genericService.Authenticate(new User() { Email = "user1@gmail.com", Password = "123456" }, true);

                // Assert
                Assert.Null(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task Authentication_ShouldReturnAttenpingUser_WhenEmailOrPasswordIsCorrect()
        {
            try
            {
                // Arrange
                var entityMockQueryable = new List<User>() { new User() { Email = "user1@test.com", Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413" } }.AsQueryable();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.GetEntitiesAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0)).Returns(entityMockQueryable);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0,
                true)).Returns(entityMockQueryable);

                // Act
                var entity = await _genericService.Authenticate(new User() { Email = "user1@test.com", Password = "123456" }, true);

                // Assert
                Assert.NotNull(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region LOGOUT
        [Fact]
        public async Task Logout_ShouldReturnFalse_WhenUserIsNull()
        {
            try
            {
                // Arrange
                User? entityMock = null;

                // Act
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var entity = await _genericService.Logout(entityMock, true);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

                // Assert
                Assert.False(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task Logout_ShouldReturnFalse_WhenReturnedUserFromHandleIsNull()
        {
            try
            {
                // Arrange
                var entityMockQueryable = new List<User>() { new User() { Email = "user1@test.com", IsOnLine = true, Id = 1 } }.AsQueryable();
                var entityMockUp = entityMockQueryable.ToList()[0];
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.GetEntitiesAsync(
               x => x.Id == entityMockUp.Id && x.Email == entityMockUp.Email,
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0)).Returns(entityMockQueryable);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(
               x => x.Id == entityMockUp.Id && x.Email == entityMockUp.Email,
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0,
                true)).Returns(entityMockQueryable);

                // Act
                var isLogout = await _genericService.Logout(new User() { Email = "user1@gmail.com", IsOnLine = true, Id = 1 }, true);

                // Assert
                Assert.False(isLogout);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task Logout_ShouldReturnTrue_WhenUserIsFound()
        {
            try
            {
                // Arrange
                var entityMockQueryable = new List<User>() { new User() { Email = "user1@test.com", IsOnLine = true, Id = 1 } }.AsQueryable();
                var entityMockUp = entityMockQueryable.ToList()[0];
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.GetEntitiesAsync(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0)).Returns(entityMockQueryable);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<User>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                null,
                ",",
                true,
                0,
                0,
                true)).Returns(entityMockQueryable);

                // Act
                var isLogout = await _genericService.Logout(new User() { Email = "user1@test.com", IsOnLine = true, Id = 1 }, true);

                // Assert
                Assert.True(isLogout);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region TOKEN
        [Fact]
        public void CreateToken_ShouldReturnToken_WhenUserAndJwtSecurityTokenInformationsIsNotNull()
        {
            try
            {
                // Arrange
                //object user, string keyString, string issuerString, string audienceString, int expireTokenDays
                var user = _entities!.Where(x => x.Id == 1).AsQueryable();
                var key = "i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415";
                var issuerString = "JobOfferAppApi";
                var audienceString = "public";
                var expireTokenDays = 2;

                // Act
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var token = _genericService.CreateToken(user, key, issuerString, audienceString, expireTokenDays);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

                // Assert
                Assert.NotNull(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNull_WhenUserIsNull()
        {
            try
            {
                // Arrange
                //object user, string keyString, string issuerString, string audienceString, int expireTokenDays
                User? user = null;
                var key = "i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415";
                var issuerString = "JobOfferAppApi";
                var audienceString = "public";
                var expireTokenDays = 2;

                // Act
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var token = _genericService.CreateToken(user, key, issuerString, audienceString, expireTokenDays);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

                // Assert
                Assert.Null(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNull_WhenKeyIsNull()
        {
            try
            {
                // Arrange
                //object user, string keyString, string issuerString, string audienceString, int expireTokenDays
                var user = _entities!.Where(x => x.Id == 1).AsQueryable();
                string? key = null;
                var issuerString = "JobOfferAppApi";
                var audienceString = "public";
                var expireTokenDays = 2;

                // Act
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var token = _genericService.CreateToken(user, key, issuerString, audienceString, expireTokenDays);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

                // Assert
                Assert.Null(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNull_WhenIssuerIsNull()
        {
            try
            {
                // Arrange
                //object user, string keyString, string issuerString, string audienceString, int expireTokenDays
                var user = _entities!.Where(x => x.Id == 1).AsQueryable();
                var key = "i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415";
                string? issuerString = null;
                var audienceString = "public";
                var expireTokenDays = 2;

                // Act
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var token = _genericService.CreateToken(user, key, issuerString, audienceString, expireTokenDays);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

                // Assert
                Assert.Null(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public void CreateToken_ShouldReturnNull_WhenAudienceIsNull()
        {
            try
            {
                // Arrange
                //object user, string keyString, string issuerString, string audienceString, int expireTokenDays
                var user = _entities!.Where(x => x.Id == 1).AsQueryable();
                var key = "i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415i2miCGkAl88GrN6ZrHf9r813s5RijKk4TUF4wcEOiobdDDS445415";
                var issuerString = "JobOfferAppApi";
                string? audienceString = null;
                var expireTokenDays = 2;

                // Act
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var token = _genericService.CreateToken(user, key, issuerString, audienceString, expireTokenDays);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

                // Assert
                Assert.Null(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #endregion

        #region SEND MAILS
        [Fact]
        public async Task SendMail_ShouldSendAndReturnNull_WhenEmailMessageIsNull()
        {
            try
            {
                // Arrange
                EmailMessage? emailMessage = null;
                _mockEmailConfigurationFactory.Setup(x => x.GetEmailConfiguration()).Returns(_emailConfiguration);

                // Act
                var response = await _sendMailService.Send(emailMessage);

                // Assert
                Assert.Null(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task SendMail_ShouldSendAndReturnNull_WhenEmailMessageToAddressesIsNullAndFromAddressesIsEmpty()
        {
            try
            {
                // Arrange
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                var emailMessage = new EmailMessage()
                {
                    ToAddresses = null,
                    FromAddresses = new List<EmailAddress>()
                };
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _mockEmailConfigurationFactory.Setup(x => x.GetEmailConfiguration()).Returns(_emailConfiguration);

                // Act
                var response = await _sendMailService.Send(emailMessage);

                // Assert
                Assert.Null(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task SendMail_ShouldSendAndReturnNull_WhenEmailMessageToAddressesIsEmptyAndFromAddressesIsNull()
        {
            try
            {
                // Arrange
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                var emailMessage = new EmailMessage()
                {
                    ToAddresses = new List<EmailAddress>(),
                    FromAddresses = null
                };
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
                _mockEmailConfigurationFactory.Setup(x => x.GetEmailConfiguration()).Returns(_emailConfiguration);

                // Act
                var response = await _sendMailService.Send(emailMessage);

                // Assert
                Assert.Null(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task SendMail_ShouldSendAndReturnNull_WhenEmailConfigurationFactorySmtpServerIsNull()
        {
            try
            {
                // Arrange
                var emailMessage = new EmailMessage()
                {
                    ToAddresses = new List<EmailAddress>()
                    {
                        new()
                        {
                            Name = "Marouane",
                            Address = "marouane@test.com"
                        }
                    },
                    FromAddresses = new List<EmailAddress>()
                    {
                        new()
                        {
                            Name = "Ahmed",
                            Address = "ahmed@test.com"
                        }
                    },
                    Subject = "Test Email Subject",
                    Content = "Test Email Content"
                };
                EmailConfiguration? emailConfiguration = new()
                {
                    SmtpServer = null,
                    SmtpPort = 1025
                };
                _mockEmailConfigurationFactory.Setup(x => x.GetEmailConfiguration()).Returns(emailConfiguration);

                // Act
                var response = await _sendMailService.Send(emailMessage);

                // Assert
                Assert.Null(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task SendMail_ShouldSendAndReturnNull_WhenEmailConfigurationFactorySmtpPortEqualsZero()
        {
            try
            {
                // Arrange
                var emailMessage = new EmailMessage()
                {
                    ToAddresses = new List<EmailAddress>()
                    {
                        new()
                        {
                            Name = "User 1",
                            Address = "user1@test.com"
                        }
                    },
                    FromAddresses = new List<EmailAddress>()
                    {
                        new()
                        {
                            Name = "User 2",
                            Address = "user2@test.com"
                        }
                    },
                    Subject = "Test Email Subject",
                    Content = "Test Email Content"
                };
                EmailConfiguration? emailConfiguration = new()
                {
                    SmtpServer = "localhost",
                    SmtpPort = 0
                };
                _mockEmailConfigurationFactory.Setup(x => x.GetEmailConfiguration()).Returns(emailConfiguration);

                // Act
                var response = await _sendMailService.Send(emailMessage);

                // Assert
                Assert.Null(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [Fact]
        public async Task SendMail_ShouldSendAndReturnResponse_WhenEmailConfigurationFactoryIsWellSetAndEmailMessageIsNotNullAndWellSet()
        {
            try
            {
                // Arrange
                var emailMessage = new EmailMessage()
                {
                    ToAddresses = new List<EmailAddress>()
                    {
                        new()
                        {
                            Name = "User 1",
                            Address = "user1@test.com"
                        }
                    },
                    FromAddresses = new List<EmailAddress>()
                    {
                        new()
                        {
                            Name = "User 2",
                            Address = "user2@test.com"
                        }
                    },
                    Subject = "Test Email Subject",
                    Content = "Test Email Content"
                };
                _mockEmailConfigurationFactory.Setup(x => x.GetEmailConfiguration()).Returns(_emailConfiguration);
                _mockSmtpClient.Setup(x => x.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>())).ReturnsAsync("Email Sent !");

                // Act
                var response = await _sendMailService.Send(emailMessage);

                // Assert
                Assert.NotNull(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}
