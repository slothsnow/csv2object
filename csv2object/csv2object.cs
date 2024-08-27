using System.Reflection;
using System.Linq;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace csv2object
{
    public class csv2object
    {
        static public List<T> Convert<T> (string csv, char separator) where T : new()
        {
            var table = ConvertCsvToTable(csv, separator);
            var objectFields = GetObjectFields(typeof(T));
            var columnsToObjectFieldName = ConvertColumnsToObjectFieldName(table, objectFields);
            return fillObjects<T>(table, columnsToObjectFieldName);
        }

        /// <summary>
        /// The method is used to convert the CSV to a table so it is easier to convert.
        /// </summary>
        /// <returns>The method returns the plain CSV file as a table. Rows are the records, columns the fields.</returns>
        static private List<List<string>> ConvertCsvToTable(string csv, char separator)
        {
            return csv.Split('\n') // Split the CSV file at the endings to convert easier
                      .Select(record => record.Split(separator).ToList()) // Split the records to get fields
                      .ToList();
        }

        /// <summary>
        /// The method is used to get the names of the fields of the objects
        /// </summary>
        /// <param name="type">The type of the objects in which the CSV should be converted</param>
        /// <returns></returns>
        private static List<string> GetObjectFields(Type type)
        {
            var fieldNamesTemp = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Select(field => field.Name).ToList();
            List<string> result = new List<string>();
            foreach (var field in fieldNamesTemp)
            {
                result.Add(field.Replace("k__BackingField", "").Replace("<", "").Replace(">", ""));
            }
            return result;
        }


        /// <summary>
        /// The method converts the number of columns to the field names of the object. 
        /// </summary>
        /// <param name="table">The CSV as table.</param>
        /// <param name="objectFields">The object field names.</param>
        /// <returns>A dictionary. Column number -> Object File Name</returns>
        static private Dictionary<int, string> ConvertColumnsToObjectFieldName(List<List<string>> table, List<string> objectFields)
        {
            Dictionary<int, string> columnsToObjectFieldName = new();
            for (int i = 0; i < table[0].Count; i++)
            {
                string nameInCSV = table[0][i].ToLower();
                foreach (string objectField in objectFields)
                {
                    if (nameInCSV == objectField.ToLower())
                    {
                        columnsToObjectFieldName.Add(i, objectField);
                        break;
                    }
                }
            }
            return columnsToObjectFieldName;
        }

        /// <summary>
        /// The method sets the value of a field of an object without knowing the type.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        static private T SetFieldValue<T>(T obj, string fieldName, object value)
        {
            var property = obj.GetType().GetProperty(fieldName);
            if (property != null && property.PropertyType.IsAssignableFrom(value.GetType()))
            {
                property.SetValue(obj, value);
            }
            return obj;
        }

        /// <summary>
        /// The method fills a list with objects with a table and the "columnsToObjectFieldName".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="columnsToObjectFieldName"></param>
        /// <returns></returns>
        static private List<T> fillObjects<T>(List<List<string>> table, Dictionary<int, string> columnsToObjectFieldName) where T : new()
        {
            List<T> result = new List<T>();
            for (int i = 1; i < table.Count; i++)
            {
                T obj = new T();
                for (int j = 0; j < table[i].Count; j++)
                {
                    obj = SetFieldValue(obj, columnsToObjectFieldName[j], table[i][j]);
                }
                result.Add(obj);
            }
            return result;
        }
    }
}
