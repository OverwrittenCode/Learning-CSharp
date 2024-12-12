using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class BusinessPartnership : BaseScene
{
    public BusinessPartnership()
    {
        Choices.Add(new("Propose an internship program", () => new InternshipProgram()));
        Choices.Add(new("Suggest a mentorship initiative", () => new MentorshipInitiative()));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("Could we partner with local businesses for real-world experience?")
                                    .SudoTeacher("That's a fantastic suggestion. Let's explore some possibilities.")
                                    .Say("The class discusses various ways to collaborate with local businesses.")
                                    .Init();
}
