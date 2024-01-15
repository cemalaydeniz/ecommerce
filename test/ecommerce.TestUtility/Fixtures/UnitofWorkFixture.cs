using ecommerce.Application.UnitofWorks;
using ecommerce.Persistence.Context;
using ecommerce.Persistence.Interceptors;
using ecommerce.Persistence.UnitofWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ecommerce.TestUtility.Fixtures
{
    public class UnitofWorkFixture : IDisposable
    {
        public IUnitofWork UnitofWork { get; private set; }
        public Mock<IMediator> MockMediator { get; private set; }

        public AppDbContext _dbContext;

        public UnitofWorkFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            MockMediator = new Mock<IMediator>();
            _dbContext = new AppDbContext(options,
                new PublishDomainEventsInterceptor(MockMediator.Object));

            UnitofWork = new UnitofWork(_dbContext);
        }

        public void Dispose()
        {
            UnitofWork.Dispose();
            _dbContext.Dispose();
        }
    }
}
