using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avocado
{
  public class CsvReader
  {
    System.IO.TextReader dataSource;

    public CsvReader(string rawData)
    {
      this.dataSource = new System.IO.StringReader(rawData);
    }

    /*public T ReadLine<T>() where T: new()
    {
      T obj = default(T);
      string dataLine;
      string[] dataTokens;

      dataLine = this.dataSource.ReadLine();
      dataTokens = StringToTokens(dataLine);

      obj = new T();
      TokensToObj(dataTokens, obj);
      
      return obj;
    }*/

    public static string[] StringToTokens(string dataLine)
    {
      string[] res = null;

      res = dataLine.Split(',');

      return res;
    }

    /*public static void TokensToObj(string[] dataTokens, Object obj)
    {

    }*/
  }
}
