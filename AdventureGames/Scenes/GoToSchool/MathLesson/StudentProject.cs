using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class StudentProject : BaseScene
{
    public StudentProject()
    {
        Choices.Add(new("Organize a community fundraiser", () => new CommunityFundraiser()));
        Choices.Add(new("Start a school newspaper", () => new SchoolNewspaper()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("What if we started a student-led project to help our community?")
            .SudoTeacher("That's an excellent idea. What kind of project did you have in mind?")
            .Say("Your classmates look interested and start suggesting ideas.")
            .Init();
    }
}
