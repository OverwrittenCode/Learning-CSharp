namespace AdventureGames.Scenes.StayAtHome;

internal sealed class ParentsRoomSearch : BaseScene
{
    public ParentsRoomSearch()
    {
        Choices.Add(new("Confront your parents", () => new FamilyConfrontation()));
        Choices.Add(new("Keep searching in secret", () => new DeepInvestigation()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You carefully enter your parents' room, heart pounding.")
            .Say("In their dresser, you find a hidden compartment.")
            .Say("Inside, there are coded messages and a map of the city with marked locations.")
            .SudoUser("What is all this? Are my parents involved in something dangerous?")
            .Say("You hear footsteps approaching the room.")
            .Init();
    }
}
