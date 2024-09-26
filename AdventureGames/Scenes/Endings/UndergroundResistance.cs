namespace AdventureGames.Scenes.Endings;

internal sealed class UndergroundResistance : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You decide to join the resistance.")
            .Say(
                "Over the next few weeks, you become an integral part of the underground movement."
            )
            .Say("Your knowledge and bravery help expose government corruption.")
            .Say("Though the future is uncertain, you feel you've made the right choice.")
            .SudoUser("We may not change the world overnight, but we're making a difference.")
            .Say("THE END - Resistance Fighter Ending")
            .Init();
    }
}
