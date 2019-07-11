using System.Data;
// folder
namespace TimeSheet.DAL.Repositories.Repository.Implementation
{
    public static class IDbConnectionExtensions
    {
        public static IDbCommand CreateCommand(this IDbConnection connection, string commandText)
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }

        public static IDbCommand AddCommand(this IDbCommand command, string commandText)
        {
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;
            return command;
        }

        public static IDbDataParameter CreateParameter<T>(this IDbCommand command, string name, T value)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}
