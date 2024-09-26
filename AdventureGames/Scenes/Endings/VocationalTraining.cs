namespace AdventureGames.Scenes.Endings;

internal sealed class VocationalTraining : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You opt for vocational training, focusing on practical skills.")
            .Say("Your hands-on approach leads you to become a skilled tradesperson.")
            .SudoUser("There's value in being able to build and fix things with your own hands.")
            .Say("Your expertise becomes crucial in rebuilding and maintaining infrastructure.")
            .Say("THE END - Skilled Tradesperson Ending")
            .Init();
    }
}
