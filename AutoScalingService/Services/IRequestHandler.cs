using AutoScalingService.Models;

namespace AutoScalingService.Services
{
    public interface IRequestHandler
    {
        string Push(Request request);
        Request Delete(string requestId);
        Request Get(string requestId);
    }
}
