# [Boboom](https://github.com/chenshenchao/boboom-js)

## 安装

```bash
# dotnet
dotnet add package Boboom.Js
# Package Manager
Install-Package Boboom.Js
```

## 使用

```csharp
// 引入服务
builder.Services.AddBoboomJsService();
```

通过 Inject 使用，C# 或者 razor 任选一种。

```csharp
public partial class YourBlazorComponent
{
	[Inject]
	public BoJsPart<YourBlazorComponent> Js { get; set; } = null!;
}
```

```razor
@inject BoJsPart<YourBlazorComponent> Js

@code {

}
```

### 存储（LocalStorage 和 SessionStorage)

```csharp
builder.Services.AddBoboomJsService()
	.AddBoboomJsStorageService();
```

```razor
@inject BoJsStorage Storage

@code {

}
```
