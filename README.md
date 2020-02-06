# NeoFS API CSharp

### Requirements
- Linux / macOS (Win not supported for now)
- DotNet SDK
- Golang 1.13+ (to fetch proto dependencies)
- protobuf / protoc

### Regenerate NeoFS Proto files and documentation
```
# DotNet Restore 
$ dotnet restore

# Regenerate proto files
$ make docgen protoc
```
