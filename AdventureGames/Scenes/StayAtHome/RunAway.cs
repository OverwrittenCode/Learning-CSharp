namespace AdventureGames.Scenes.StayAtHome;

internal sealed class RunAway : BaseScene
{
    public RunAway()
    {
        Choices.Add(new("Return home", () => new FamilyReconciliation()));
        Choices.Add(new("Seek help from Jack", () => new FriendshipSupport()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You hastily pack a bag and slip out of the house.")
            .Say("As you walk down the street, the weight of your decision sinks in.")
            .SudoUser("What have I done? Where can I go now?")
            .Init();
    }
}
