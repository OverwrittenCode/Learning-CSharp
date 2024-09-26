using AdventureGames.Scenes.GoToSchool.Consequences;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ChallengeTheTeacher : BaseScene
{
    public ChallengeTheTeacher()
    {
        Choices.Add(
            new(
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
        Choices.Add(new("Stand your ground", () => new PrincipalOffice()));
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
