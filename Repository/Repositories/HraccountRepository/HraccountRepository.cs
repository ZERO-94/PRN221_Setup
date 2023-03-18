using Repository.Models;
using Repository.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.HraccountRepository
{
    public class HraccountRepository : BaseRepository<JobManagementContext, Hraccount>, IHraccountRepository
    {
        public HraccountRepository(JobManagementContext context) : base(context)
        {
        }
    }
}
