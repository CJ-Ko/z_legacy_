using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Balancer;
using Grpc.Net.Client.Configuration;
using GrpcClientLib.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace GrpcClientLib
{
    public class ClientConnector
    {
        //private static string _serverIp = ConfigurationManager.AppSettings["AppConfiguration:RpcServer"].ToString();
        private readonly IConfiguration configuration;
        private static GrpcChannel _channel; /*= GrpcChannel.ForAddress(_serverIp);*/
        private static readonly MethodConfig _methodConfig = new MethodConfig
        {
            Names = { MethodName.Default },
            RetryPolicy = new RetryPolicy
            {                
                MaxAttempts = 5,
                InitialBackoff = TimeSpan.FromMilliseconds(10),
                MaxBackoff = TimeSpan.FromMilliseconds(100),
                BackoffMultiplier = 1.5,
                RetryableStatusCodes = { StatusCode.Unavailable }
            }
        };
        private static Dictionary<string, GrpcClient> _clients = new Dictionary<string, GrpcClient>();
        private static object obj = new object();

        

        public ClientConnector(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            var serverIps = configuration.GetValue<string>("AppConfiguration:RpcServer");


            #region 테스트용 설정
            //var serverList = "localhost:5142";
            //var factory = new StaticResolverFactory(addr => new[]
            //{
            //    new BalancerAddress("localhost", 5142)
            //});
            #endregion
            
            var serverList = serverIps.Split(',').ToList();
            var factory = new StaticResolverFactory(addr =>
            {
                return serverList.Select(server => new BalancerAddress(server.Split(":")[0], int.Parse(server.Split(":")[1]))).ToArray();
            });
            services.AddSingleton<ResolverFactory>(factory);

            //client side 를 위해서는 모든 ip가 
            _channel = GrpcChannel.ForAddress("static:///example-host", new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure,
                ServiceConfig = new ServiceConfig { LoadBalancingConfigs = { new RoundRobinConfig() }, MethodConfigs = { _methodConfig } },
                ServiceProvider = services.BuildServiceProvider()
            });
            
        }
        
        private static T Getclient<T>(string key, Func<T> creator) where T : Client.GrpcClient
        {
            if(!_clients.ContainsKey(key))
            {
                lock(obj)
                {
                    if (!_clients.ContainsKey(key))
                    {
                        _clients[key] = creator();
                    }
                }
            }
            return (T)_clients[key];
        }

        public TypeAClient TypeA
        {
            get
            {
                return Getclient("TypeA", () => new TypeAClient(_channel));
            }
        }
    }
}
