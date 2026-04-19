using UnityEngine;

namespace Code.GameMap.FogRooms
{
    public abstract class FogRoom : MonoBehaviour
    {
        public virtual void OnConnected(BattleFogRoom battleFogRoom) { }
        public virtual void OnStep(DronRoomMover dronRoomMover) { }
        public abstract MessageType GetStatus();
        public abstract SendAction[] GetSpecialActions();
    }
}