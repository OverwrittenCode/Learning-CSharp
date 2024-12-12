using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class NextClass : BaseScene
{
    public NextClass()
    {
        Choices.Add(new("Pay attention in class", () => new RegularSchoolDay()));
        Choices.Add(new("Pass note to Jack", () => new SecretCommunication()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You enter your next class, still thinking about the economics discussion.")
                                    .Say("The teacher starts talking about history, but your mind wanders.")
                                    .SudoUser("(thinking) How does all of this connect to what's happening now?")
                                    .Init();
}
