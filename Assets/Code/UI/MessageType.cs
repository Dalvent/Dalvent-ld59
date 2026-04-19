using System;

namespace Code
{
    public enum MessageType
    {
        Ok,
        UnknownRoom,
        EmptyRoom,
        ItsYou,
        ItsYourTarget,
        WarningMonsterHere,
    }

    public static class MessageTypeExtensions
    {
        public static string GetMessage(this MessageType messageType)
        {
            return messageType switch
            {
                MessageType.Ok => "Status - Ok",
                MessageType.UnknownRoom => "Status - Unknown Room. Connect this area with your drone.",
                MessageType.EmptyRoom => "Status - Empty Room. It's nothing here.",
                MessageType.WarningMonsterHere => "WARNING - Monster is here!",
                MessageType.ItsYou => "Status - It's you.",
                MessageType.ItsYourTarget => "Status - collect all password letters and bring your drone here to escape!",
                _ => throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null)
            };
        }
    }
}