namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class RealWorldEconomics : BaseScene
{
    public RealWorldEconomics()
    {
        Choices.Add(new("Ask about local impacts", () => new LocalEconomyDiscussion()));
        Choices.Add(new("Inquire about global effects", () => new GlobalEconomyDiscussion()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("How do these economic concepts apply to what's happening in our town?")
            .SudoTeacher("An excellent question. Let's explore that...")
            .Say("The teacher begins to explain the local economic situation.")
            .Init();
    }
}
