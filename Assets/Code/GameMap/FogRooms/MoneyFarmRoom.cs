using UnityEngine;

namespace Code.GameMap.FogRooms
{
    public class MoneyFarmRoom : FogRoom
    {
        private int _moneyToTake;
        
        public int MoneyPerSecond = 1;

        private float _passedTime = 1f;

        private bool _isConnected;
        private BattleFogRoom _battleFogRoom;

        private void Update()
        {
            if (!_isConnected)
                return;
            
            _passedTime -= Time.deltaTime;
            if (_passedTime <= 0)
            {
                _passedTime = 1;
                _moneyToTake += MoneyPerSecond;
                _battleFogRoom.FillList(); 
            }
        }

        public override void OnConnected(BattleFogRoom battleFogRoom)
        {
            _isConnected = true;
            _battleFogRoom = battleFogRoom;
        }

        public override MessageType GetStatus()
        {
            return MessageType.MoneyFarmRoom;
        }

        public override SendAction[] GetSpecialActions()
        {
            return new[]
            {
                new SendAction($"Take ${_moneyToTake}", () =>
                {
                    Game.Instance.MoneyStorage.AddMoney(_moneyToTake);
                    _moneyToTake = 0;
                    _battleFogRoom.FillList(); 
                })
            };
        }
    }
}