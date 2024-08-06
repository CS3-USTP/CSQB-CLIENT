
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

1. Requirements
```
Download MinGW v19 https://nuwen.net/mingw.html

Extract to C:/MinGW

Add C:/MinGW/bin to ENV path 
```

2. Compile icon

```
windres icon.rc -o icon.o
```


3. Compile app

```
g++ -o bin/Csqb.exe main.cpp asset/icon.o
```


## Future Works

1. Public Domain

![image](https://github.com/user-attachments/assets/6aed4b0c-706b-49ed-8f76-f08214daba2c)
