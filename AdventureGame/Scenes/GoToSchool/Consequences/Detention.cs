using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.Consequences;

internal sealed class Detention : BaseScene
{
    public Detention()
    {
        Choices.Add(new("Use time productively", () => new ProductiveDetention()));
        Choices.Add(new("Attempt to escape", () => new EscapeAttempt()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You find yourself in detention after school.")
                                    .SudoTeacher("I hope you'll use this time to reflect on your actions.")
                                    .SudoUser("(thinking) This isn't how I planned to spend my afternoon...")
                                    .Say("You look around the quiet classroom, considering your options.")
                                    .Init();
}
