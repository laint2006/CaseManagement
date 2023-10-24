using Aperia.CaseManagement.Api.Entities;
using Aperia.CaseManagement.Api.Persistence;

namespace Aperia.CaseManagement.Api.Repositories
{
    /// <summary>
    /// The Contact Repository
    /// </summary>
    /// <seealso cref="Aperia.CaseManagement.Api.Repositories.Repository{Inquiry}" />
    /// <seealso cref="Aperia.CaseManagement.Api.Repositories.IInquiryRepository" />
    public class InquiryRepository : Repository<Inquiry>, IInquiryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InquiryRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public InquiryRepository(CaseManagementContext dbContext)
            : base(dbContext)
        {
        }

    }
}