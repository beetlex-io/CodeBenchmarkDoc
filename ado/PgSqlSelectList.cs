using CodeBenchmark;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


    [System.ComponentModel.Category("ADO.NET")]
    public class PgSelectList : IExample
    {

        private string mConnectionString = "Server=192.168.2.19;Database=hello_world;User Id=benchmarkdbuser;Password=benchmarkdbpass;Maximum Pool Size=256;NoResetOnClose=true;Enlist=false;Max Auto Prepare=3";

        public void Initialize(Benchmark benchmark)
        {
        }

        public async Task Execute()
        {
            var result = new List<FortuneDTO>();

            using (var db = Npgsql.NpgsqlFactory.Instance.CreateConnection())
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT id, message FROM fortune";

                db.ConnectionString = mConnectionString;
                await db.OpenAsync();
                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await rdr.ReadAsync())
                    {
                        result.Add(new FortuneDTO
                        {
                            Id = rdr.GetInt32(0),
                            Message = rdr.GetString(1)
                        });
                    }
                }
            }
        }

        public class FortuneDTO : IComparable<FortuneDTO>, IComparable
        {
            public int Id { get; set; }

            public string Message { get; set; }

            public int CompareTo(object obj)
            {
                return CompareTo((FortuneDTO)obj);
            }

            public int CompareTo(FortuneDTO other)
            {
                return String.CompareOrdinal(Message, other.Message);
            }
        }


    }

