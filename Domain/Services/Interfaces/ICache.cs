using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge;

///summary
/// Implementation of this interface will give ability to store data in quick accessible location which will help to reduce load from Database and improve performance
/// Various types on Cache implementations can be done like in Memory, redis, Static memory etc.
///summary

public interface ICache
{
    //Method will be used to list all the elements in cache
    List<CacheModel> ListALlElements();
    
    //Method will be used to fetch specific object
    CacheModel GetElement(string ElementName);

    //Method will be used to add or update elements in Cache
    void SetElement(CacheModel Element);

    //Method will remove specific element
    bool RemoveELement(string ElementName);

    //Method will remove all elements from cache
    void ClearCache();

}