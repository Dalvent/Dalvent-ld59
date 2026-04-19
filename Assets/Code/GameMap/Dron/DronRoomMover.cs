using System;

namespace Code.GameMap
{
    using UnityEngine;
    using PrimeTween;

    public class DronRoomMover : MonoBehaviour
    {
        public event Action CanMoveUpdated;

        public Room CurrentRoom { get; private set; }
        
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

            _moveTween = Tween.Position(
                    transform,
                    target,
                    duration: Game.Instance.DronStats.MoveTime,
                    ease: Ease.Linear
                )
                .OnComplete(() =>
                {
                    CurrentRoom = room;
                    CanMove = room != null;
                });
        }

        private void StopMoving()
        {
            if (_moveTween.isAlive)
            {
                _moveTween.Stop();
            }
        }
    }
}