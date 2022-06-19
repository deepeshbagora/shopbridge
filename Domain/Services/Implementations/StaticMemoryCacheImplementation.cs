using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge;

public class StaticMemoryCacheImplementation : ICache
{

    private static List<CacheModel> CacheObject;

    public StaticMemoryCacheImplementation() => CacheObject = new List<CacheModel>();

    public void ClearCache()
    {
        try
        {
            CacheObject.Clear();

        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public CacheModel GetElement(string ElementName)
    {
        return CacheObject.Find(x => x.PropertyName == ElementName);
    }

    public List<CacheModel> ListALlElements()
    {
        return CacheObject;
    }

    public bool RemoveELement(string ElementName)
    {
        try
        {
            CacheModel item = CacheObject.Find(x => x.PropertyName == ElementName);
            return CacheObject.Remove(item);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void SetElement(CacheModel Element)
    {
        CacheModel item = CacheObject.Find(x => x.PropertyName == Element.PropertyName);
        if (item != null)
        {
            if (RemoveELement(Element.PropertyName))
            {
                CacheObject.Add(Element);
            }
        }
        else
        {
            CacheObject.Add(Element);
        }


    }

}