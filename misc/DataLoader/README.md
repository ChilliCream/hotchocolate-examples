# DataLoader Example

This example shows how DataLoader can be used with Hot Chocolate and uses _Mongo_ as database.

The **Cache DataLoader** and the **Batch DataLoader** are used in `MessageType.cs`.

The **GroupDataLoader** is used in `QueryType.cs`.

We support more DataLoader scenarious with Hot Chocolate than are showcased with this example. The example is aimed to show the most common use-cases.

## Setup Mongo

Personally I used docker to host my mongo db for the example. If you have setup docker that just add the following line in your terminal emulator of choice:

```bash
docker run --name mongo -p 27017:27017 -d mongo mongod
```

If you don't have docker or do not want to use it you can install mongo from here: [https://www.mongodb.com/download-center/community](https://www.mongodb.com/download-center/community).

[Hot Chocolate Documentation](https://hotchocolate.io) 
