using DesignPatternChallenge_Iterator.Components;

namespace DesignPatternChallenge_Iterator.Mediator;

internal class ChatMediator : IChatMediator
{
    public void JoinGroup(ChatUser sender, ChatGroup group)
    {
        if (sender == null || group == null)
        {
            throw new ArgumentNullException("Sender and group cannot be null.");
        }

        if (sender.IsMemberOf(group))
        {
            Console.WriteLine($"[{sender}] já é membro do grupo '{group}'");
            return;
        }

        group.AddMember(sender);

        foreach (var member in group.GetMembers())
        {
            if (member != sender)
            {
                member.ReceiveNotification($"[{sender}] entrou no grupo '{group}'");
            }
        }
    }

    public void LeaveGroup(ChatUser sender, ChatGroup group)
    {
        if (sender == null || group == null)
        {
            throw new ArgumentNullException("Sender and group cannot be null.");
        }

        if (!sender.IsMemberOf(group))
        {
            Console.WriteLine($"[{sender}] ❌ não é membro do grupo '{group}'");
            return;
        }

        group.RemoveMember(sender);

        foreach (var member in group.GetMembers())
        {
            member.ReceiveNotification($"[{sender}] saiu do grupo '{group}'");
        }
    }

    public void SendMessage(ChatUser sender, ChatGroup group, string message)
    {
        if (sender == null || group == null)
        {
            throw new ArgumentNullException("Sender and group cannot be null.");
        }

        if (!sender.IsMemberOf(group))
        {
            Console.WriteLine($"[{sender}] ❌ não é membro do grupo '{group}' e não pode enviar mensagens");
            return;
        }

        if (group.IsUserMuted(sender))
        {
            Console.WriteLine($"[{sender}] ❌ Você está mutado e não pode enviar mensagens para este grupo.");
            return;
        }

        Console.WriteLine($"[{sender}] Enviou para o grupo {group}: {message}");

        foreach (var member in group.GetMembers())
        {
            if (member != sender)
            {
                member.ReceiveMessage(sender.Name, message);
            }
        }
    }

    public void SendPrivateMessage(ChatUser sender, ChatUser recipient, string message)
    {
        if (sender == null || recipient == null)
        {
            throw new ArgumentNullException("Sender and recipient cannot be null.");
        }

        Console.WriteLine($"[{sender}] Enviou mensagem privada para {recipient}");

        recipient.ReceivePrivateMessage(sender.Name, message);
    }

    public void MuteUser(ChatUser sender, ChatUser target, ChatGroup group)
    {
        if (sender == null || target == null || group == null)
        {
            throw new ArgumentNullException("Sender, target, and group cannot be null.");
        }

        if (!sender.IsMemberOf(group) || !target.IsMemberOf(group))
        {
            Console.WriteLine($"Ambos os usuários devem ser membros do grupo '{group}' para aplicar muting.");
            return;
        }

        if (group.IsUserMuted(target))
        {
            Console.WriteLine($"[{target}] já está mutado no grupo '{group}'");
            return;
        }

        group.MuteMember(target);

        foreach (var member in group.GetMembers())
        {
            member.ReceiveNotification($"[{target}] foi silenciado no grupo '{group}' por [{sender}]");
        }
    }

    public void UnmuteUser(ChatUser sender, ChatUser target, ChatGroup group)
    {
        if (sender == null || target == null || group == null)
        {
            throw new ArgumentNullException("Sender, target, and group cannot be null.");
        }

        if (!sender.IsMemberOf(group) || !target.IsMemberOf(group))
        {
            Console.WriteLine($"Ambos os usuários devem ser membros do grupo '{group}' para aplicar unmuting.");
            return;
        }

        if (!group.IsUserMuted(target))
        {
            Console.WriteLine($"[{target}] não está mutado no grupo '{group}'");
            return;
        }

        group.UnmuteMember(target);

        foreach (var member in group.GetMembers())
        {
            member.ReceiveNotification($"[{target}] foi desmutado no grupo '{group}' por [{sender}]");
        }
    }
}