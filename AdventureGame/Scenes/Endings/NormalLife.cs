using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class NormalLife : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You choose to ignore the strange occurrences and focus on your studies.")
                                    .Say("Days pass, and life seems to return to normal.")
                                    .Say("Yet, you can't shake the feeling that you've missed something important.")
                                    .SudoUser("Maybe it's better this way. At least we're safe.")
                                    .Say("You graduate from school, leaving behind unanswered questions.")
                                    .Say("THE END - Normal Life Ending")
                                    .Init();
}
