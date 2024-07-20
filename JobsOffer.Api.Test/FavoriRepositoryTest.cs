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

namespace JobsOffer.Api.Test
{
    public class FavoriRepositoryTest
    {
        #region ATTRIBUTES
        protected readonly Mock<IGenericRepository<Favori>> _mockGenericRepository;
        protected readonly Mock<IUnitOfWork<DbContextJobsOffer>> _mockUnitOfWork;
        protected readonly IGenericCreateCommand<Favori> _genericCreateCommand;
        protected readonly Mock<IGenericCreateCommand<Favori>> _mockGenericCreateCommand;
        protected readonly IGenericUpdateCommand<Favori> _genericUpdateCommand;
        protected readonly Mock<IGenericUpdateCommand<Favori>> _mockGenericUpdateCommand;
        protected readonly IGenericDeleteQuery<Favori> _genericDeleteQuery;
        protected readonly Mock<IGenericDeleteQuery<Favori>> _mockGenericDeleteQuery;
        protected readonly IGenericGetEntitiesQuery<Favori> _genericGetEntitiesQuery;
        protected readonly Mock<IGenericGetEntitiesQuery<Favori>> _mockGenericGetEntitiesQuery;
        protected readonly IGenericService<Favori> _genericService;
        private IList<Favori>? _entities { get; set; }
        #endregion

        #region CONSTRUCTOR
        public FavoriRepositoryTest()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Favori>>();
            _mockUnitOfWork = new Mock<IUnitOfWork<DbContextJobsOffer>>();
            _genericCreateCommand = new GenericCreateCommand<Favori>(_mockUnitOfWork.Object);
            _mockGenericCreateCommand = new Mock<IGenericCreateCommand<Favori>>();
            _genericUpdateCommand = new GenericUpdateCommand<Favori>(_mockUnitOfWork.Object);
            _mockGenericUpdateCommand = new Mock<IGenericUpdateCommand<Favori>>();
            _genericDeleteQuery = new GenericDeleteQuery<Favori>(_mockUnitOfWork.Object);
            _mockGenericDeleteQuery = new Mock<IGenericDeleteQuery<Favori>>();
            _genericGetEntitiesQuery = new GenericGetEntitiesQuery<Favori>(_mockUnitOfWork.Object);
            _mockGenericGetEntitiesQuery = new Mock<IGenericGetEntitiesQuery<Favori>>();
            _genericService = new GenericService<Favori>(_mockGenericCreateCommand.Object, _mockGenericUpdateCommand.Object, _mockGenericGetEntitiesQuery.Object, _mockGenericDeleteQuery.Object);
            _entities = FavoriFakeDataSeed.FakeDataFavoriSeed(300);
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
                var entityMock = new Faker<Favori>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.UserId, f => 1)
                    .RuleFor(x => x.JobId, f => 1)
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.CreateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
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
                Favori? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.CreateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
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
                var entityMock = new Faker<Favori>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.UserId, f => 1)
                    .RuleFor(x => x.JobId, f => 1)
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.UpdateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
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
                Favori? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.UpdateAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
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
                var entityMock = new Faker<Favori>("fr")
                    .RuleFor(x => x.Id, f => 301)
                    .RuleFor(x => x.UserId, f => 1)
                    .RuleFor(x => x.JobId, f => 1)
                    .RuleFor(x => x.CreateDate, f => f.Date.Past())
                    .RuleFor(x => x.CreatedBy, f => f.Person.FullName)
                    .RuleFor(x => x.UpdateDate, f => f.Date.Past())
                    .RuleFor(x => x.UpdatedBy, f => f.Person.FullName)
                    .Generate(1).SingleOrDefault();
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.DeleteAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
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
                Favori? entityMock = null;
#pragma warning disable CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockGenericRepository.Setup(x => x.DeleteAsync(entityMock!)).ReturnsAsync(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
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
                It.IsAny<Expression<Func<Favori, bool>>>(),
                It.IsAny<Func<IQueryable<Favori>, IOrderedQueryable<Favori>>>(),
                "Jobs",
                ",",
                true,
                0,
                0)).Returns(entitiesMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(It.IsAny<Expression<Func<Favori, bool>>>(),
                It.IsAny<Func<IQueryable<Favori>, IOrderedQueryable<Favori>>>(),
                "Jobs",
                ",",
                true,
                0,
                0)).Returns(entitiesMock);

                // Act
                var entities = (IList<Favori>)_genericService.GetEntitiesAsync(includes: "Jobs").ToList();

                // Assert
                var entitiesMockAsync = (IList<Favori>)entitiesMock.ToList();
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
                It.IsAny<Func<IQueryable<Favori>, IOrderedQueryable<Favori>>>(),
                "Jobs",
                ",",
                true,
                0,
                0)).Returns(entityMock);
#pragma warning restore CS8620 // Impossible d'utiliser l'argument pour le paramètre, car il existe des différences dans l'acceptation des valeurs null par les types référence.
                _mockUnitOfWork.Setup(x => x.GetGenericRepository<Favori>()).Returns(_mockGenericRepository.Object);
                _mockGenericGetEntitiesQuery.Setup(x => x.Handle(x => x.Id == 1,
                It.IsAny<Func<IQueryable<Favori>, IOrderedQueryable<Favori>>>(),
                "Jobs",
                ",",
                true,
                0,
                0)).Returns(entityMock);

                // Act
                var entity = _genericService.GetEntitiesAsync(expression: x => x.Id == 1, includes: "Jobs").SingleOrDefault();

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
    }
}
