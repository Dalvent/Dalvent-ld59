using System;

namespace Code.GameMap.FogRooms
{
    public class PasswordFogRoom : FogRoom
    {
        private char _letter;

        public override void OnConnected(BattleFogRoom battleFogRoom)
        {
            _letter = Game.Instance.PasswordStorage.RevealFirst();
            Game.Instance.MessagePopup.Open($"You found letter {_letter}");
        }

        public override MessageType GetStatus()
        {
            return MessageType.YouFoundLetterHere;
        }

        public override SendAction[] GetSpecialActions()
        {
            return Array.Empty<SendAction>();
        }
    }
}