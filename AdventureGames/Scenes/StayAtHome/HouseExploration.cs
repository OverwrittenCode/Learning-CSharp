namespace AdventureGames.Scenes.StayAtHome;

internal sealed class HouseExploration : BaseScene
{
    public HouseExploration()
    {
        Choices.Add(new("Investigate the attic", () => new AtticDiscovery()));
        Choices.Add(new("Search your parents' room", () => new ParentsRoomSearch()));
        Choices.Add(new("Explore the basement", () => new BasementExploration()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You decide to explore the house while your parents are distracted.")
            .Say("As you wander, you notice things you've never paid attention to before.")
            .Say("A loose floorboard in your room... A locked drawer in your father's study...")
            .SudoUser("What secrets is this house hiding?")
            .Say("You hear your mother's muffled voice from downstairs.")
            .Init();
    }
}
