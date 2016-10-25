using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _475_Lab_4_Part_3
{
    public class StandardRepository : Repository<Standard>, IStandardRepository
    {
        public StandardRepository() : base(new SchoolDBEntities())
        {

        }
    }
}
