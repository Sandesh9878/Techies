using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techies.Common.ServiceExtensions.RepositoryPatternExtension;
using Techies.Data;

namespace Techies.Services.HomeRepository
{
    public class HomeRepository : EfCoreRepository<staff>,  IHomeRepository
    {
        public HomeRepository(TechiesContext techiesContext): base(techiesContext){}
    }
}
