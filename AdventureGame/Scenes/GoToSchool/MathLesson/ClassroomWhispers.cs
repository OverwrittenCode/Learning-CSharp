using AdventureGame.Helpers;
using AdventureGame.Scenes.StayAtHome;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class ClassroomWhispers : BaseScene
{
    public ClassroomWhispers()
    {
        Choices.Add(new("Share what you've heard", () => new RumourSpread()));
        Choices.Add(new("Keep the information to yourself", () => new SecretKeeper()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You lean over to whisper to your classmate.")
                                    .SudoUser("Psst... have you noticed anything weird going on?")
                                    .Say("Your classmate looks around nervously before replying.")
                                    .Say("Classmate: (whispering) My parents said something about a secret meeting in the school basement...")
                                    .Say("The teacher's voice interrupts your conversation.")
                                    .SudoTeacher("Is there something you'd like to share with the class?")
                                    .Init();
}
