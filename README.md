ervices:
  - type: web
    name: my-aspnet-app
    runtime: dotnet
    buildCommand: "dotnet publish -c Release -o publish"
    startCommand: "dotnet publish/DoctorTrain.dll"
