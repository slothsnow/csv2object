using System.Runtime.InteropServices;
using System.Reflection;
using System.Linq;

namespace csv2object
{
    public class object2csv
    {
        static public string Convert<T>(List<T> objects, [Optional] char separator)
        {
            if (objects is null || objects.Count == 0) return "";
            else
            {
                // Table to convert easier
                List<List<string>> csv = new List<List<string>>();

                // Get structure of object and put it in the table
                List<string> names = new List<string>(from field in objects[0]!.GetType().GetProperties()
                                                      select field.Name);

                // Copy objects in table
                foreach (T obj in objects)
                {
                    List<string> temp = new List<string>();
                    foreach (var field in obj.GetType().GetProperties())
                    {
                       temp.Add(field.ToString());
                    }
                }

            }
            throw new NotImplementedException();
        }
    }
}