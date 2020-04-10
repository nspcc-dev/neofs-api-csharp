# NeoFS API CSharp

### Requirements
- DotNet SDK
- protobuf / protoc

### Regenerate NeoFS Proto files and documentation
```
# DotNet Restore 
$ dotnet restore

# Regenerate proto files
$ make docgen protoc
```

### Example in Docker

`docker pull nspccdev/neofs-api-csharp:example`

```
Examples for NeoFS API Library:

→ /app/cmd help
  put        put file into the container
  get        get file from the container
  help       Display more information on a specific command.
  version    Display version information.

→ /app/cmd object:get \
  --host s01.fs.nspcc.ru:8080 \
  --cid 35Zg4Mj8y998VDftVyQygFrQCPRTPvG8QK6WXhGUWPMH \
  --oid 72b94cce-a3fc-4c25-90e4-1817999fb2ad \
  -o /tmp/2.mp4

Used host: s01.fs.nspcc.ru:8080
HealthResponse = { "Healthy": true, "Status": "OK" }

Received object
PayloadLength = 24150016
Headers:
{ "UserHeader": { "Key": "plugin", "Value": "sendneofs" } }
{ "UserHeader": { "Key": "expired", "Value": "1585700042" } }
{ "UserHeader": { "Key": "filename", "Value": "/tmp/1.mp4" } }
{ "PublicKey": { "Value": "ArNiK/QBe9/jF8WK7V9MdT8ga324lgRvp9d0u8S/f43C" } }
{ "Verify": { "PublicKey": "A20QXhwN0ux708B/LOf09G9JI+6HngNjV67TrvXd11w2", "KeySignature": "BNrw+iAqMP9GBj2YcsZ0+hHK+lNd9ed2V3tTzcKomzFNsc5naeBzXPBSrcMP1c0+ztobhjZnexbioVdWW8y1TJw=" } }
{ "HomoHash": "M+shMlQldYpUT1g/H1hadVuOJcnGdQO8kvoz6nTIY6A4H7vv08H4QfZVBTC14+H+HkSpzXURAlhzLqLeJb6xPA==" }
{ "PayloadChecksum": "LUDSUA86ltTmNpsPsJXarUukxnLKnjXx9AB7918jQMI=" }
{ "Integrity": { "HeadersChecksum": "O9BxaOqS2h5mC99d+Sj4pvKquKjB6URPOOIGALDbyhk=", "ChecksumSignature": "BA/Ou75HR/56Aey45xd6UNfviwHgEiKtZ/ss0m6VxeWaGCetsLRQUzP4J9axwIIKX6ynopodbq5ben8ctuW7Tb8=" } }

Received chunks: Done!
Close file

→ /app/cmd object:put \
  --host s01.fs.nspcc.ru:8080 \
  --cid 35Zg4Mj8y998VDftVyQygFrQCPRTPvG8QK6WXhGUWPMH \
  -i /tmp/1.mp4

Used host: s01.fs.nspcc.ru:8080
HealthResponse = { "Healthy": true, "Status": "OK" }

Write chunks: Done!

Object stored:
URL: https://cdn.fs.neo.org/35Zg4Mj8y998VDftVyQygFrQCPRTPvG8QK6WXhGUWPMH/72b94cce-a3fc-4c25-90e4-1817999fb2ad
CID: 35Zg4Mj8y998VDftVyQygFrQCPRTPvG8QK6WXhGUWPMH
OID: 72b94cce-a3fc-4c25-90e4-1817999fb2ad

Close file.
```

## License

This project is licensed under the Apache 2.0 License - 
see the [LICENSE](LICENSE) file for details
