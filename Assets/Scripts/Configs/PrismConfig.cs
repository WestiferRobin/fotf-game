using System;

public class PrismConfig: IConfig
{
    private const string UNKNOWN = "Unknown";

    public Guid ID { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Name 
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public PrismConfig(Guid id, string firstName, string lastName)
    {
        InitConfig(id, firstName, lastName);
    }

    public PrismConfig(string firstName, string lastName)
    {
        InitConfig(new Guid(), firstName, lastName);
    }

    public PrismConfig()
    {
        InitConfig(new Guid(), UNKNOWN, UNKNOWN);
    }

    private void InitConfig(
        Guid id, 
        string firstName, 
        string lastName
    )
    {
        ID = id;
        FirstName = firstName;
        LastName = lastName;
    }
}