namespace AdventureGames.Scenes.Endings;

internal sealed class MassMovement : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "Your leadership in the school protest evolves into a nationwide student movement."
            )
            .Say(
                "You organize rallies and campaigns advocating for economic and educational reform."
            )
            .SudoUser("Our voices are powerful when we stand united.")
            .Say(
                "The movement you started brings about significant changes in policy and public awareness."
            )
            .Say("THE END - Mass Movement Leader Ending")
            .Init();
    }
}
