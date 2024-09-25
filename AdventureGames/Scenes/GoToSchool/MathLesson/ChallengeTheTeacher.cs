using AdventureGames.Scenes.GoToSchool.Consequences;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

/// <inheritdoc/>
public sealed class ChallengeTheTeacher : BaseScene
{
    /// <summary>
    /// Authority? Nah, just a word thrown around to assert order.
    /// </summary>
    public ChallengeTheTeacher()
    {
        Choices.Add(
            new Choice(
                "Apologize and back down",
                () =>
                {
                    new ConversationBuilder()
                        .SudoUser("No sir.")
                        .SudoTeacher("That's what I thought.")
                        .Init();

                    return new Detention();
                }
            )
        );
        Choices.Add(new Choice("Stand your ground", () => new PrincipalOffice()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("But sir, the homework was unreasonable given everything that's happening!")
            .SudoTeacher("Excuses won't help you in the real world, young man.")
            .Say("The teacher's face reddens with anger.")
            .SudoTeacher("Perhaps you'd like to discuss this further with the principal?")
            .Init();
    }
}
