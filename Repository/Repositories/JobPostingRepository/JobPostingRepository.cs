using Repository.Models;
using Repository.Repositories.BaseRepository;
using Repository.Repositories.JobPostingRepository;

namespace Repository.Repositories.JobPostingRepository
{
    public class JobPostingRepository : BaseRepository<JobManagementContext, JobPosting>, IJobPostingRepository
    {
        public JobPostingRepository(JobManagementContext context) : base(context)
        {
        }
    }
}
