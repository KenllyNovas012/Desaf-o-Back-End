using System.Infrastructure.DTO;
using System.Infrastructure.IRepository;
using System.Linq;
using SystemQuickzal.Contexts;
using SystemQuickzal.Data.Entity;

namespace System.Infrastructure.Repository
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        private readonly AplicationDataContext _context;
        public UsersRepository(AplicationDataContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        ///  1. Traer un usuario
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<UsersDto> GetUser(int Id)
        {

            var query = (from use in _context.Users
                         join Adv in _context.Users on use.Id equals Adv.Advisorid
                         where use.Id==Id
                         select new UsersDto
                         {
                             Id = use.Id,
                             FullName = use.Firstname +" "+use.Surname,
                             FullNameAdvisor = Adv.Firstname +" "+ Adv.Surname,
                             Created = use.Created,
                         }); 
            return query;
        }

        /// <summary>
        ///  Traer metas de un usuario 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<UsersGoalsDto> UsersGoals(int Id)
        {

            var query = (from gol in _context.Goals
                         join use in _context.Users on gol.Userid equals use.Id
                         join port in _context.Portfolios on gol.Portfolioid equals port.Id
                         join finaci in _context.Financialentities on gol.Financialentityid equals finaci.Id
                         where use.Id == Id
                         select new UsersGoalsDto
                         {
                             Id = gol.Id,
                             Title = gol.Title,
                             Years = gol.Years,
                             Initialinvestment = gol.Initialinvestment,
                             Monthlycontribution = gol.Monthlycontribution,
                             Targetamount = gol.Targetamount,
                             Created = gol.Created,
                             Portfolio = port.Title,
                             Financialentity = finaci.Title,
                         });
            return query;
        }

        /// <summary>
        ///  Traer detalle de una meta
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<UsersGoalsDetailsDto> UsersGoalsDetails(int UserId, int GoalsId)
        {

            var query = (from gol in _context.Goals
                         join GoC in _context.Goalcategories on gol.Goalcategoryid equals GoC.Id
                         join use in _context.Users on gol.Userid equals use.Id
                         join port in _context.Portfolios on gol.Portfolioid equals port.Id
                         join finaci in _context.Financialentities on gol.Financialentityid equals finaci.Id
                         where use.Id == UserId && gol.Id == GoalsId
                         select new UsersGoalsDetailsDto
                         {
                             Id = gol.Id,
                             Title = gol.Title,
                             TitleCategory = GoC.Title,
                             Years = gol.Years,
                             Initialinvestment = gol.Initialinvestment,
                             Monthlycontribution = gol.Monthlycontribution,
                             Targetamount = gol.Targetamount,
                             Created = gol.Created,
                             Portfolio = port.Title,
                             Financialentity = finaci.Title,
                             Totalcontributions = Totalcontributions(UserId,GoalsId),
                             Fullwithdrawal = Fullwithdrawal(UserId, GoalsId),
                             Goalfulfillmentpercentage= (GetUsersummary(UserId, GoalsId).Balance / gol.Targetamount)*100
                         });
            return query;
        }

        /// <summary>
        ///  Traer el resumen del usuario actual (considerar moneda)* 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public UsersummaryDto GetUsersummary(int userid, int? goalid)
        {
            var query = (from gol in _context.Goaltransactionfundings
                         join tran in _context.Goaltransactions on gol.Transactionid equals tran.Id
                         join use in _context.Users on gol.Ownerid equals use.Id
                         join fun in _context.Fundings on gol.Fundingid equals fun.Id
                         join funSh in _context.Fundingsharevalues on fun.Id equals funSh.Fundingid
                         join cur in _context.Currencies on use.Currencyid equals cur.Id
                         join cuin in _context.Currencyindicators on use.Currencyid equals cuin.Sourcecurrencyid
                         where use.Id == userid && gol.Goalid == (goalid == null ? gol.Goalid:goalid) 
                         select new UsersummaryDto
                         {
                             Balance = gol.Quotas * funSh.Value * cuin.Value,
                             AportesActuales = tran.Amount,
                         });

            var response = new UsersummaryDto();
            response.Balance = query.Sum(x => x.Balance);
            response.AportesActuales = query.Sum(x=>x.AportesActuales);
            return response;
        }

        public  double Totalcontributions(int? UserId, int? GoalsId)
        {
            double GoalTotal = _context.Goaltransactions.Where(x => x.Ownerid == UserId && x.Goalid== GoalsId && x.Type=="buy").Select(a => a.Amount).Sum() ?? 0;

            return GoalTotal;
        }

        public double Fullwithdrawal(int? UserId, int? GoalsId)
        {
            double GoalTotal = _context.Goaltransactions.Where(x => x.Ownerid == UserId && x.Goalid == GoalsId && x.Type == "sale").Select(a => a.Amount).Sum() ?? 0;

            return GoalTotal;
        }

    }
}
