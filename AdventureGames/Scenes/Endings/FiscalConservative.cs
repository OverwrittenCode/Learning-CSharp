namespace AdventureGames.Scenes.Endings;

internal sealed class FiscalConservative : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You adopt a fiscally conservative approach to managing resources.")
            .Say("Your careful planning and budgeting lead to financial stability.")
            .SudoUser("Prudent management of resources is essential for long-term success.")
            .Say("Your fiscal conservatism earns you respect and admiration.")
            .Say("THE END - Fiscal Conservative Ending")
            .Init();
    }
}
