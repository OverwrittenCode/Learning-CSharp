using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class InnovativeSolutions : BaseScene
{
    public InnovativeSolutions()
    {
        Choices.Add(new("Propose a digital currency solution", () => new DigitalEconomyPioneer()));
        Choices.Add(new("Suggest a community-based economy", () => new LocalEconomyRevolution()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("What if we completely reimagined our economic system?")
            .SudoTeacher("That's a bold idea. What did you have in mind?")
            .Say("The class listens intently as you share your innovative ideas.")
            .Init();
    }
}
