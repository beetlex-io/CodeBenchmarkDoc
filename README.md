# Code Benchmark
Unit code performance benchmark test component for netstandard 2.0
## using 
```
Install-Package BeetleX.CodeBenchmark -Version 0.6.2
```
## Tcp text example
``` csharp
[System.ComponentModel.Category("TCP")]
public class TcpTextLine : IExample
{
    public async Task Execute()
    {
        var data = $"henryfan@{DateTime.Now}";
        var stream = await mClient.ReceiveFrom(s => s.WriteLine(data));
        stream.ReadLine();

    }

    private BeetleX.Clients.AsyncTcpClient mClient;

    public void Initialize(Benchmark benchmark)
    {
        mClient = BeetleX.SocketFactory.CreateClient<BeetleX.Clients.AsyncTcpClient>("192.168.2.19", 9012);
    }

    public void Dispose()
    {
       
    }
}
```
## Websocket json example
``` csharp
[System.ComponentModel.Category("TCP")]
public class WebsocketJson : IExample
{
    public async Task Execute()
    {
        var request = new { url = "/json" };
        var result = await jsonClient.ReceiveFrom(request);
    }

    private BeetleX.Http.WebSockets.JsonClient jsonClient;

    public void Initialize(Benchmark benchmark)
    {
        jsonClient = new BeetleX.Http.WebSockets.JsonClient("ws://192.168.2.19:8080");
    }

    public void Dispose()
    {
        jsonClient.Dispose();
    }
}
```
## Http get example
``` csharp
  [System.ComponentModel.Category("TCP")]
class HttpGet : IExample
{
    public void Dispose()
    {

    }

    public async Task Execute()
    {
        var result = await _httpHandler.json();
    }

    public void Initialize(Benchmark benchmark)
    {
        if (_httpApi == null)
        {
            _httpApi = new BeetleX.Http.Clients.HttpClusterApi();
            _httpApi.DefaultNode.Add("http://192.168.2.19:8080");
            _httpHandler = _httpApi.Create<IHttpHandler>();
        }
    }

    static BeetleX.Http.Clients.HttpClusterApi _httpApi;

    static IHttpHandler _httpHandler;

    [BeetleX.Http.Clients.FormUrlFormater]
    public interface IHttpHandler
    {
        // http://host/json
        Task<string> json();
    }
}
```
## XRPC sample
``` csharp
[System.ComponentModel.Category("XRPC")]
public class XRPC_Add : IExample
{
    public void Dispose()
    {

    }

    public async Task Execute()
    {
        var result = await userService.Add("henry", "henryfan@msn.com", "guangzhou", "http://github.com");
    }

    private IUserService userService;

    public void Initialize(Benchmark benchmark)
    {
        userService = XRPCHandler.Single.UserService;
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
## Open
default url 'http://localhost:9090/'
![](https://raw.githubusercontent.com/IKende/CodeBenchmarkDoc/master/images/ui.png)
