mkdir build
cd build
cmake -G"Visual Studio 16 2019" -A x64 ..
msbuild /p:Configuration=Release ALL_BUILD.vcxproj
cd src
cd Release
start OpenTTLauncher.exe
pause