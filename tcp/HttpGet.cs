using CodeBenchmark;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Install-Package BeetleX.Http.Clients -Version 0.8.8
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

