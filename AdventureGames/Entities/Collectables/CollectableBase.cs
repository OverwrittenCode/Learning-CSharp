namespace AdventureGames.Entities.Collectables;

/// <summary>
/// <para>
/// <b><see cref="CollectableBase"/></b>
/// Provides a base structure for all collectables in the adventure game.
/// </para>
/// </summary>
public abstract class CollectableBase
{
    /// <summary>
    /// Equal to the name of the class.
    /// </summary>
    public string Name { get; }
    public string Description { get; }
    public int Worth { get; }
    public int RequiredIntelligence { get; }
    public int Uses { get; protected set; }
    public int MaxUses { get; }

    public CollectableBase(
        string description,
        int worth = 0,
        int requiredIntelligence = 0,
        int maxUses = -1
    )
    {
        Description = description;
        Name = GetType().Name;
        Worth = worth;
        RequiredIntelligence = requiredIntelligence;
        MaxUses = maxUses;
    }

    /// <summary>
    /// An interactive way to view the contents of the collectable. The <see cref="Description"/> is sent by default.
    /// </summary>
    public virtual void Interact()
    {
        Uses++;
        Game.Say(Description);
    }
}
