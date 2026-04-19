using System;

namespace Code.GameMap.FogRooms
{
    public class EmptyFogRoom : FogRoom
    {
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