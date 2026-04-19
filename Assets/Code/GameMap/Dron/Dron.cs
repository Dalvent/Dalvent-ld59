using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace Code.GameMap
{
    [RequireComponent(typeof(DronRoomMover))]
    public class Dron : MonoBehaviour
    {
        public GameMapSelectable GameMapSelectable;
        private DronRoomMover _dronRoomMover;
        
        private void Awake()
        {
            _dronRoomMover = GetComponent<DronRoomMover>();
        }

        private void OnEnable()
        {
            _dronRoomMover.CanMoveUpdated += OnCanMoveUpdated;
            GameMapSelectable.Selected += OnSelected;
            GameMapSelectable.Unselected += OnUnselected;
        }

        private void OnDisable()
        {
            _dronRoomMover.CanMoveUpdated -= OnCanMoveUpdated;
            GameMapSelectable.Selected -= OnSelected;
            GameMapSelectable.Unselected -= OnUnselected;
        }

        private void OnCanMoveUpdated()
        {
            if (GameMapSelectable.IsSelected)
                OnSelected();
        }

        private void OnSelected()
        {
            if (_dronRoomMover.CurrentRoom == null || !_dronRoomMover.CanMove)
            {
                Game.Instance.SendButtonsPanel.ClearAll();
                return;
            }

            var groupDoorByDirection = Game.Instance.GameField.GetDoors(_dronRoomMover.CurrentRoom)
                .Where(d => d.CanUse && Game.Instance.GameField.IsVisible(d))
                .GroupBy(d => d.GetDirection(_dronRoomMover.CurrentRoom))
                .OrderBy(d => d);
            
            var actions = new List<SendAction>();
            
            foreach (var doorGroup in groupDoorByDirection)
            {
                var nameDirection = doorGroup.Key switch
                {
                    DirectionToInfo.Right => "Move right",
                    DirectionToInfo.Left => "Move left",
                    DirectionToInfo.Top => "Move top",
                    DirectionToInfo.Down => "Move down",
                    _ => throw new ArgumentOutOfRangeException()
                };
                Room secondRoom = doorGroup.First().SecondRoom(_dronRoomMover.CurrentRoom);
                actions.Add(new SendAction(nameDirection, () => _dronRoomMover.MoveTo(secondRoom)));
            }
            Game.Instance.SendButtonsPanel.SetActions(new [] { new SendAction("Connect room", OnConnectRoom) });
            Game.Instance.SendButtonsPanel.SetActions(actions);
        }

        private void OnConnectRoom()
        {
        }

        private void OnUnselected()
        {
            Game.Instance.SendButtonsPanel.ClearAll();
        }

        private void OnWritingPassword()
        {
        }
    }
}