# Code Benchmark
Unit code performance benchmark test component for netstandard 2.0
## using 
```
Install-Package CodeBenchmark -Version 0.5.0
```
## New example
``` csharp
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
```
## Runing
``` csharp
    class Program
    {
        static void Main(string[] args)
        {

            Benchmark benchmark = new Benchmark();
            benchmark.Register(typeof(Program).Assembly);
            benchmark.Start();
            benchmark.OpenWeb();
            Console.Read();
        }
    }
```
![](https://raw.githubusercontent.com/IKende/CodeBenchmarkDoc/master/images/ui.png)
