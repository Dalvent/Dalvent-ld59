using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Code.GameMap
{
    [CustomEditor(typeof(DoorData))]
    public class DoorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DoorData door = (DoorData)target;

            if (GUILayout.Button("Find Rooms"))
            {
                Undo.RecordObject(door, "Find Door Rooms");
                AssignRooms(door);
                EditorUtility.SetDirty(door);
            }
        }

        private static void AssignRooms(DoorData door)
        {
            RectTransform doorRect = door.transform as RectTransform;
            if (doorRect == null)
            {
                Debug.LogError("Door must have RectTransform", door);
                return;
            }

            Room[] rooms = Object.FindObjectsOfType<Room>(true);
            List<Room> foundRooms = new();

            Rect worldRect = GetWorldRect(doorRect);
            Vector3 center = worldRect.center;

            bool isHorizontal = worldRect.width > worldRect.height;

            Vector3 offset;
            if (isHorizontal)
            {
                float halfWidth = worldRect.width * 0.5f;
                offset = Vector3.right * halfWidth;
            }
            else
            {
                float halfHeight = worldRect.height * 0.5f;
                offset = Vector3.up * halfHeight;
            }

            // Чуть выходим за пределы двери, чтобы попасть в комнаты по обе стороны
            offset *= 1.1f;

            Vector3 pointA = center - offset;
            Vector3 pointB = center + offset;

            TryAddRoom(pointA, rooms, foundRooms);
            TryAddRoom(pointB, rooms, foundRooms);

            door.From = null;
            door.To = null;

            if (foundRooms.Count == 0)
            {
                Debug.LogWarning("No rooms found", door);
                return;
            }

            if (foundRooms.Count > 2)
            {
                Debug.LogError("More than 2 rooms found", door);
                return;
            }

            door.From = foundRooms[0];

            if (foundRooms.Count == 2)
                door.To = foundRooms[1];
        }

        private static void TryAddRoom(Vector3 point, Room[] rooms, List<Room> result)
        {
            foreach (Room room in rooms)
            {
                RectTransform roomRect = room.transform as RectTransform;
                if (roomRect == null)
                    continue;

                if (Contains(roomRect, point) && !result.Contains(room))
                    result.Add(room);
            }
        }

        private static bool Contains(RectTransform rectTransform, Vector3 point)
        {
            Rect rect = GetWorldRect(rectTransform);
            return rect.Contains(point);
        }

        private static Rect GetWorldRect(RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Vector3 bottomLeft = corners[0];
            Vector3 topRight = corners[2];

            return new Rect(
                bottomLeft.x,
                bottomLeft.y,
                topRight.x - bottomLeft.x,
                topRight.y - bottomLeft.y
            );
        }
    }
}