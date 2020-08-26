using Data;
using Microsoft.AspNetCore.SignalR;
using Service;
using System.Threading.Tasks;
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
            var x = _numService.Get(numType);

            Clients.Group(numType).SendAsync("broadcastNumbers", x, numType);
            return x;

        }
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }
    }
}
