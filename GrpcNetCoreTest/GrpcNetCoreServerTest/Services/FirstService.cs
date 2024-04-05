using Grpc.Core;
using GrpcProtoLib;
using GrpcProtoLib.Model;
using GrpcProtoLib.Model.Common;

namespace GrpcNetCoreServerTest.Services
{
    public class FirstService : TypeAService.TypeAServiceBase

    {
        public override Task<BaseResponseModel> FirstHello(BaseRequestModel request, ServerCallContext context)
        {

            var result = new BaseResponseModel
            {
                Message = $"{request.Name} Hello, World!"
            };
            Console.WriteLine(result.Message);
            return Task.FromResult(result);
        }
    }
}
