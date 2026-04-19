using System;

namespace Code.GameMap.FogRooms
{
    public class EmptyFogRoom : FogRoom
    {
        public const float MoneyScaler = 1.2f;
        public static int MoneyPerNewRoom = 3;
        
        public override void OnConnected(BattleFogRoom battleFogRoom)
        {
            Game.Instance.MoneyStorage.AddMoney(20);
            MoneyPerNewRoom = (int)(MoneyPerNewRoom * MoneyScaler);
        }

        public override MessageType GetStatus()
        {
            return MessageType.EmptyRoom;
        }

        public override SendAction[] GetSpecialActions()
        {
            return Array.Empty<SendAction>();
        }
    }
}