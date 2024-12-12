using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class SecretCommunication : BaseScene
{
    public SecretCommunication()
    {
        Choices.Add(new("Plan a secret meeting", () => new StudentResistance()));
        Choices.Add(new("Share information cautiously", () => new InformationNetwork()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You discreetly pass a note to Jack.")
                                    .SudoUser("(whispering) We need to talk about what we've learned. It's important.")
                                    .Say("Jack nods subtly, understanding the gravity of the situation.")
                                    .Init();
}
