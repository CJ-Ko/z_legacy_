using Grpc.Core;
using GrpcSample_01;
using GrpcSample_01.Practice;

namespace GrpcSample_01.Services
{
    public class RequestTypeService : requestType.requestTypeBase
    {   

        public override Task<TestDynamicResponse> TestDynamic(IAsyncStreamReader<TestDynamicRequest> requestStream, ServerCallContext context)
        {
            return base.TestDynamic(requestStream, context);
        }
    }
}
