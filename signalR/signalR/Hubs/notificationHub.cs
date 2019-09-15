﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace signalR.Hubs
{
    public class notificationHub: Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["newConnection"].ToString();
        public void Hello()
        {
            Clients.All.hello();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<notificationHub>();
            context.Clients.All.updateMessages();
        }
    }
}