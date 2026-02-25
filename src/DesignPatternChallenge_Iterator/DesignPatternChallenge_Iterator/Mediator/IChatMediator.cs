using DesignPatternChallenge_Iterator.Components;

namespace DesignPatternChallenge_Iterator.Mediator
{
    internal interface IChatMediator
    {
        void JoinGroup(ChatUser sender, ChatGroup group);

        void LeaveGroup(ChatUser sender, ChatGroup group);

        void SendMessage(ChatUser sender, ChatGroup group, string message);

        void SendPrivateMessage(ChatUser sender, ChatUser recipient, string message);

        void MuteUser(ChatUser sender, ChatUser target, ChatGroup group);

        void UnmuteUser(ChatUser sender, ChatUser target, ChatGroup group);
    }
}
