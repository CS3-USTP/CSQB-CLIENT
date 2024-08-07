
Open the `CSQB/bin` folder then run `Csqb.exe`


## Introduction

CSQB is an abstracted tunneling client to access <i>Computer Science Student Society CS3</i> servers securely at minimum cost.

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

2. Install [Build Tools For Visual Studio](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2022)

3. Install MSVC and C++ CMake Tools for Windows

![image](https://github.com/user-attachments/assets/43be30a7-32ce-48d2-a4f3-4f9e4c79a08d)
  
4. Install ming (g++ compiler) cmake (c++ builder) and vcpkg (c++ package manager)  

```
scoop install mingw cmake vcpkg 
```

5. Compile icon
   
```
windres icon.rc -o icon.o
```

6. Compile app

```
g++ -o bin/Csqb.exe src/main.cpp asset/icon.o
```

## Future Works

1. Public Domain

![image](https://github.com/user-attachments/assets/6aed4b0c-706b-49ed-8f76-f08214daba2c)
