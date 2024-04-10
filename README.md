# ASP.NET Gost cryptography API

Web API for working with Russian crypto algorithms as a console application or a windows service using TopShelf.

### How To: building and hosting Web API as a windows service (OWIN)

**Step 1: Edit App.config and TopShelf\\ConfigureSevice.cs**
- Section appsettings in App.config:
![test api](https://i.imgur.com/n7T3lfS.jpeg)

BaseUrl value - url, that you want bind your Web API;

LogFilePath value - path to text file for logs.

- Change username and password in TopShelf\\ConfigureService.cs:
``` csharp
x.RunAs(username: @"username", password: "password");
```

**Step 2: Build solution**

**Step 3: Run 'cmd' as Administrator, go to bin\\Debug and run**
> GostCryptographyAPI.exe install

**Step 4: Start your windows service**
> GostCryptographyAPI.exe start

Now the web api is listening http://localhost:5000/

### How To: testing Web API

**Step 1: Open test project**

**Step 2: Open Test Explorer**

**Step 3: Run tests**

### How To: using Client library

**Step 1: In your project add reference to GostCryptography.Client.dll**

**Step 2: Create new instance of IGostCryptographyClient**
``` csharp
IGostCryptographyClient _client;

_client = new GostCryptographyClient(
    new GostCryptographyOptions("http://localhost:5000/api/"));
```

**Step 3: Using methods**
``` csharp
var unsignedData = await _client.VerifySignCMS(
    message: File.ReadAllBytes("signed_message.xml.p7s"));

using (var fs = new FileStream(
    path: "C:\\...\unsigned_message.xml",
    FileMode.Create);
    await fs.WriteAsync(unsignedData);
```

