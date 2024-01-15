using AutoMapper;
using System.Reflection;

namespace ecommerce.TestUtility.Fixtures
{
    public class AutoMapperFixture : IDisposable
    {
        public IMapper Mapper { get; private set; }
        public AutoMapperFixture()
        {
            MapperConfiguration config = new MapperConfiguration(_ =>
            {
                _.AddMaps(typeof(Application.ServiceRegistration).GetTypeInfo().Assembly);
                _.AddMaps(typeof(API.Mappings.UserController.SignUpMapping).GetTypeInfo().Assembly);
            });

            Mapper = config.CreateMapper();
        }

        public void Dispose() { }
    }
}
