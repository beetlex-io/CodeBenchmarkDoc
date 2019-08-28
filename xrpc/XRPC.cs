using BeetleX.XRPC.Clients;
using CodeBenchmark;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Install-Package BeetleX.XRPC.Clients -Version 0.6.0
public class XRPCHandler
{

    public XRPCHandler()
    {
        Client = new XRPCClient("192.168.2.19", 9013,3);
        Client.Connect();
        UserService = Client.Create<IUserService>();
    }

    public XRPCClient Client { get; private set; }

    public IUserService UserService { get; private set; }

    private static XRPCHandler mSingle;

    public static XRPCHandler Single
    {
        get
        {
            if (mSingle == null)
                mSingle = new XRPCHandler();
            return mSingle;
        }
    }
}

public interface IUserService
{
    Task<bool> Login(string name, string pwd);

    Task<User> Add(string name, string email, string city, string remark);

    Task Save();

    Task<User> Modify(User user);

    Task<List<User>> List(int count);
}

[MessagePackObject]
public class User
{
    [Key(4)]
    public string ID { get; set; }

    [Key(0)]
    public string Name { get; set; }

    [Key(1)]
    public string City { get; set; }

    [Key(2)]
    public string EMail { get; set; }

    [Key(3)]
    public string Remark { get; set; }
}

[System.ComponentModel.Category("XRPC")]
public class XRPC_Login : IExample
{
    public void Dispose()
    {
        
    }

    public async Task Execute()
    {
        var result = await userService.Login("admin", "123456");
    }

    private IUserService userService;

    public void Initialize(Benchmark benchmark)
    {
        userService = XRPCHandler.Single.UserService;
    }
}

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


[System.ComponentModel.Category("XRPC")]
public class XRPC_List : IExample
{
    public void Dispose()
    {

    }

    public async Task Execute()
    {
        var result = await userService.List(5);
    }

    private IUserService userService;

    public void Initialize(Benchmark benchmark)
    {
        userService = XRPCHandler.Single.UserService;
    }
}


