namespace AdventureGames.Entities.Collectables;

internal sealed class Homework : CollectableBase
{
    public Homework()
        : base("Your maths homework") { }

    public override void Interact()
    {
        Game.Say(
            $"""
            [Candidate Name]: {Game.User}
            Q: What is the area of a circle, radius 5cm, in terms of PI? (1 mark)
            A: PI * 5 * 5 = 25PI squared centimetres
            """
        );
    }
}
