using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class CareerGuidance : BaseScene
{
    public CareerGuidance()
    {
        Choices.Add(new("Ask about emerging industries", () => new EmergingCareers()));
        Choices.Add(new("Inquire about further education", () => new EducationPathways()));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("Given the current economic situation, what career paths should we consider?")
                                    .SudoTeacher("That's a forward-thinking question. Let's discuss some options...")
                                    .Say("The teacher provides insights into various career opportunities and their prospects.")
                                    .Init();
}
