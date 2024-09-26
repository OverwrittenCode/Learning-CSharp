namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class SchoolProtest : BaseScene
{
    public SchoolProtest()
    {
        Choices.Add(new("Lead the protest", () => new ProtestLeader()));
        Choices.Add(new("Participate quietly", () => new QuietParticipant()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You and your classmates organize a peaceful protest in the school yard.")
            .Say("Students gather, holding signs and chanting for change.")
            .SudoUser("We demand transparency and action!")
            .Say("You notice teachers and the principal watching from the windows.")
            .Init();
    }
}
