using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Infrastructure.DTO
{
    public class UsersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } 
        public string FullNameAdvisor { get; set; }
        public DateTime Created { get; set; }
   
    }
}
