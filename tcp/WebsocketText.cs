using CodeBenchmark;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Install-Package BeetleX.Http.Clients -Version 0.8.8
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

