using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class EducationPathways : BaseScene
{
    public EducationPathways()
    {
        Choices.Add(new("Inquire about university options", () => new UniversityPreparation()));
        Choices.Add(new("Ask about vocational training", () => new VocationalTraining()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoTeacher("There are many paths to success. Let's discuss your educational options.")
            .Say("The teacher outlines various educational pathways and their potential outcomes.")
            .SudoUser("I never realized we had so many choices.")
            .Init();
    }
}
