namespace AdventureGames.Scenes.StayAtHome;

internal sealed class FamilyRift : BaseScene
{
    public FamilyRift()
    {
        Choices.Add(new("Try to reconcile", () => new FamilyReconciliation()));
        Choices.Add(new("Leave home", () => new RunAway()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("Your refusal to get involved creates a rift in your family.")
            .SudoFather("I thought we raised you better than this.")
            .SudoMother("Please, try to understand our position.")
            .SudoUser("I can't believe you've been lying to me all this time!")
            .Init();
    }
}
