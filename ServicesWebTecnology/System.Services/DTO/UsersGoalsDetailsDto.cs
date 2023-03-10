using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.DTO
{
    public class UsersGoalsDetailsDto
    {
        public string TitleCategory { get; set; }
        public string Title { get; set; } = null!;
        public int Years { get; set; }
        public int Initialinvestment { get; set; }
        public int Monthlycontribution { get; set; }
        public int Targetamount { get; set; }
        public int Id { get; set; }
        public int Userid { get; set; }
        public DateTime Created { get; set; }
        public string Portfolio { get; set; }
        public string Financialentity { get; set; }
        public double Fullwithdrawal { get; set; }
        public double Totalcontributions { get; set; }
        public double? Goalfulfillmentpercentage { get; set; }
    }
}
