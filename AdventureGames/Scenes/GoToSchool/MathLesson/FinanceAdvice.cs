namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class FinanceAdvice : BaseScene
{
    public FinanceAdvice()
    {
        Choices.Add(new("Thank the teacher and leave", () => new ClassDismissal()));
        Choices.Add(new("Ask more questions", () => new DeepLearning()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("How can we protect our personal finances during this crisis?")
            .SudoTeacher("That's a very practical question. Here are some strategies...")
            .Say("The teacher provides advice on budgeting, saving, and financial planning.")
            .Init();
    }
}
