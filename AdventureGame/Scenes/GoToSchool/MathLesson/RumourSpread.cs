using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class RumourSpread : BaseScene
{
    public RumourSpread()
    {
        Choices.Add(new("Organize a student meeting", () => new StudentResistance()));
        Choices.Add(new("Confront the teacher", () => new TeacherConfrontation()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You decide to share what you've heard with your classmates.")
                                    .Say("The information spreads quickly, and soon the whole class is buzzing with whispers.")
                                    .SudoTeacher("What's going on here? Why is everyone talking?")
                                    .Say("The atmosphere in the classroom becomes tense.")
                                    .SudoUser("(thinking) Maybe I shouldn't have said anything...")
                                    .Init();
}
