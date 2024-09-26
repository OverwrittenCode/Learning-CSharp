using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class TraditionalApproaches : BaseScene
{
    public TraditionalApproaches()
    {
        Choices.Add(new("Advocate for fiscal responsibility", () => new FiscalConservative()));
        Choices.Add(new("Propose government intervention", () => new GovernmentIntervention()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("Maybe we should look at what's worked in the past?")
            .SudoTeacher("Good thinking. Let's examine some traditional economic approaches.")
            .Say(
                "The class discusses various established economic theories and their applications."
            )
            .Init();
    }
}
