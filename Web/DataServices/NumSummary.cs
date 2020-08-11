using Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Hubs;

namespace Web.DataServices
{
    public class NumSummary
    {
        private string NumType;
        private readonly object _fetchNumbers = new object();
        private readonly object _updateNumbers = new object();
        private readonly NumService _numService;
        private readonly TimeSpan _updateInterval;

        private readonly Timer _timer;
        private NumModel _result;

        private IHubContext<NumberHub> HubContext { get; set; }
        public NumSummary(IHubContext<NumberHub> hubContext, IConfiguration configuration, NumService service)
        {
            _numService = service;
            HubContext = hubContext;

            _updateInterval = TimeSpan.FromMilliseconds(int.Parse(configuration["SystemSettings:SignalRUpdateInterval"]));
            _timer = new Timer(UpdateSubstationSummaryDashboard, null, _updateInterval, _updateInterval);

        }

        public NumModel Get(string numType)
        {
            NumModel _result = new NumModel();
            lock (_fetchNumbers)
            {
                NumType = numType;
                _result = _numService.Get(numType);
                return _result;
            }
        }

        private void UpdateSubstationSummaryDashboard(object state)
        {
            lock (_updateNumbers)
            {
                _result = Get(NumType);
                BroadcastNumbers();
            }
        }

        private void BroadcastNumbers()
        {
            HubContext.Clients.All.SendAsync("broadcastNumbers", _result, NumType);
        }
    }
}
