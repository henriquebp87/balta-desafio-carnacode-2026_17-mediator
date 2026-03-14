namespace DesignPatternChallenge_Iterator.Components;

internal class ChatGroup(string groupName)
{
    private readonly List<ChatUser> _members = [];
    private readonly List<ChatUser> _mutedMembers = [];

    public string GroupName { get; set; } = groupName;

    public void AddMember(ChatUser user)
    {
        _members.Add(user);
    }

    public void RemoveMember(ChatUser user)
    {
        _members.Remove(user);
    }

    public void MuteMember(ChatUser user)
    {
        _mutedMembers.Add(user);
    }

    public void UnmuteMember(ChatUser user)
    {
        _mutedMembers.Remove(user);
    }

    public IEnumerable<ChatUser> GetMembers()
    {
        return _members;
    }

    public bool ContainsMember(ChatUser user)
    {
        return _members.Contains(user);
    }

    public bool IsUserMuted(ChatUser user)
    {
        return _mutedMembers.Contains(user);
    }

    public override string ToString()
    {
        return GroupName;
    }
}