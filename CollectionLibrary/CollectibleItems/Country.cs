namespace CollectionLibrary.CollectibleItems;

public class Country : IHasId
{
    public virtual int Id {get; set;}
    public virtual string Name { get; set; }

    public Country()
    {
    }
    public Country(string name)
    {
        this.Name = name;
    }
}