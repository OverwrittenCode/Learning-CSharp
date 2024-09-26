using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class FriendshipSupport : BaseScene
{
    public FriendshipSupport()
    {
        Choices.Add(new("Form a student alliance", () => new StudentAlliance()));
        Choices.Add(new("Return home with Jack's advice", () => new FamilyReconciliation()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You arrive at Jack's house, explaining your situation.")
            .SudoJack("This is serious. We need to think carefully about what to do next.")
            .SudoUser("I'm glad I can count on you, Jack. What do you think we should do?")
            .Init();
    }
}
