

1. Requirements
```
Download MinGW v19 https://nuwen.net/mingw.html

Extract to C:/MinGW

Add C:/MinGW/bin to ENV path 
```

2. Compile icon

```windres icon.rc -o icon.o```


3. Compile app

```g++ -o bin/Csqb.exe main.cpp asset/icon.o```
