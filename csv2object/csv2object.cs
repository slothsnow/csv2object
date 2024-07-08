namespace csv2object
{
    public class csv2object
    {
        static public List<T> Convert<T>(string csv, char separator)
        {
            var table = ConvertCsvToTable(csv, separator);
            throw new NotImplementedException();
        }

        /// <summary>
        /// The method is used to convert the CSV to a table so it is easier to convert.
        /// </summary>
        /// <returns>The method returns the plain CSV file as a table. Rows are the records, columns the fields.</returns>
        static private List<List<string>> ConvertCsvToTable(string csv, char separator)
        {
            return csv.Split("\n") // Split the CSV file at the endings to convert easier
                      .Select(record => record.Split(separator).ToList()) // Split the records to get fields
                      .ToList();
        }
    }
}
