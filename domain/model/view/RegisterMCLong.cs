using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.domain.registermc.model.view;

public class RegisterMCLong
{
    public long Id { get; set; }

    public int Number { get; set; }

    public DateTime ValidDate { get; set; }

    public Organization Organization { get; set; }

    public Municipality Municipality { get; set; }
    
    public int Year { get; set; }

    public Double Price { get; set; }
 
    public Double SubventionShare { get; set; }

    public Double AmountMoney { get; set; }
    
    public Double ShareFundsSubvention { get; set; }
    
    public List<FileDocumentShort> Documents { get; set; }
}