# Open Toontown Launcher
A .NET GUI launcher for Toontown Rewritten.

## Installing
### Windows
Simply download the Setup.msi file from the [latest release](https://github.com/chrisd149/Open-Toontown-Launcher/releases), and run the installer.

### Full Binary Installation 
You must have a recent version of Visual Studio 19 installed in order to build the project solution, as well as MSBuild installed.  You must also have CMake installed to build the project from the command line.

Example steps to build the project with Visual Studio 2019 and MSBuild:
```
mkdir build
cd build
cmake -G"Visual Studio 16 2019" .. 
msbuild /p:Configuration=Release ALL_BUILD.vcxproj
```

## Running the project
A shortcut will be created on your desktop for the program. Run the file and the launcher will start!

The launcher implements several new features on top of the UI change from the official launcher.  You can now setup a QuickLogin account, which allows you to easily store an account for the launcher to use, which can then launch the game without requiring user credentials to be entered.  Accounts are stored even after the program is shut down, so you can easily and quickly login on added accounts.  The launcher also features a live counter of the population of Toontown.

![image](https://user-images.githubusercontent.com/48182689/125847091-28632c08-a35a-4747-8601-1380082dae70.png)

*The launcher GUI*

## Contact
I can be contacted via the following methods:
* Email: `christianmigueldiaz@gmail.com`
* Discord: `chrisd149#7640`

I will try my best to respond to legitimate questions/inquires at soon as possible,however it may take me a few days at most.

## Licensing
This project uses the [MIT License](LICENSE).
