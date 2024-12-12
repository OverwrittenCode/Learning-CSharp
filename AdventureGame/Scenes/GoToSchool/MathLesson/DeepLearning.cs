using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class DeepLearning : BaseScene
{
    public DeepLearning()
    {
        Choices.Add(new("Ask more questions", () => new FurtherDiscussion()));
        Choices.Add(new("Thank the teacher and end the conversation", () => new ClassDismissal()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You delve deeper into the economic concepts discussed in class.")
                                    .SudoTeacher("I'm impressed by your interest. Let me explain further...")
                                    .Say("The teacher goes into more detail about the economic situation.")
                                    .SudoUser("(thinking) This is more complex than I thought...")
                                    .Init();
}
