using PISWF.domain.registermc.service;

namespace PISWF.domain.registermc.model.entity;

public class RegisterMcComparer : IComparer<RegisterMC>
{
    private SortPatemeters<RegisterMC> _sortPatemeters;
    
    public RegisterMcComparer(SortPatemeters<RegisterMC> sortPatemeters)
    {
        _sortPatemeters = sortPatemeters;
    }

    public int Compare(RegisterMC x, RegisterMC y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        
        var idComparison = x.Id.CompareTo(y.Id);
        var numberComparison = x.Number.CompareTo(y.Number);
        var validDateComparison = x.ValidDate.CompareTo(y.ValidDate);
        var yearComparison = x.Year.CompareTo(y.Year);
        
        if (idComparison != 0) return idComparison;
        if (numberComparison != 0) return numberComparison;
        if (validDateComparison != 0) return validDateComparison;
        if (yearComparison != 0) return yearComparison;
        
        return x.Price.CompareTo(y.Price);
    }
}