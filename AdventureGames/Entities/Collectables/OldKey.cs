namespace AdventureGames.Entities.Collectables;

public sealed class OldKey : CollectableBase
{
    public OldKey()
        : base("An old, rusty key. Where does it go?") { }

    public override void Interact()
    {
        Game.Say("It's a key, but to what?");
    }
}
