using System.Infrastructure.DTO;
using System.Linq;
using SystemQuickzal.Data.Entity;

namespace System.Infrastructure.IRepository
{
    public interface IUsersRepository : IGenericRepository<User>
    {

        IQueryable<UsersDto> GetUser(int Id);
        IQueryable<UsersGoalsDto> UsersGoals(int Id);
        IQueryable<UsersGoalsDetailsDto> UsersGoalsDetails(int UserId, int GoalsId);
        UsersummaryDto GetUsersummary(int userid);


    }
}

