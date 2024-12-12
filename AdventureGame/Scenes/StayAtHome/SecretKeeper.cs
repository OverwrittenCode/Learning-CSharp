using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.StayAtHome;

internal sealed class SecretKeeper : BaseScene
{
    public SecretKeeper(bool isFromFriendshipTest = false)
    {
        Choices.Add(new("Investigate further", () => new SecretInvestigation(isFromFriendshipTest)));
        Choices.Add(new("Try to forget about it", () => new UneasyNormalcy()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You decide to keep the secret to yourself.")
                                    .SudoUser("(thinking) I can't believe what I've discovered. What should I do with this information?")
                                    .Say("The weight of the secret feels heavy on your shoulders.")
                                    .Init();
}
