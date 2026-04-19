using System;
using System.Collections.Generic;
using System.Linq;
using Code.GameMap;
using UnityEngine;

public class GameField : MonoBehaviour
{
    private Room[] _rooms;
    private Dictionary<Room, List<DoorData>> _doorsByRoom;

    public IReadOnlyList<DoorData> GetDoors(Room room) => _doorsByRoom[room];

    private void Awake()
    {
        _rooms = GetComponentsInChildren<Room>();
        var doors = GetComponentsInChildren<DoorData>();
        _doorsByRoom = DoorUtils.BuildRoomDoorsMap(doors);

        foreach (var room in _rooms)
        {
            if (ShouldShowAtStart(room))
                continue;
            
            room.gameObject.SetActive(false);
        }

        foreach (var door in doors)
        {
            try
            {
                if (door.EnumerateRooms().All(s => s.gameObject.activeSelf))
                    continue;

                door.gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                throw new Exception($"{door!.gameObject.name} is broken!");
            }
        }
    }

    private bool ShouldShowAtStart(Room room)
    {
        if (IsStartRoom(room))
            return true;

        if (_doorsByRoom.TryGetValue(room, out var doorDatas))
            return doorDatas.SelectMany(d => d.EnumerateRooms()).Any(IsStartRoom);

        return false;
    }
    
    private static bool IsStartRoom(Room room) => room is YouRoom or PasswordRoom or DroneFactoryRoom;

    public bool IsVisible(DoorData doorData)
    {
        return doorData.From.gameObject.activeSelf && doorData.To.gameObject.activeSelf;
    }

    public void ConnectRoom(Room currentRoom)
    {
        if (currentRoom is not BattleFogRoom battleFogRoom)
            return;
        
        battleFogRoom.MakeConnected();
        var doors = _doorsByRoom[battleFogRoom];

        foreach (var door in doors)
        {
            door.gameObject.SetActive(true);
            door.SecondRoom(battleFogRoom).gameObject.SetActive(true);
        }
    }
}
