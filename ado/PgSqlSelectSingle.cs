using CodeBenchmark;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


[System.ComponentModel.Category("ADO.NET")]
public class PgSqlSelectSingle : IExample
{

    public void Initialize(Benchmark benchmark)
    {

    }

    private string mConnectionString = "Server=192.168.2.19;Database=hello_world;User Id=benchmarkdbuser;Password=benchmarkdbpass;Maximum Pool Size=256;NoResetOnClose=true;Enlist=false;Max Auto Prepare=3";

    public async Task Execute()
    {
        using (var db = Npgsql.NpgsqlFactory.Instance.CreateConnection())
        {
            db.ConnectionString = mConnectionString;
            await db.OpenAsync();

            using (var cmd = CreateReadCommand(db))
            {
                await ReadSingleRow(db, cmd);
            }
        }
    }

    async Task<World> ReadSingleRow(DbConnection connection, DbCommand cmd)
    {
        using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
        {
            await rdr.ReadAsync();

            return new World
            {
                Id = rdr.GetInt32(0),
                RandomNumber = rdr.GetInt32(1)
            };
        }
    }

    DbCommand CreateReadCommand(DbConnection connection)
    {
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT id, randomnumber FROM world WHERE id = @Id";
        var id = cmd.CreateParameter();
        id.ParameterName = "@Id";
        id.DbType = DbType.Int32;
        id.Value = new Random().Next(1, 10001);
        cmd.Parameters.Add(id);
        return cmd;
    }

    [StructLayout(LayoutKind.Sequential, Size = 8)]
    public struct World
    {
        public int Id { get; set; }

        public int RandomNumber { get; set; }
    }
}

