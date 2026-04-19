using UnityEngine;

namespace Code.GameMap.FogRooms
{
    public abstract class FogRoom : MonoBehaviour
    {
        public abstract MessageType GetStatus();
        public abstract SendAction[] GetSpecialActions();
    }
}