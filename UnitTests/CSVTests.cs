using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
  [TestClass]
  public class CSVTests
  {
    public class PlainObj1
    {
      [Avocado.Csv.Column(1)]
      public string name;

      [Avocado.Csv.Column(2)]
      public int age;

      [Avocado.Csv.Column(3)]
      public float height;

      [Avocado.Csv.Column(4)]
      public System.DateTime dob;
    }

    /*[TestMethod]
    public void Read_Single_Plain_Obj()
    {
      Avocado.CsvReader csv;
      PlainObj1 obj;

      csv = new Avocado.CsvReader("Roger Ramjet, 47, 170.12, 13/11/1971");
      obj = csv.ReadLine<PlainObj1>();

      Assert.AreEqual("Roger Ramjet", obj.name);
      Assert.AreEqual(47, obj.age);
      Assert.AreEqual(170.12, obj.height);
      Assert.AreEqual(new System.DateTime(1971, 11, 13), obj.dob);
    }*/

    [TestMethod]
    public void String_To_Token()
    {
      string[] tokens;

      tokens = Avocado.CsvReader.StringToTokens("Roger Ramjet, 47, 170.12, 13/11/1971");
      Assert.AreEqual("Roger Ramjet", tokens[0]);
      Assert.AreEqual("47", tokens[1]);
      Assert.AreEqual("170.12", tokens[2]);
      Assert.AreEqual("13/11/1971", tokens[3]);
    }
  }
}
