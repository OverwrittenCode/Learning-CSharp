namespace AdventureGames.Scenes.GoToSchool.Explore;

// New class for School Exploration scene
public sealed class SchoolExploration : BaseScene
{
    public SchoolExploration() { }

    public override void Play()
    {
        new ConversationBuilder().Say("You decide to skip class and explore the school.").Init();
    }
}
