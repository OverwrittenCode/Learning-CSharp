namespace AdventureGame.Entities.Collectables;

/// <summary>
///     <para>
///         <b>
///             <see cref="CollectableBase" />
///         </b>
///         Provides a base structure for all collectables in the adventure game.
///     </para>
/// </summary>
internal abstract class CollectableBase
{
    private string Description { get; }

    /// <summary>
    ///     Equal to the name of the class.
    /// </summary>
    public string Name { get; }

    protected CollectableBase(string description)
    {
        Description = description;
        Name = GetType().Name;
    }

    /// <summary>
    ///     An interactive way to view the contents of the collectable. The <see cref="Description" /> is sent by default.
    /// </summary>
    public virtual void Interact() => Game.Say(Description);
}
