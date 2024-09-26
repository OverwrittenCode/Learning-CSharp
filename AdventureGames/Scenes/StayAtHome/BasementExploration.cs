namespace AdventureGames.Scenes.StayAtHome;

internal sealed class BasementExploration : BaseScene
{
    public BasementExploration()
    {
        Choices.Add(new("Investigate the strange device", () => new MysteriousDevice()));
        Choices.Add(new("Look through old documents", () => new HiddenDocuments()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You cautiously descend into the dimly lit basement.")
            .Say("Dust motes dance in the air as you look around.")
            .Say("In the corner, you spot a strange device covered with a sheet.")
            .Say("On a nearby shelf, you see a stack of old, yellowed documents.")
            .SudoUser("I've never noticed these before...")
            .Init();
    }
}
