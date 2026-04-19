using System;
using System.Collections.Generic;
using System.Linq;
using Code.GameMap;
using UnityEngine;

public class GameField : MonoBehaviour
{
    private Room[] _rooms;
    private Dictionary<Room, List<DoorData>> _doorsByRoom;
    private List<DronRoomMover> _drons = new();

    public IReadOnlyList<DoorData> GetDoors(Room room) => _doorsByRoom[room];

    public void AddDron(DronRoomMover dron) => _drons.Add(dron);

    public void RemoveDrone(DronRoomMover dron) => _drons.Remove(dron);
    
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

    public bool IsRoomWithoutDron(Room room) => _drons.Any(d => d.CurrentRoom == room);
    
    private static bool IsStartRoom(Room room) => room is YouRoom or PasswordRoom or DroneFactoryRoom;

    public bool IsVisible(DoorData doorData)
    {
        return doorData.From.gameObject.activeSelf && doorData.To.gameObject.activeSelf;
    }
}
