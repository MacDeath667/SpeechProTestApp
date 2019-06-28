using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using SpeechProApp.Model;

namespace SpeechProApp.BL
{
    public class DbInfoExtractor
    {
        private string _connectionStringTemplate =
            "Server={0};Database={1};User Id={2};Password={3};MultipleActiveResultSets=true;";

        public ObservableCollection<Database> BuildDB(string serverName, string database, string username,
            string password)
        {
            ObservableCollection<Database> databases;
            var connectionString = string.Format(_connectionStringTemplate, serverName, database, username, password);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM sys.databases", connection);

                using (var reader = command.ExecuteReader())
                {
                    databases = GetServerDatabases(reader);
                }
            }

            foreach (var db in databases)
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.InitialCatalog = db.Name;
                connectionString = builder.ToString();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT * FROM sys.objects where type_desc = 'USER_TABLE'",
                        connection);

                    using (var reader = command.ExecuteReader())
                    {
                        var result = GetDatabaseTables(reader);
                        foreach (var table in result)
                        {
                            db.Tables.Add(table);
                        }
                    }

                    FillAllTablesWithColumnInfo(db, connection);
                }
            }

            return databases;
        }

        private ObservableCollection<Table> GetDatabaseTables(SqlDataReader reader)
        {
            var nameColumnPosition = GetColumnPosition(reader, "name");

            var tables = new ObservableCollection<Table>();

            while (reader.Read())
            {
                var tableName = reader.GetString(nameColumnPosition);
                var table = new Table
                {
                    Name = tableName
                };
                tables.Add(table);
            }

            return tables;
        }

        private ObservableCollection<Database> GetServerDatabases(SqlDataReader reader)
        {
            var nameColumnPosition = GetColumnPosition(reader, "name");

            var databases = new ObservableCollection<Database>();

            while (reader.Read())
            {
                var databaseName = reader.GetString(nameColumnPosition);
                var database = new Database
                {
                    Name = databaseName
                };
                databases.Add(database);
            }

            return databases;
        }

        private int GetColumnPosition(SqlDataReader reader, string columnName)
        {
            for (var nameColumnPosition = 0; nameColumnPosition < reader.FieldCount; nameColumnPosition++)
            {
                if (reader.GetName(nameColumnPosition).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return nameColumnPosition;
                }
            }

            throw new Exception($"Column with this name '{columnName}' was not found");
        }

        private void FillAllTablesWithColumnInfo(Database database, SqlConnection connection)
        {
            foreach (var table in database.Tables)
            {
                table.Columns = GetTableColumns(connection, table.Name);
            }
        }

        private ObservableCollection<Column> GetTableColumns(SqlConnection connection, string tableName)
        {
            var command = new SqlCommand($"SELECT* FROM {tableName} WHERE 1 = 0", connection);
            using (var reader = command.ExecuteReader())
            {
                var columns = new ObservableCollection<Column>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var column = GetColumn(reader, i);
                    columns.Add(column);
                }

                return columns;
            }
        }

        private Column GetColumn(SqlDataReader reader, int columnPosition)
        {
            var type = reader.GetFieldType(columnPosition);
            var name = reader.GetName(columnPosition);
            var sqltype = (SqlDbType) (int) reader.GetSchemaTable().Rows[columnPosition]["ProviderType"];
            var column = new Column
            {
                DotnetType = type,
                Name = name,
                SqlType = sqltype
            };
            return column;
        }
    }
}