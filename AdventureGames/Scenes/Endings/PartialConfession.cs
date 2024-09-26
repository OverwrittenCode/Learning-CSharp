namespace AdventureGames.Scenes.Endings;

internal sealed class PartialConfession : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You decide to reveal part of what you know, hoping to gain more information.")
            .SudoUser("I overheard some things, but I'm not sure what it all means.")
            .Say("The teacher looks conflicted, then sighs.")
            .SudoTeacher("It's complicated. Let me explain what I can...")
            .Say("You gain some insights, but are left with more questions than answers.")
            .Say("THE END - Partial Truth Ending")
            .Init();
    }
}
