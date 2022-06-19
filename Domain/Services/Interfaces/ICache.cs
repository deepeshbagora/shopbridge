using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge;

public interface ICache
{
    List<CacheModel> ListALlElements();
    
    CacheModel GetElement(string ElementName);

    void SetElement(CacheModel Element);

    bool RemoveELement(string ElementName);

    void ClearCache();

}