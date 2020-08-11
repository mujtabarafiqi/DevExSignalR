using Data;
using Microsoft.AspNetCore.SignalR;
using Service;
using Web.DataServices;

namespace Web.Hubs
{
    public class NumberHub : DynamicHub
    {
        private readonly NumSummary _numService;

        public NumberHub(NumSummary numService)
        {
            _numService = numService;
        }
        public NumModel GetData(string numType)
        {
            return _numService.Get(numType);
        }
    }
}
