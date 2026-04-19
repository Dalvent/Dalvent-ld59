using System;

namespace Code.GameMap.FogRooms
{
    public class ToxicFogRoom : FogRoom
    {
        public override void OnStep(DronRoomMover dronRoomMover)
        {
            var dron = dronRoomMover.GetComponent<Dron>();
            dron.En = 1;
        }

        public override MessageType GetStatus()
        {
            return MessageType.WARNINGthisRoomIsToxic;
        }

        public override SendAction[] GetSpecialActions()
        {
            return Array.Empty<SendAction>();
        }
    }
}