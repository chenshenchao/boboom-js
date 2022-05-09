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
builder.Services.AddBoboomJsService();
```

```razor
@inject BoJsPart<UserBlazor> Js

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
