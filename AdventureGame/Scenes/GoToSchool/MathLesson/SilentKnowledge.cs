using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;
using AdventureGame.Scenes.StayAtHome;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class SilentKnowledge : BaseScene
{
    public SilentKnowledge()
    {
        Choices.Add(new("Investigate after class", () => new SecretInvestigation()));
        Choices.Add(new("Try to forget about it", () => new InnerConflict()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You decide to keep your observations to yourself.")
                                    .Say("The class continues, but you can't shake the feeling that something is wrong.")
                                    .SudoUser("(thinking) Should I dig deeper into this, or is it safer to stay out of it?")
                                    .Say("The bell rings, signaling the end of class.")
                                    .SudoTeacher("Remember to review today's lesson. It's more important than you might think.")
                                    .Init();
}
