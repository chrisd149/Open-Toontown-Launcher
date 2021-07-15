# Open Toontown Launcher
A .NET GUI launcher for Toontown Rewritten.

## Installing
You must have a recent version of Visual Studio 19 installed in order to build the project solution, as well as MSBuild installed.

Example steps to build the project with Visual Studio 2019 and MSBuild:
```
mkdir build
cd build
cmake -G"Visual Studio 16 2019" .. 
msbuild /p:Configuration=Release ALL_BUILD.vcxproj
```

## Running the project
The executable for the program will be located in `build/src/Release`.  Simply run the file and the program will start! (You can also copy the file to a more convenient location or create a shortcut.)

The launcher operates very similarly to Toontown Rewritten's official launcher.  2 entry fields for username and password are present, with the ability to login using either the login button or pressing the Enter key.  The launcher also has a live counter of the population of Toontown.  The launcher can also handle 2FA and ToonGuard requests to login.

![image](https://user-images.githubusercontent.com/48182689/124153765-c6175480-da62-11eb-9a9b-3214fe56a5e5.png "The launcher")

*The launcher GUI*

## Contact
I can be contacted via the following methods:
* Email: `christianmigueldiaz@gmail.com`
* Discord: `chrisd149#7640`

I will try my best to respond to legitimate questions/inquires at soon as possible,however it may take me a few days at most.

## Licensing
This project uses the [MIT License](LICENSE.md).
