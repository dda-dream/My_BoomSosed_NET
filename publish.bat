dotnet publish My_BoomSosed_NET.csproj -o bin\publishRelease -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true

dotnet publish My_BoomSosed_NET.csproj -o bin\publishDebug   -c Debug   -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true

