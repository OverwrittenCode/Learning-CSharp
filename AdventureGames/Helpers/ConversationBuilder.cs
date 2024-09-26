using AdventureGames.Entities.Humanoids;

namespace AdventureGames;

internal sealed class ConversationBuilder
{
    private readonly List<Action> _actions = [];

    /// <summary>
    /// Adds an action to the conversation sequence.
    /// </summary>
    /// <param name="action">The action to be performed.</param>
    public ConversationBuilder Perform(Action action)
    {
        _actions.Add(action.Invoke);
        return this;
    }

    /// <inheritdoc cref="Utils.ContinueOnEnter"/>
    public ConversationBuilder Pause() => Perform(Utils.ContinueOnEnter);

    /// <inheritdoc cref="Game.Say(String)"/>
    public ConversationBuilder Say(string message) => Perform(() => Game.Say(message));

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    /// <param name="humanoid">The speaker</param>
    public ConversationBuilder Sudo(HumanoidBase humanoid, params string[] message) =>
        Perform(() => humanoid.Sudo(message));

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    public ConversationBuilder SudoUser(params string[] messages) => Sudo(Game.User, messages);

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    public ConversationBuilder SudoFather(params string[] messages) => Sudo(Game.Father, messages);

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    public ConversationBuilder SudoMother(params string[] messages) => Sudo(Game.Mother, messages);

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    public ConversationBuilder SudoJohn(params string[] messages) => Sudo(Game.John, messages);

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    public ConversationBuilder SudoTeacher(params string[] messages) =>
        Sudo(Game.Teacher, messages);

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    public ConversationBuilder SudoJack(params string[] messages) => Sudo(Game.Jack, messages);

    /// <inheritdoc cref="HumanoidBase.Sudo(global::System.String[])"/>
    public ConversationBuilder SudoPrinciple(params string[] messages) =>
        Sudo(Game.Principle, messages);

    /// <summary>
    /// Iterates over the conversation sequence
    /// </summary>
    public void Init()
    {
        foreach (Action action in _actions)
        {
            action.Invoke();
        }
    }
}
