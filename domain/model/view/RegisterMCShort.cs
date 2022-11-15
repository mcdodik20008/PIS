namespace PISWF.domain.registermc.model.view;

// TODO: Подумать над тем, что должно быть сдесь

public class RegisterMCShort
{
    public long Id { get; set; }

    public string Number { get; set; }

    public DateTime ValidDate { get; set; }
    
    public int Year { get; set; }

    public Double Price { get; set; }
}