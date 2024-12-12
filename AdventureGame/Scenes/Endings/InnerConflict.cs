using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class InnerConflict : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say(
                                         """
                                         You decide to pretend you saw nothing, but the knowledge weighs heavily on you.
                                         As days pass, you struggle with your conscience.
                                         The world around you continues to change, and you wonder if you made the right choice.
                                         """
                                     )
                                    .SudoUser("Maybe it's not too late to make a difference...")
                                    .Say("THE END - Inner Conflict Ending")
                                    .Init();
}
