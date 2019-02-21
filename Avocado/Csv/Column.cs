using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avocado.Csv
{
  [System.AttributeUsage(System.AttributeTargets.Field)]
  public class Column: System.Attribute
  {
    public int colIdx;

    public Column(int colIdx)
    {
      this.colIdx = colIdx;
    }
  }
}
