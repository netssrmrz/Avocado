using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
  [TestClass]
  public class CSVTests
  {
    public class PlainObj1
    {
      [Avocado.Csv.Column(0)]
      public string name;

      [Avocado.Csv.Column(1)]
      public int age;

      [Avocado.Csv.Column(2)]
      public float height;

      [Avocado.Csv.Column(3)]
      public System.DateTime dob;
    }

    public class PlainObj2
    {
      [Avocado.Csv.Column(0)]
      public string policyID;

      [Avocado.Csv.Column(3)]
      public float eq_site_limit;    }

    [TestMethod]
    public void StringToTokens_Basic()
    {
      string[] tokens;

      tokens = Avocado.CsvReader.StringToTokens("Roger Ramjet, 47, 170.12, 13/11/1971");
      Assert.AreEqual("Roger Ramjet", tokens[0]);
      Assert.AreEqual("47", tokens[1]);
      Assert.AreEqual("170.12", tokens[2]);
      Assert.AreEqual("13/11/1971", tokens[3]);
    }

    [TestMethod]
    public void GetFieldColumnIdx_Basic()
    {
      PlainObj1 obj;
      System.Reflection.FieldInfo field;
      int? colIdx;

      obj = new PlainObj1();
      obj.name = "Roger Ramjet";
      obj.age = 47;
      obj.height = 170.12f;
      obj.dob = new System.DateTime(1971, 11, 13);

      field = obj.GetType().GetField("age");
      colIdx = Avocado.CsvReader.GetFieldColumnIdx(field);

      Assert.AreEqual(1, colIdx);
    }

    [TestMethod]
    public void GetFieldWithColumnIdx_Basic()
    {
      PlainObj1 obj;
      System.Reflection.FieldInfo field;

      obj = new PlainObj1();
      field = Avocado.CsvReader.GetFieldWithColumnIdx(1, obj);

      Assert.AreEqual("age", field.Name);
    }

    [TestMethod]
    public void ToInt_Basic()
    {
      int? res;

      res = Avocado.CsvReader.ToInt(null);
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToInt("");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToInt("fred");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToInt("1.11");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToInt("1");
      Assert.AreEqual(1, res);
    }

    [TestMethod]
    public void ToFloat_Basic()
    {
      float? res;

      res = Avocado.CsvReader.ToFloat(null);
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToFloat("");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToFloat("fred");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToFloat("1");
      Assert.AreEqual(1f, res);

      res = Avocado.CsvReader.ToFloat("1.11");
      Assert.AreEqual(1.11f, res);
    }

    [TestMethod]
    public void ToDateTime_Basic()
    {
      System.DateTime? res;

      res = Avocado.CsvReader.ToDateTime(null);
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToDateTime("");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToDateTime("fred");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToDateTime("1");
      Assert.IsNull(res);

      res = Avocado.CsvReader.ToDateTime("13/11/1971");
      Assert.AreEqual(new System.DateTime(1971, 11, 13), res);
    }

    [TestMethod]
    public void SetFieldValue_Basic()
    {
      PlainObj1 obj;
      System.Reflection.FieldInfo field;

      obj = new PlainObj1();

      field = obj.GetType().GetField("name");
      Avocado.CsvReader.SetFieldValue(obj, field, "Roger Ramjet");
      Assert.AreEqual("Roger Ramjet", obj.name);

      field = obj.GetType().GetField("age");
      Avocado.CsvReader.SetFieldValue(obj, field, "47");
      Assert.AreEqual(47, obj.age);

      field = obj.GetType().GetField("height");
      Avocado.CsvReader.SetFieldValue(obj, field, "170.12");
      Assert.AreEqual(170.12f, obj.height);

      field = obj.GetType().GetField("dob");
      Avocado.CsvReader.SetFieldValue(obj, field, "13/11/1971");
      Assert.AreEqual(new System.DateTime(1971, 11, 13), obj.dob);
    }

    [TestMethod]
    public void TokensToObj_Basic()
    {
      string[] tokens = { "Roger Ramjet", "47", "170.11", "13/11/1971" };
      PlainObj1 obj;

      obj = new PlainObj1();
      Avocado.CsvReader.TokensToObj(tokens, obj);

      Assert.AreEqual("Roger Ramjet", obj.name);
      Assert.AreEqual(47, obj.age);
      Assert.AreEqual(170.11f, obj.height);
      Assert.AreEqual(new System.DateTime(1971, 11, 13), obj.dob);
    }

    [TestMethod]
    public void ReadLine_Basic()
    {
      Avocado.CsvReader csv;
      PlainObj1 obj;

      csv = new Avocado.CsvReader("Roger Ramjet, 47, 170.12, 13/11/1971");
      obj = csv.ReadLine<PlainObj1>();

      Assert.AreEqual("Roger Ramjet", obj.name);
      Assert.AreEqual(47, obj.age);
      Assert.AreEqual(170.12f, obj.height);
      Assert.AreEqual(new System.DateTime(1971, 11, 13), obj.dob);
    }

    [TestMethod]
    public void ReadLines_Basic()
    {
      Avocado.CsvReader csv;
      PlainObj1[] objs;
      string data;

      data =
        "Roger Ramjet, 47, 170.11, 13/11/1971\n" +
        "Yank, 27, 160.22, 1/1/1991\n" +
        "Doodle, 26, 150.33, 2/2/1992\n" +
        "Dan, 25, 140.44, 3/3/1993";
      csv = new Avocado.CsvReader(data);
      objs = csv.ReadLines<PlainObj1>();

      Assert.IsNotNull(objs);
      Assert.AreEqual(4, objs.Length);

      Assert.AreEqual("Roger Ramjet", objs[0].name);
      Assert.AreEqual(47, objs[0].age);
      Assert.AreEqual(170.11f, objs[0].height);
      Assert.AreEqual(new System.DateTime(1971, 11, 13), objs[0].dob);

      Assert.AreEqual("Yank", objs[1].name);
      Assert.AreEqual(27, objs[1].age);
      Assert.AreEqual(160.22f, objs[1].height);
      Assert.AreEqual(new System.DateTime(1991, 1, 1), objs[1].dob);
    }

    [TestMethod]
    public void ReadLines_FromFile()
    {
      Avocado.CsvReader csv;
      PlainObj1[] objs;
      System.IO.StreamReader file;

      file = new System.IO.StreamReader("data\\sample1.csv");
      csv = new Avocado.CsvReader(file);
      objs = csv.ReadLines<PlainObj1>();

      Assert.IsNotNull(objs);
      Assert.AreEqual(4, objs.Length);

      Assert.AreEqual("Roger Ramjet", objs[0].name);
      Assert.AreEqual(47, objs[0].age);
      Assert.AreEqual(170.11f, objs[0].height);
      Assert.AreEqual(new System.DateTime(1971, 11, 13), objs[0].dob);

      Assert.AreEqual("Yank", objs[1].name);
      Assert.AreEqual(27, objs[1].age);
      Assert.AreEqual(160.22f, objs[1].height);
      Assert.AreEqual(new System.DateTime(1991, 1, 1), objs[1].dob);
    }

    [TestMethod]
    public void ReadLines_FromFileLarge()
    {
      Avocado.CsvReader csv;
      PlainObj2[] objs;
      System.IO.StreamReader file;

      file = new System.IO.StreamReader("data\\sample2.csv");
      csv = new Avocado.CsvReader(file);
      csv.hasTitleRow = true;
      objs = csv.ReadLines<PlainObj2>();

      Assert.IsNotNull(objs);
      Assert.AreEqual(36634, objs.Length);

      Assert.AreEqual("119736", objs[0].policyID);
      Assert.AreEqual(498960f, objs[0].eq_site_limit);

      Assert.AreEqual("398149", objs[36633].policyID);
      Assert.AreEqual(373488.3f, objs[36633].eq_site_limit);
    }
  }
}
