using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.GameMap
{
    public enum DirectionToInfo
    {
        Right,
        Left,
        Top,
        Down
    }
    
    public class DoorData : MonoBehaviour
    {
        public bool CanUse;
        public bool CanClose;
        public bool IsClosed;
        public Room From;
        public Room To;

        public Room SecondRoom(Room first)
        {
            if (first == From)
                return To;
            
            if (first == To)
                return From;

            throw new ArgumentException("Argument not in DoorData!");
        }
        
        public IEnumerable<Room> EnumerateRooms()
        {
            yield return From;
            yield return To;
        }
        
        public DirectionToInfo GetDirection(Room room)
        {
            if (room != From && room != To)
                throw new ArgumentException("Room is not connected to this door", nameof(room));

            var fromRect = From.transform as RectTransform;
            var toRect = To.transform as RectTransform;

            if (fromRect == null || toRect == null)
                throw new Exception("Rooms must have RectTransform");

            Vector3 fromPos = fromRect.position;
            Vector3 toPos = toRect.position;

            Vector3 dir = (room == From)
                ? (toPos - fromPos)
                : (fromPos - toPos);

            float dx = dir.x;
            float dy = dir.y;

            if (Mathf.Abs(dy) > Mathf.Abs(dx))
            {
                return dy > 0 ? DirectionToInfo.Top : DirectionToInfo.Down;
            }
            else
            {
                return dx > 0 ? DirectionToInfo.Right : DirectionToInfo.Left;
            }
        }
    }
}