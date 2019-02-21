using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.domain
{
  public class Person
  {
    public long Id { get; set; }

    [Avocado.Csv.Column(0)]
    public string Name { get; set; }

    [Avocado.Csv.Column(1)]
    public int Age { get; set; }

    [Avocado.Csv.Column(2)]
    public float Height { get; set; }

    [Avocado.Csv.Column(3)]
    public System.DateTime Dob { get; set; }
  }
}
