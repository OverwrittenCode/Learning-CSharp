using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class StudentResistance : BaseScene
{
    public StudentResistance()
    {
        Choices.Add(new("Form a secret student group", () => new UndergroundResistance()));
        Choices.Add(new("Organize a peaceful protest", () => new SchoolProtest()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You gather a group of trusted classmates after school.")
            .SudoUser("We can't just sit by and do nothing. We need to take action.")
            .Say("Your classmates nod in agreement, a mix of excitement and fear in their eyes.")
            .SudoJack("What exactly are you proposing we do?")
            .Init();
    }
}
