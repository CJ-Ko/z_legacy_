using Grpc.Net.Client;
using GrpcProtoLib;
using GrpcProtoLib.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcClientLib.Client
{
    public class TypeAClient : GrpcClient
    {
        private readonly TypeAService.TypeAServiceClient _client;

        public TypeAClient(GrpcChannel channel)
        {
            _client = new TypeAService.TypeAServiceClient(channel);
        }
        public async Task<BaseResponseModel> FirstHello(BaseRequestModel request)
        {
            return await _client.FirstHelloAsync(request);
        }
    }
}
