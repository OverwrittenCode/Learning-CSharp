namespace AdventureGames.Entities.Collectables;

public sealed class MysteriousMap : CollectableBase
{
    public MysteriousMap()
        : base("A map with strange markings.") { }

    public override void Interact()
    {
        Game.Say("Hm... the map shows a route, but the destination is unclear.");
    }
}
