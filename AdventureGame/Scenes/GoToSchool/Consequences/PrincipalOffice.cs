using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.Consequences;

internal sealed class PrincipalOffice : BaseScene
{
    public PrincipalOffice()
    {
        Choices.Add(new("Apologize and accept punishment", () => new Detention()));
        Choices.Add(new("Explain your concerns", () => new PrincipalDiscussion()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You're escorted to the principal's office.")
                                    .Say("The principal looks at you sternly.")
                                    .Say("The principal sighs deeply.")
                                    .SudoPrinciple("What do you have to say for yourself?")
                                    .Init();
}
