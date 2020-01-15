dotnet publish -r linux-arm /p:ShowLinkerSizeComparison=true 
pushd .\bin\Debug\netcoreapp3.1\linux-arm\publish
pscp -pw farissy -v -r .\* pi@menkeesi.space:/home/pi/Desktop/rpitest
popd