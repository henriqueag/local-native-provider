using System.Data;

namespace LocalNativeProvider.BusinessObjects;

public class BusinessObjectList : List<IBusinessObject>
{
    public static readonly IReadOnlyCollection<IBusinessObject> Instance = new BusinessObjectList();

    private BusinessObjectList()
    {
        var businessObjects = typeof(BusinessObjectList).Assembly
            .GetTypes()
            .Where(x => !x.IsAbstract && x.IsClass)
            .Where(x => typeof(IBusinessObject).IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<IBusinessObject>()
            ;

        foreach(var item in businessObjects)
        {
            Add(item);
        }
    }
}