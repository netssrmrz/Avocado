using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.data
{
  public class AvocadoDb : System.Data.Entity.DbContext
  {
    public AvocadoDb(): base()
    {

    }

    public System.Data.Entity.DbSet<UnitTests.domain.Person> People { get; set; }
  }
}
