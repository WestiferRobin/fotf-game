using System;
using FotF.Api.Enums;
using FotF.Api.Enums.Units;
using FotF.Api.Prisms;

public class TrooperConfig: PrismConfig
{
    public PrismAgent Agent { get; private set; }
    public FactionType Faction { get; private set; }
    public TrooperClass Class { get; private set; }

    public TrooperConfig(PrismConfig config, FactionType faction, TrooperClass type): base(config.ID, config.FirstName, config.LastName)
    {
        Agent = new PrismAgent(config);
        Faction = faction;
        Class = type;
    }

    public TrooperConfig(PrismAgent agent, FactionType faction, TrooperClass type): base(agent.Config.ID, agent.Config.FirstName, agent.Config.LastName)
    {
        Agent = agent;
        Faction = faction;
        Class = type;
    }

    public TrooperConfig(FactionType faction, TrooperClass type, string firstName, string lastName): base(new Guid(),firstName, lastName)
    {
        Agent = new PrismAgent(new PrismConfig(ID, FirstName, LastName));
        Faction = faction;
        Class = type;
    }

    public  override string ToString()
    {
        return $"{Name} - ({Faction}, {Class})";
    }
}