using System.Collections.Generic;

namespace Code.GameMap
{
    public static class DoorUtils
    {
        public static Dictionary<Room, List<DoorData>> BuildRoomDoorsMap(DoorData[] doors)
        {
            var map = new Dictionary<Room, List<DoorData>>();

            foreach (var door in doors)
            {
                if (door == null)
                    continue;

                Add(map, door.From, door);
                Add(map, door.To, door);
            }

            return map;
        }

        private static void Add(Dictionary<Room, List<DoorData>> map, Room room, DoorData door)
        {
            if (room == null)
                return;

            if (!map.TryGetValue(room, out var list))
            {
                list = new List<DoorData>();
                map[room] = list;
            }

            list.Add(door);
        }
    }
}