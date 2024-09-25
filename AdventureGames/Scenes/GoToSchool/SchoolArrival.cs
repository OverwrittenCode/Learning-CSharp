using AdventureGames.Scenes.GoToSchool.Explore;
using AdventureGames.Scenes.GoToSchool.MathLesson;

namespace AdventureGames.Scenes.GoToSchool;

/// <inheritdoc/>
public sealed class SchoolArrival : BaseScene
{
    /// <summary>
    /// You have arrived at school. Weird that school is still on despite the news?
    /// </summary>
    public SchoolArrival()
    {
        Choices.Add(new Choice("Go to math class", () => new MathClass()));
        Choices.Add(new Choice("Skip class and explore the school", () => new SchoolExploration()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You arrive at school, the atmosphere feels tense.")
            .Say("Students are whispering in hushed tones, some looking worried.")
            .SudoUser("Something feels off...")
            .Say("You see your friend Jack in the distance, looking anxious.")
            .Init();
    }
}
