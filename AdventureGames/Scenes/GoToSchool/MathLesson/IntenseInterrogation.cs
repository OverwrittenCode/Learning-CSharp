namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class IntenseInterrogation : BaseScene
{
    public IntenseInterrogation()
    {
        Choices.Add(new("Reveal everything you know", () => new FullDisclosure()));
        Choices.Add(new("Remain vague and evasive", () => new EvasiveTactics()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("I know about the resistance. What's really going on?")
            .Say("The teacher's face pales, then hardens.")
            .SudoTeacher("This is a very serious accusation. Where did you hear this?")
            .Say("The atmosphere in the room becomes tense.")
            .Init();
    }
}
