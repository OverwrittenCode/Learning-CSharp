namespace AdventureGames.Scenes.Endings;

internal sealed class EconomicPreparedness : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You focus on developing strategies to prepare for future economic challenges.")
            .Say(
                "Your foresight helps individuals and businesses build resilience against economic shocks."
            )
            .SudoUser("We can't predict everything, but we can be ready for anything.")
            .Say("Your work in economic preparedness becomes invaluable during times of crisis.")
            .Say("THE END - Economic Preparedness Expert Ending")
            .Init();
    }
}
