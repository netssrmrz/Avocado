# Avocado Test

Requirements  
- tba

Assumptions
- Only CSV read functionality required, not write.
- No Web developemt mentioned.
- Didn't understand "Each type of CSV will be defined as dataset. This dataset categories are mapped with CSV Columns and Entity attributes". A sample interface would've helped.
- No mention of whether to map columns by index or name so assumed by index.

Implemented Features
- Can instantiate objects from CSV file or string
- Can indicate if header row present
- Supports string, int, float, and datetime columns
- Can instantiate objects of any required type
- Supports both object field and property mappings

Usage From CSV String
```
  csv = new Avocado.CsvReader(stringData);
  objs = csv.ReadLines<ObjType>();
```

Usage From CSV File
```
  file = new System.IO.StreamReader(filePath);
  csv = new Avocado.CsvReader(file);
  objs = csv.ReadLines<ObjType>();
```

To Indicate Object Field Mappings
  Use the Avocado.Csv.Column attribute to indicate which CSV column will populate a certain object field or property. 
  Column indexes are zero based. Example,
```
  public class SomeObj
  {
    [Avocado.Csv.Column(3)] // indicates that the fourth CSV column will populate this field
    public System.DateTime dob;

    public string name; // not mapped to any CSV column

    [Avocado.Csv.Column(1)] // indicates that the second CSV column will populate this property
    public int age {get; set; }
  }
```

To Do
- Allow for column mappings by name as well as number
- Allow for different delimeter characters
- Allow for additional column data types
- Allow for mappings via parameters or config settings in case attributes cannot be used

Development Environment
- Microsoft Visual Studio Community 2017, Version 15.9.6
- Entity Framework 6
- .NET Framework 4.6.1
- MS SQL Server Local Db
