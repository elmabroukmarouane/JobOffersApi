using Microsoft.AspNetCore.SignalR;

namespace JobsOffer.Api.Server.RealTime.Class
{
    public class RealTimeHub : Hub
    {
        #region ATTRIBUTE
        private IList<string> ConnectedUsers { get; set; }
        #endregion

        #region CONTRUCTOR
        public RealTimeHub()
        {
            ConnectedUsers = [];
        }
        #endregion

        #region METHODS
        public override async Task OnConnectedAsync()
        {
            ConnectedUsers.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
            var message = "Connected successfully!";
            await Clients.Caller.SendAsync("Message", message);
        }

        public async Task SendEntityToAll(object entitiy)
        {
            await Clients.All.SendAsync("SendEntityToAll", entitiy);
        }

        public async Task SendEntitiesToAll(object[] entities)
        {
            await Clients.All.SendAsync("SendEntitesToAll", entities);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedUsers.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public IList<string> GetConnectedUsersList()
        {
            return ConnectedUsers;
        }
        #endregion
    }
}
