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
        MoneyFarmRoom,
        ThisIsRoomWithPassword,
        YouFoundLetterHere,
        PayForLetterHere,
        WARNINGthisRoomIsToxic,
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
                MessageType.MoneyFarmRoom => "Status - every second, it farm more money! Thank you, rabbit boy.",
                MessageType.ThisIsRoomWithPassword => "Status - It's password room. You should found all 4 letters.",
                MessageType.PayForLetterHere => "Status - Pay For Letter Here.",
                MessageType.YouFoundLetterHere => $"Status - You found letter here. You have more {Game.Instance.PasswordStorage.ShouldRevealCount()} to reveal.",
                MessageType.WARNINGthisRoomIsToxic => "Warning - this room is TOXIC!",
                _ => throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null)
            };
        }
    }
}