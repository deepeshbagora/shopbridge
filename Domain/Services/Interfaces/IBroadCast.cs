using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge;

public interface IBroadCast
{

    ///summary
    /// This Interface is used for sending real-time async messages to users subscribed into application
    /// Various types on Cache implementations can be done like in Memory, redis, Static memory etc.
    ///summary
    public Task Get();

    public void Broadcast(BroadCastModel Data);
    
}