using Effort;
using System.Data.Common;
using University;

namespace UniversityTest
{
    public class FakeContextFactory : IContextFactory
    {
        public UniversityContext Create()
        {
            DbConnection connection = DbConnectionFactory.CreateTransient();
            return new UniversityContext(connection);
        }
    }
}
