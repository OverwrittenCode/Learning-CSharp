namespace AdventureGames.Scenes.Endings;

internal sealed class StudentRepresentative : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "You become the official student representative, working with the school administration."
            )
            .Say("Your diplomatic approach leads to meaningful dialogue and positive changes.")
            .SudoUser("We can achieve more through cooperation and understanding.")
            .Say(
                "Your success in negotiating student interests paves the way for your future in politics."
            )
            .Say("THE END - Student Diplomat Ending")
            .Init();
    }
}
