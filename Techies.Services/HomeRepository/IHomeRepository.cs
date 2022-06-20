using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techies.Common.ServiceExtensions.RepositoryPatternExtension;
using Techies.Data;

namespace Techies.Services.HomeRepository
{
    public interface IHomeRepository : IRepository<staff> { }
}
