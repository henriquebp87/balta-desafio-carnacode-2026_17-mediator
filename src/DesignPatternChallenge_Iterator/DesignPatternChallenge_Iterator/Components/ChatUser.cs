using DesignPatternChallenge_Iterator.Mediator;

namespace DesignPatternChallenge_Iterator.Components;

internal class ChatUser(string name, IChatMediator mediator) : IEquatable<ChatUser>
{
    private readonly IChatMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public string Name { get; set; } = name;

    public void JoinGroup(ChatGroup group)
    {
        _mediator.JoinGroup(this, group);
    }

    public void SendMessage(string message, ChatGroup group)
    {
        _mediator.SendMessage(this, group, message);
    }

    public void SendPrivateMessage(ChatUser recipient, string message)
    {
        _mediator.SendPrivateMessage(this, recipient, message);
    }

    public void LeaveGroup(ChatGroup group)
    {
        _mediator.LeaveGroup(this, group);
    }

    public void MuteUser(ChatUser target, ChatGroup group)
    {
        _mediator.MuteUser(this, target, group);
    }

    public void UnmuteUser(ChatUser target, ChatGroup group)
    {
        _mediator.UnmuteUser(this, target, group);
    }

    public void ReceiveMessage(string senderName, string message)
    {
        Console.WriteLine($"  → [{Name}] Recebeu de {senderName}: {message}");
    }

    public void ReceivePrivateMessage(string senderName, string message)
    {
        Console.WriteLine($"  → [{Name}] 🔒 Mensagem privada de {senderName}: {message}");
    }

    public void ReceiveNotification(string notification)
    {
        Console.WriteLine($"  → [{Name}] ℹ️ {notification}");
    }

    public bool IsMemberOf(ChatGroup group)
    {
        return group.ContainsMember(this);
    }

    public bool Equals(ChatUser? other)
    {
        return Name == other?.Name;
    }

    public override string ToString()
    {
        return Name;
    }
}