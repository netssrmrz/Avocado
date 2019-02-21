using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Data Source = (localdb)\mssqllocaldb;Initial Catalog = UnitTests.data.AvocadoDb; Integrated Security = True; MultipleActiveResultSets=True
namespace UnitTests
{
  [TestClass]
  public class DbTests
  {
    [TestMethod]
    public async System.Threading.Tasks.Task SaveToDb()
    {
      Avocado.CsvReader csv;
      System.IO.StreamReader file;
      UnitTests.domain.Person[] persons;
      UnitTests.data.AvocadoDb db;
      System.Data.Entity.Infrastructure.DbRawSqlQuery query;
      System.Collections.Generic.List<object> people;

      file = new System.IO.StreamReader("data//sample1.csv");
      csv = new Avocado.CsvReader(file);
      persons = csv.ReadLines<UnitTests.domain.Person>();

      db = new data.AvocadoDb();
      db.Database.ExecuteSqlCommand("delete from people");
      foreach (UnitTests.domain.Person person in persons)
        db.People.Add(person);
      db.SaveChanges();

      query = db.Database.SqlQuery(typeof(UnitTests.domain.Person), "select * from people");
      people = await query.ToListAsync();

      Assert.IsNotNull(people);
      Assert.AreEqual(4, people.Count);
    }
  }
}
