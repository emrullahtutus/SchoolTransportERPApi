using SchoolTransport.Application.Repositories;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class DriverRepository : RepositoryBase<Domain.Entities.Driver.Driver>, IDriverRepository
    {
        public DriverRepository(SchoolTransportDbContext context) : base(context)
        {
        }
    }
}
