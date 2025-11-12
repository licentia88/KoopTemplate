KoopTemplate gRPC

This package contains a dotnet new template for creating a gRPC project based on KoopTemplate. To install locally, run:

    dotnet new --install <path-to-nupkg>

Or from a nupkg published to NuGet:

    dotnet new --install KoopTemplate.Grpc.Template::1.0.0

Once installed, create a project with:

    dotnet new koop-grpc -n MyProject
{
  "$schema": "http://json.schemastore.org/template",
  "author": "Koop",
  "classifications": [ "Web", "gRPC", "Template" ],
  "identity": "KoopTemplate.Grpc.CSharp.Template",
  "name": "KoopTemplate gRPC Project",
  "shortName": "koop-grpc",
  "tags": {
    "language": "C#",
    "type": "project"
  },
  "sourceName": "KoopTemplate.Grpc",
  "preferNameDirectory": true
}

