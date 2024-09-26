using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class DeviceActivation : BaseScene
{
    public DeviceActivation()
    {
        Choices.Add(new("Join the resistance", () => new UndergroundResistance()));
        Choices.Add(new("Shut down the device", () => new DeviceShutdown()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("With trembling hands, you turn the dials on the device.")
            .Say("Suddenly, it crackles to life, emitting a series of beeps and static.")
            .Say("A voice comes through: 'This is the resistance. Identify yourself.'")
            .SudoUser("I... I'm the child of...")
            .Say("Before you can finish, you hear your parents rushing down the stairs.")
            .SudoMother("What have you done?!")
            .Init();
    }
}
