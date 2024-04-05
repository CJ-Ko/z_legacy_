// See https://aka.ms/new-console-template for more information
using GrpcClientLib;
using GrpcProtoLib.Model.Common;
using Microsoft.Extensions.Configuration;
using System;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true );

IConfiguration config = builder.Build();

Console.WriteLine("Hello, World!");
var gClientConnect = new GrpcClientLib.ClientConnector(config).TypeA;
Console.WriteLine("client is connected?");
var result = gClientConnect.FirstHello(new BaseRequestModel { Name = "Jhon Doe" });
Console.WriteLine($@"result is {result.Result.Message}");
Console.ReadLine();