using PISWF.infrasrtucture.muni_org.model.entity;
using PISWF.infrasrtucture.muni_org.model.view;

namespace PISWF.domain.registermc.model.view;

public class RegisterMCLong
{
    public long Id { get; set; }

    public string Number { get; set; }
    
    public DateTime ValidDate { get; set; }
    
    public string Location { get; set; }
    
    public DateTime ActionDate { get; set; }

    public OrganizationShort OrganizationShort { get; set; }

    public MunicipalityShort MunicipalityShort { get; set; }
    
    public string Omsu { get; set; }
    
    public int Year { get; set; }

    public Double Price { get; set; }
 
    public Double SubventionShare { get; set; }

    public Double AmountMoney { get; set; }
    
    public Double ShareFundsSubvention { get; set; }
    
    public List<FileDocumentShort> Documents { get; set; }
}