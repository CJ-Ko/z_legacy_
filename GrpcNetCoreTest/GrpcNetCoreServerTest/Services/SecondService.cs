using Grpc.Core;
using GrpcProtoLib;
using GrpcProtoLib.Model.Common;

namespace GrpcNetCoreServerTest.Services
{
    public class SecondService : TypeBService.TypeBServiceBase
    {
        public override Task<BaseResponseModel> SecondHello(BaseRequestModel request, ServerCallContext context)
        {
            var result = new BaseResponseModel
            {
                Message = $"SecondHello {request.Name}"
            };
            Console.WriteLine(result.Message);
            return Task.FromResult(result);
        }
    }
}
