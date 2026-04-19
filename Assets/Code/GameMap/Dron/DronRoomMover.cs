using System;

namespace Code.GameMap
{
    using UnityEngine;
    using PrimeTween;

    public class DronRoomMover : MonoBehaviour
    {
        public event Action CanMoveUpdated;
        public event Action StopMove;
        public event Action<DronRoomMover> Moved;

        private Room _currentRoom;
        public Room CurrentRoom
        {
            get => _currentRoom;
            private set
            {
                if (_currentRoom == value)
                    return;
                
                _currentRoom = value;
                
                if (_currentRoom is BattleFogRoom battleFogRoom)
                    battleFogRoom.Connected.OnStep(this);
                Moved?.Invoke(this);
            }
        }

        private bool _canMove;
        public bool CanMove
        {
            get => _canMove;
            private set
            {
                if (_canMove == value)
                    return;

                _canMove = value;
                CanMoveUpdated?.Invoke();
            }
        }
        
        private Tween _moveTween;

        public void TeleportTo(Room room)
        {
            StopMoving();

            CurrentRoom = room;
            transform.position = room.transform.position;
        }

        public void MoveTo(Room room)
        {
            StopMoving();
            Vector3 target = room.transform.position;
            CanMove = false;
            CurrentRoom = room;

            _moveTween = Tween.Position(
                    transform,
                    target,
                    duration: Game.Instance.DronStats.MoveTime,
                    ease: Ease.Linear
                )
                .OnComplete(() =>
                {
                    CanMove = room != null;
                    StopMove?.Invoke();
                    
                    if (CurrentRoom is PasswordRoom)
                        Game.Instance.WinTransitor.ShowEnd();
                });
        }

        private void StopMoving()
        {
            if (_moveTween.isAlive)
            {
                _moveTween.Stop();
            }
        }

        public void OnDronMoved()
        {
            throw new NotImplementedException();
        }
    }
}