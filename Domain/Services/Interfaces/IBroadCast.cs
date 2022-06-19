using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge;

public interface IBroadCast
{

    public Task Get();

    public void Broadcast(BroadCastModel Data);
    
}