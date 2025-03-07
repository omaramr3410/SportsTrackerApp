using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportsTrackerApp.Constants;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace SportsTrackerApp.Helpers
{
    public static class DatabaseHelpers
    {
        public static SqlMapper.ICustomQueryParameter AsTVP(this int[] ids)
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));

            foreach (var id in ids) table.Rows.Add(id);

            var tvp = table.AsTableValuedParameter(DatabaseTypes.tpGenericIntType);

            return tvp;
        }

        public static SqlMapper.ICustomQueryParameter AsTVP<T>(this IEnumerable<T> values, string type)
        {
            var dataTable = new DataTable();

            // Get the properties of the model (T)
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Dynamically add columns to DataTable based on the properties of the model
            foreach (var prop in properties)
            {
                var columnName = prop.Name;
                dataTable.Columns.Add(columnName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Add rows to the DataTable for each object in the list
            foreach (var item in values)
            {
                var row = dataTable.NewRow();
                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }

            var tvp = dataTable.AsTableValuedParameter(type);

            return tvp;
        }
    }
}
