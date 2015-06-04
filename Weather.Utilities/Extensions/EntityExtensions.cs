using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Weather.Utilities.Extensions
{
    public static class EntityExtensions
    {
        public static int ExecuteSqlCommandSmart(this Database database, string storedProcedure, object parameters = null)
        {
            if (database == null)
                throw new ArgumentNullException("database");
            if (string.IsNullOrEmpty(storedProcedure))
                throw new ArgumentException("storedProcedure");

            var arguments = PrepareArguments(storedProcedure, parameters);
            return database.ExecuteSqlCommand(arguments.Item1, arguments.Item2);
        }

        private static Tuple<string, object[]> PrepareArguments(string storedProcedure, object parameters)
        {
            var parameterNames = new List<string>();
            var parameterParameters = new List<object>();

            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties().Where(p => p.PropertyType.IsSqlCompatible()).ToList())
                {
                    var name = "@" + prop.Name;
                    var value = prop.GetValue(parameters, null);

                    parameterNames.Add(name);
                    parameterParameters.Add(new SqlParameter(name, value ?? DBNull.Value));
                }
            }

            if (parameterNames.Count > 0)
                storedProcedure += " " + string.Join(", ", parameterNames);

            return new Tuple<string, object[]>(storedProcedure, parameterParameters.ToArray());
        }
    }
}
