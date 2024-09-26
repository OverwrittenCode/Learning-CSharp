namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class CommunityInitiative : BaseScene
{
    public CommunityInitiative()
    {
        Choices.Add(new("Propose a student-led project", () => new StudentProject()));
        Choices.Add(
            new("Suggest partnering with local businesses", () => new BusinessPartnership())
        );
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("What can we do as students to help our local community?")
            .SudoTeacher("That's a great question. There are several ways we can contribute...")
            .Say("The teacher outlines various community support initiatives.")
            .Init();
    }
}
