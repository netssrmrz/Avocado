using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Avocado
{
  public class CsvReader
  {
    System.IO.TextReader dataSource;
    public bool hasTitleRow;

    public CsvReader(string rawData)
    {
      this.dataSource = new System.IO.StringReader(rawData);
    }

    public CsvReader(System.IO.StreamReader file)
    {
      this.dataSource = file;
    }

    public T ReadLine<T>() where T: new()
    {
      T obj = default(T);
      string dataLine;
      string[] dataTokens;

      dataLine = this.dataSource.ReadLine();
      if (dataLine != null)
      {
        dataTokens = StringToTokens(dataLine);

        obj = new T();
        TokensToObj(dataTokens, obj);
      }
      return obj;
    }

    public T[] ReadLines<T>() where T : new()
    {
      System.Collections.Generic.List<T> objs;
      T obj;
      T[] res = null;

      objs = new System.Collections.Generic.List<T>();
      if (this.hasTitleRow)
        this.dataSource.ReadLine();
      do
      {
        obj = this.ReadLine<T>();
        if (obj != null)
          objs.Add(obj);
      }
      while (this.dataSource.Peek()!=-1);

      if (objs.Count > 0)
        res = objs.ToArray();

      return res;
    }

    public static string[] StringToTokens(string dataLine)
    {
      string[] tokens = null;
      int c;

      if (!string.IsNullOrEmpty(dataLine))
      {
        tokens = dataLine.Split(',');
        for (c=0; c<tokens.Length; c++)
          tokens[c] = tokens[c].Trim();
      }

      return tokens;
    }

    public static void TokensToObj(string[] tokens, Object obj)
    {
      int c;
      string token;
      System.Reflection.MemberInfo field;

      for (c=0; c< tokens.Length; c++)
      {
        token = tokens[c];
        field = GetMemberWithColumnIdx(c, obj);
        SetFieldValue(obj, field, token);
      }
    }

    public static System.Reflection.MemberInfo GetMemberWithColumnIdx(int colIdx, Object obj)
    {
      System.Reflection.MemberInfo res = null;
      System.Reflection.MemberInfo[] fields;

      fields = obj.GetType().GetFields();
      res = GetMemberWithColumnIdx(colIdx, fields);

      if (res == null)
      {
        fields = obj.GetType().GetProperties();
        res = GetMemberWithColumnIdx(colIdx, fields);
      }

      return res;
    }

    public static System.Reflection.MemberInfo GetMemberWithColumnIdx(int colIdx, System.Reflection.MemberInfo[] fields)
    {
      System.Reflection.MemberInfo res = null;
      int c;
      int? fieldColIdx;

      for (c = 0; c < fields.Length; c++)
      {
        fieldColIdx = GetFieldColumnIdx(fields[c]);
        if (fieldColIdx != null && fieldColIdx == colIdx)
          res = fields[c];
      }

      return res;
    }

    public static int? GetFieldColumnIdx(System.Reflection.MemberInfo field)
    {
      int? res = null;
      object[] attrs;

      attrs = field.GetCustomAttributes(typeof(Avocado.Csv.Column), false);
      if (attrs!=null && attrs.Length>0)
        res = ((Avocado.Csv.Column)attrs[0]).colIdx;

      return res;
    }

    public static void SetFieldValue(object obj, object field, string value)
    {
      object fieldValue=null;
      Type fieldType=null;

      if (field != null)
      {
        if (field is System.Reflection.FieldInfo)
          fieldType = ((System.Reflection.FieldInfo)field).FieldType;
        else if (field is System.Reflection.PropertyInfo)
          fieldType = ((System.Reflection.PropertyInfo)field).PropertyType;

        if (fieldType == typeof(int))
          fieldValue = ToInt(value);
        else if (fieldType == typeof(float))
          fieldValue = ToFloat(value);
        else if (fieldType == typeof(string))
          fieldValue = value;
        else if (fieldType == typeof(System.DateTime))
          fieldValue = ToDateTime(value);

        if (field is System.Reflection.FieldInfo)
          ((System.Reflection.FieldInfo)field).SetValue(obj, fieldValue);
        else if (field is System.Reflection.PropertyInfo)
          ((System.Reflection.PropertyInfo)field).SetValue(obj, fieldValue);
      }
    }

    public static System.DateTime? ToDateTime(string strValue)
    {
      System.DateTime parseValue;
      System.DateTime? res = null;

      if (System.DateTime.TryParse(strValue, out parseValue))
        res = parseValue;

      return res;
    }

    public static float? ToFloat(string strValue)
    {
      float parseValue;
      float? res = null;

      if (System.Single.TryParse(strValue, out parseValue))
        res = parseValue;

      return res;
    }

    public static int? ToInt(string strValue)
    {
      int parseValue;
      int? res = null;

      if (System.Int32.TryParse(strValue, out parseValue))
        res = parseValue;

      return res;
    }
  }
}
