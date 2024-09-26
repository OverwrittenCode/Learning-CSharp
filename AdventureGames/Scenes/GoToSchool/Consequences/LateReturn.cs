namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class LateReturn : BaseScene
{
    public LateReturn()
    {
        Choices.Add(new("Sneak into class", () => new ClassroomSneaking()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You decide to return to school after your city adventure.")
            .Say("The hallways are empty, and you can hear classes in session.")
            .SudoUser("I hope I can slip in without anyone noticing...")
            .Init();
    }
}
