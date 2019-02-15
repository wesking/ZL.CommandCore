ZL.CommandCore�ǻ���dotnet core��ּ��Ϊǰ���ṩͳһ�Ľӿڽ���
## �����Ŀ����

```
Install-Package ZL.CommandCore
```

## ��ʼ��ZL.CommandCore

```
public void ConfigureServices(IServiceCollection services)
{
	...

    services.AddCommand(opt => {
		opt.ServiceKey = "DemoWeb1";
    });

    //
    //��̬��ӵ�ǰ��Ŀ���нӿڵ�����ע��
    //
    var assembly = typeof(Startup).Assembly;

    //ʵ���˽ӿ�ICommandBase�ӿڵ��������ע��
    var types = assembly.ExportedTypes.Where(x => x.IsClass && x.IsPublic && x.GetInterface("ICommandBase") != null);

    foreach (var type in types)
    {
        services.AddScoped(type, type);
    }
	...
}
``` 

## �ӿڴ���

```
//����ӿڴ������
public class Test1Parameter : IParameter
{
	//�ڴ˶�������
}

//����ӿڷ��ؽ��
public class Test1Result: Result<string>
{
}

//����ӿ�
public class Test1Command : Command<string>
{
	//�ӿڵĴ����߼�
    protected override IResult<string> OnExecute(IParameter parameter)
    {
        Test1Parameter test1Parameter = parameter as Test1Parameter;

        Result<string> result = new Result<string>();

        return result;
    }
}
```

## �ڿ������������ӿ�

```
[Route("api/test/[action]")]//�����ӿڵĵ��õ�ַ
[ApiController]
public class TestController : ControllerBase
{
    public TestController(IServiceProvider provider)
    {
        _p = provider;
    }

    /// <summary>
    /// �ӿڵ�������1
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    [HttpPost]
    public Test1Result Test1(Test1Parameter parameter)
    {
        return _p.GetService<Test1Command>().Execute(parameter) as Test1Result;
    }
    private IServiceProvider _p;
}
```

## �ӿڻ���
ʹ�ýӿڻ�����Ҫ��ʼ����ʱ��ָ�����ݿ����Ӵ�
```
services.AddCommand(opt => {
	//���ݿ����ӵ�ַ
    opt.ConnectionString = "server=localhost;userid=root;password=123456;database=zl_command;";
    opt.ServiceKey = "DemoWeb1";
});
```
ʹ��CacheAttributeָ������ʹ�û���
```
[HttpPost, Cache(Key = "test1")]
public Test1Result Test1(Test1Parameter parameter)
{
	return _p.GetService<Test1Command>().Execute(parameter) as Test1Result;
}
```

## �ӿ���Ȩ
������Ҫ�̳�AuthorizationParameter���ӿ���Ҫ�̳�AuthorizationCommand

```
public class Test3Parameter : AuthorizationParameter
{
}

public class Test3Result : Result<string>
{
}
public class Test3Command : AuthorizationCommand<string>
{

    protected override IResult<string> OnExecute(IParameter parameter)
    {
        Test3Parameter test2Parameter = parameter as Test3Parameter;

        Test3Result result = new Test3Result() { };
        //�ڴ�ʵ����ص�ҵ���߼�

        return result;
    }
}
```
�ڿ�������ʹ��AuthorizationAttribute����

```
/// <summary>
/// �ӿڵ�������3������Ȩ���ƣ�
/// </summary>
/// <param name="parameter"></param>
/// <returns></returns>
[HttpPost, Authorization]
public Test3Result Test3(Test3Parameter parameter)
{
    return _p.GetService<Test3Command>().Execute(parameter) as Test3Result;
}
```
