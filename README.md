


## Introduction

Cube is a tunneling client to access <i>Computer Science Student Society CS3</i> servers called CSQB securely and at minimum cost.

Using the client allows for encrypted services such as:
- Custom DNS
- Minecraft Server
- Website
- and many more!

For more information, visit [Zrok](https://zrok.io/)

<br>

![image](https://github.com/user-attachments/assets/f3e69f67-ecc2-4b45-8284-45e584fa98a7)

<br>

## Development

1. Install [Scoop](https://scoop.sh/) (windows package manager)

2. Install C# Software Development Kit (SDK)

```
scoop install dotnet-sdk
```

3. Create a new dll

```
dotnet new classlib -o <folder/namespace>
```

4. Create a new exe

```
dotnet new console -o <folder/namespace>
```

5. Build a single src folder for development

```
dotnet build
```

## Production

1. Publish every src folder for production 

```
dotnet publish
```

2. Combine all publish and tools folder to `out` folder

3. Delete *.pdb files (It exposes stack trace and can be reverse engineered)

4. Rename *.exe file to Cube.exe

6. Create installer using cracked AdvanceInstaller software then open tools/CubeSetup.aip  

6. Create a custom action, click 'installed' HostsFileOperations.dll and run IncludeServerDomain function on install and maintenance.

7. Create a custom action, click 'installed' HostsFileOperations.dll and run ExcludeServerDomain function on uninstall.

## Future Works

1. Public Domain

![image](https://github.com/user-attachments/assets/6aed4b0c-706b-49ed-8f76-f08214daba2c)
