using System;
using UnityEngine;

namespace Code.GameMap.FogRooms
{
    public class PasswordFogMoneyRoom : FogRoom
    {
        public int Money = 100;
        private BattleFogRoom _battleFogRoom;
        private bool _isReviled = false;
        public GameObject Revel;

        public override void OnConnected(BattleFogRoom battleFogRoom)
        {
            _battleFogRoom = battleFogRoom;
            Game.Instance.MoneyStorage.MoneyAdded += OnMoneyAdded;
        }

        private void OnMoneyAdded(int obj)
        {
            _battleFogRoom.FillList();
        }

        public override MessageType GetStatus()
        {
            return _isReviled ? MessageType.YouFoundLetterHere : MessageType.PayForLetterHere;
        }

        public override SendAction[] GetSpecialActions()
        {
            if (_isReviled)
                return Array.Empty<SendAction>();
            
            return new SendAction[]
            {
                new($"Pay {Money}$", () =>
                {
                    var letter = Game.Instance.PasswordStorage.RevealFirst();
                    Game.Instance.MessagePopup.Open($"You found letter {letter}");
                    Revel.SetActive(true);
                    _isReviled = true;
                    _battleFogRoom.FillList();
                }, Game.Instance.MoneyStorage.CurrentMoney < Money)
            };
        }
    }
}