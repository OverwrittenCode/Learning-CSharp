namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class EscapeAttempt : BaseScene
{
    public EscapeAttempt()
    {
        Choices.Add(new Choice("Return to detention", () => new Detention()));
        Choices.Add(new Choice("Continue escaping", () => new SchoolEscape()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You quietly open the door and peek outside.")
            .Say("The hallway seems empty, but you hear distant footsteps.")
            .SudoUser("This is my chance...")
            .Say("You step out into the hallway, heart pounding.")
            .Say("Suddenly, you hear a voice.")
            .SudoTeacher("Hey! Where do you think you're going?")
            .Say("You freeze, unsure what to do next.")
            .Init();
    }
}
