using Repository.Models;
using Repository.Repositories.BaseRepository;

namespace Repository.Repositories.CandidateProfileRepository
{
    public class CandidateProfileRepository : BaseRepository<JobManagementContext, CandidateProfile>, ICandidateProfileRepository
    {
        public CandidateProfileRepository(JobManagementContext context) : base(context)
        {
        }
    }
}
