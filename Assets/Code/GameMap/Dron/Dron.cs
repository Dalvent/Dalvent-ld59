using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.GameMap
{
    [RequireComponent(typeof(DronRoomMover))]
    public class Dron : MonoBehaviour
    {
        public GameMapSelectable GameMapSelectable;
        private DronRoomMover _dronRoomMover;
        public Slider ProgressBar;
        public Image ConnectProgress;

        public Image DronImage;
        public Sprite DeadImage;
        private bool _isDead;

        public bool IsConnecting { get; private set; }
        
        private bool _isFirst = false;
        public int En;
        private int _maxEn;
        
        private void Awake()
        {
            _dronRoomMover = GetComponent<DronRoomMover>();
            En = Game.Instance.DronStats.MaxEnergy;
            _maxEn = Game.Instance.DronStats.MaxEnergy;
            ProgressBar.value = 1;
            ConnectProgress.fillAmount = 0;
        }

        private void OnEnable()
        {
            _dronRoomMover.CanMoveUpdated += OnCanMoveUpdated;
            _dronRoomMover.StopMove += OnStopMove;
            GameMapSelectable.Selected += OnSelected;
            GameMapSelectable.Unselected += OnUnselected;
        }

        private void OnDisable()
        {
            _dronRoomMover.CanMoveUpdated -= OnCanMoveUpdated;
            _dronRoomMover.StopMove -= OnStopMove;
            GameMapSelectable.Selected -= OnSelected;
            GameMapSelectable.Unselected -= OnUnselected;
        }

        private void OnStopMove()
        {
            if (!_isFirst)
            {
                _isFirst = true;
                return;
            }

            En--;
            float progressBarValue = En / (float)_maxEn;
            ProgressBar.value = progressBarValue;

            if (progressBarValue <= 0)
                StartCoroutine(DelayedDie());
        }

        private IEnumerator DelayedDie()
        {
            _isDead = true;
            DronImage.sprite = DeadImage;
            Game.Instance.DronsManager.RemoveDrone(_dronRoomMover);
            OnSelected();
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        } 

        private void OnCanMoveUpdated()
        {
            if (GameMapSelectable.IsSelected)
                OnSelected();
        }

        private void OnSelected()
        {
            if (_isDead || IsConnecting)
            {
                Game.Instance.SendButtonsPanel.ClearAll();
                return;
            }
            
            if (_dronRoomMover.CurrentRoom == null || !_dronRoomMover.CanMove)
            {
                Game.Instance.SendButtonsPanel.ClearAll();
                return;
            }

            var groupDoorByDirection = Game.Instance.GameField.GetDoors(_dronRoomMover.CurrentRoom)
                .Where(d => !d.CanNotUse && Game.Instance.GameField.IsVisible(d) && !d.IsClosedDoor)
                .GroupBy(d => d.GetDirection(_dronRoomMover.CurrentRoom))
                .OrderBy(d => d.Key);
            
            var actions = new List<SendAction>();
            
            foreach (var doorGroup in groupDoorByDirection)
            {
                var secondRoom = doorGroup.First().SecondRoom(_dronRoomMover.CurrentRoom);
                if (!Game.Instance.DronsManager.IsRoomWithoutDron(secondRoom))
                    continue;
                
                var nameDirection = doorGroup.Key switch
                {
                    DirectionToInfo.Right => "Move right",
                    DirectionToInfo.Left => "Move left",
                    DirectionToInfo.Top => "Move top",
                    DirectionToInfo.Down => "Move down",
                    _ => throw new ArgumentOutOfRangeException()
                };
                actions.Add(new SendAction(nameDirection, () => _dronRoomMover.MoveTo(secondRoom)));
            }
            if (_dronRoomMover.CurrentRoom is BattleFogRoom { IsConnected: false })
                actions.Add(new SendAction("Connect room", OnConnectRoom));
            Game.Instance.SendButtonsPanel.SetActions(actions);
        }

        private void OnConnectRoom()
        {
            StartCoroutine(ConnectingCoroutine(Game.Instance.DronStats.ScanTime));
            
            if (GameMapSelectable.IsSelected)
                OnSelected();
        }

        private IEnumerator ConnectingCoroutine(float duration)
        {
            IsConnecting = true;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = elapsed / duration;
                ConnectProgress.fillAmount = 1f - t;

                yield return null;
            }

            ConnectProgress.fillAmount = 0f;
            Game.Instance.GameField.ConnectRoom(_dronRoomMover.CurrentRoom);
            IsConnecting = false;
            
            if (GameMapSelectable.IsSelected)
                OnSelected();
        }

        private void OnUnselected()
        {
            Game.Instance.SendButtonsPanel.ClearAll();
        }
    }
}