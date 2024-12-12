using AdventureGame.Helpers;

namespace AdventureGame.Scenes.StayAtHome;

internal sealed class FriendshipTest : BaseScene
{
    public FriendshipTest()
    {
        Choices.Add(new("Trust Jack and investigate together", () => new SecretInvestigation(true)));
        Choices.Add(new("Keep the information to yourself", () => new SecretKeeper(true)));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("Jack, I need to tell you something important. It's about the resistance...")
                                    .Say("You explain what you've discovered to Jack, watching his reaction carefully.")
                                    .SudoJack("Wow, this is huge. Are you sure about this? What should we do?")
                                    .SudoUser("I'm not sure. That's why I'm telling you. Can I trust you with this?")
                                    .Init();
}
