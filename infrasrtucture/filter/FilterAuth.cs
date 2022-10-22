using PISWF.infrasrtucture.auth.model.entity;

namespace PISWF.infrasrtucture.filter;

public class UserFilter : FilterModel<User>
{
 /*   public string Name { get; set; }
    
    public string Group { get; set; }
    
    public short? DefYear { get; set; }
    
    public bool? IsDefended { get; set; }

    public SelectList DefYearList
    {
        get { return new SelectList(new List<short> { 2011, 2012 }, DefYear); }
    }

    public SelectList IsDefendedList
    {
        get
        {
            var list = new List<object>
            {
                {new {Id="true", Title="Защищена"}},
                {new {Id="false", Title="Не защищена"}},
            };

            return new SelectList(list, "Id", "Title", IsDefended);
        }
    }

    public override Func<Student, bool> FilterExpression
    {
        get
        {
            return p =>
                (String.IsNullOrEmpty(Name) || p.Name.IndexOf(Name) == 0) &&
                (String.IsNullOrEmpty(Group) || p.Group.ToLower().IndexOf(Group.ToLower()) == 0) &&
                (DefYear == null || p.DefYear == DefYear) &&
                (IsDefended == null || (p.IsDefended == IsDefended));
        }
    }

    public override void Reset()
    {
        Name = null; Group = null; DefYear = null; IsDefended = null;
    }*/
 public override Func<User, bool> FilterExpression { get; }
}