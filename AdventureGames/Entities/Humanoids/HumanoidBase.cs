using Common.Utils;

namespace AdventureGames.Entities.Humanoids;

/// <summary>
/// <para>
/// <b><see cref="HumanoidBase"/></b>
/// Provides a base structure for all humanoid characters in the adventure game.
/// </para>
/// </summary>
public abstract class HumanoidBase
{
    private const int MaxNameLength = 20;

    public string Name { get; }
    public string Description { get; }

    /// <summary>
    /// The foreground colour used for the Humanoid when using <see cref="Sudo(global::System.String[])"/>.
    /// </summary>
    public ConsoleColor DialogueColour { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="name">The name of the humanoid</param>
    /// <param name="description">A description of the humanoid</param>
    /// <param name="dialogueColour">The foreground colour used when invoking <see cref="Sudo(global::System.String[])"/></param>
    protected HumanoidBase(string name, string description, ConsoleColor dialogueColour)
    {
        Name = name.Length > MaxNameLength ? name[..MaxNameLength] : name;
        Description = description;
        DialogueColour = dialogueColour;
    }

    /// <summary>
    /// <inheritdoc cref="Utils.TypewriterEffect(String)"/>
    /// The foreground colour is set to <see cref="DialogueColour"/>.
    /// </summary>
    /// <param name="messages">
    /// The messages of the speaker. Each message is placed on a new line with padding.
    /// </param>
    public void Sudo(params string[] messages) => Sudo(1, messages);

    /// <inheritdoc cref="Sudo(global::System.String[])"/>
    /// <inheritdoc cref="Game.Say(String, Double)" path="/param[@name='speed']"/>
    public void Sudo(double speed, params string[] messages)
    {
        var prefix = $"[{Name}]: ";
        var delimeter = $"\n{new string(' ', prefix.Length)}";

        ConsoleUtils.HighlightConsoleLine(
            prefix + String.Join(delimeter, messages),
            DialogueColour,
            (string message) => Utils.TypewriterEffect(message, speed)
        );
    }
}
