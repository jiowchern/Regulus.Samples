 dotnet run --project .\..\Regulus\Regulus.Application.Protocol.CodeWriter\Regulus.Application.Protocol.CodeWriter.csproj --  --common .\..\Chat1\Regulus.Samples.Chat1.Common\bin\Debug\netstandard2.0\Regulus.Samples.Chat1.Common.dll --output .\..\Chat1\Regulus.Samples.Chat1.Protocol\

 dotnet run --project ./../Regulus/Regulus.Application.Protocol.CodeWriter  --common $(TargetPath) --output $(SolutionDir)\Helloworld\Regulus.Samples.Helloworld.Protocol
 

 pause