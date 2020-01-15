dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
pushd .\bin\Debug\netcoreapp3.1\linux-arm\publish
pscp -pw farissy -v -r .\* pi@192.168.0.99:/home/pi/Desktop/rpitest
popd