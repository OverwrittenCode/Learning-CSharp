using AdventureGames.Scenes.GoToSchool.Consequences;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class SecretInvestigation : BaseScene
{
    private readonly bool _isFromFriendshipTest;

    public SecretInvestigation(bool isFromFriendshipTest = false)
    {
        _isFromFriendshipTest = isFromFriendshipTest;

        Choices.Add(new("Investigate the school basement", () => new BasementDiscovery()));
    }

    public override void Play()
    {
        if (_isFromFriendshipTest)
        {
            return;
        }

        Choices.Add(new("Confront Jack with the truth", () => new FriendshipTest()));

        new ConversationBuilder()
            .Say("You and Jack decide to investigate the claims about the school basement.")
            .Say("As you approach the school, you see teachers entering through a side door.")
            .SudoJack("Look! They're acting really suspicious.")
            .SudoUser("We need to be careful. This could be dangerous.")
            .Say("You both hide behind some bushes, watching the entrance.")
            .Init();
    }
}
