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
    public class WebSiteRepositoryTest
    {
        #region ATTRIBUTES
        protected readonly Mock<IGenericRepository<WebSite>> _mockGenericRepository;
        protected readonly Mock<IUnitOfWork<DbContextJobsOffer>> _mockUnitOfWork;
        protected readonly IGenericCreateCommand<WebSite> _genericCreateCommand;
        protected readonly Mock<IGenericCreateCommand<WebSite>> _mockGenericCreateCommand;
        protected readonly IGenericUpdateCommand<WebSite> _genericUpdateCommand;
        protected readonly Mock<IGenericUpdateCommand<WebSite>> _mockGenericUpdateCommand;
        protected readonly IGenericDeleteQuery<WebSite> _genericDeleteQuery;
        protected readonly Mock<IGenericDeleteQuery<WebSite>> _mockGenericDeleteQuery;
        protected readonly IGenericGetEntitiesQuery<WebSite> _genericGetEntitiesQuery;
        protected readonly Mock<IGenericGetEntitiesQuery<WebSite>> _mockGenericGetEntitiesQuery;
        protected readonly IGenericService<WebSite> _genericService;
        protected readonly Mock<ISmtpClient> _mockSmtpClient;
        protected readonly IEmailConfigurationFactory _emailConfigurationFactory;
        protected readonly Mock<IEmailConfigurationFactory> _mockEmailConfigurationFactory;
        protected readonly ISendMailService _sendMailService;
        private EmailConfiguration _emailConfiguration;
        private IList<WebSite>? _entities { get; set; }
        #endregion

        #region CONSTRUCTOR
        public WebSiteRepositoryTest()
        {
            _mockGenericRepository = new Mock<IGenericRepository<WebSite>>();
            _mockUnitOfWork = new Mock<IUnitOfWork<DbContextJobsOffer>>();
            _genericCreateCommand = new GenericCreateCommand<WebSite>(_mockUnitOfWork.Object);
            _mockGenericCreateCommand = new Mock<IGenericCreateCommand<WebSite>>();
            _genericUpdateCommand = new GenericUpdateCommand<WebSite>(_mockUnitOfWork.Object);
            _mockGenericUpdateCommand = new Mock<IGenericUpdateCommand<WebSite>>();
            _genericDeleteQuery = new GenericDeleteQuery<WebSite>(_mockUnitOfWork.Object);
            _mockGenericDeleteQuery = new Mock<IGenericDeleteQuery<WebSite>>();
            _genericGetEntitiesQuery = new GenericGetEntitiesQuery<WebSite>(_mockUnitOfWork.Object);
            _mockGenericGetEntitiesQuery = new Mock<IGenericGetEntitiesQuery<WebSite>>();
            _genericService = new GenericService<WebSite>(_mockGenericCreateCommand.Object, _mockGenericUpdateCommand.Object, _mockGenericGetEntitiesQuery.Object, _mockGenericDeleteQuery.Object);
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
            _entities = WebSiteFakeDataSeed.FakeDataWebSiteSeed(300);
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
                var entityMock = new Faker<WebSite>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.SiteName, f => f.Company.CompanyName())
                    .RuleFor(x => x.SiteUrl, f => f.Internet.Url())
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.CreateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
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
                WebSite? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.CreateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
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
                var entityMock = new Faker<WebSite>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.SiteName, f => f.Company.CompanyName())
                    .RuleFor(x => x.SiteUrl, f => f.Internet.Url())
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.UpdateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
                _mockGenericUpdateCommand.Setup(x => x.Handle(entityMock!)).ReturnsAsync(entityMock);

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
                WebSite? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.UpdateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
                _mockGenericUpdateCommand.Setup(x => x.Handle(entityMock!)).ReturnsAsync(entityMock);

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
                var entityMock = new Faker<WebSite>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.SiteName, f => f.Company.CompanyName())
                    .RuleFor(x => x.SiteUrl, f => f.Internet.Url())
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.DeleteAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
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
                WebSite? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.DeleteAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
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
                It.IsAny<Expression<Func<WebSite, bool>>>(),
                It.IsAny<Func<IQueryable<WebSite>, IOrderedQueryable<WebSite>>>(),
                "Jobs",
                ",",
                true,
                0,
                0)).Returns(entitiesMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(It.IsAny<Expression<Func<WebSite, bool>>>(),
                It.IsAny<Func<IQueryable<WebSite>, IOrderedQueryable<WebSite>>>(),
                "Jobs",
                ",",
                true,
                0,
                0,
                true)).Returns(entitiesMock);

                // Act
                var entities = (IList<WebSite>)_genericService.GetEntitiesAsync(includes: "Jobs", inDatabase: true).ToList();

                // Assert
                var entitiesMockAsync = (IList<WebSite>)entitiesMock.ToList();
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
                x => x.Id == 1,
                It.IsAny<Func<IQueryable<WebSite>, IOrderedQueryable<WebSite>>>(),
                "Jobs",
                ",",
                true,
                0,
                0)).Returns(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<WebSite>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(x => x.Id == 1,
                It.IsAny<Func<IQueryable<WebSite>, IOrderedQueryable<WebSite>>>(),
                "Jobs",
                ",",
                true,
                0,
                0,
                true)).Returns(entityMock);

                // Act
                var entity = _genericService.GetEntitiesAsync(expression: x => x.Id == 1, includes: "Jobs", inDatabase: true).SingleOrDefault();

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
