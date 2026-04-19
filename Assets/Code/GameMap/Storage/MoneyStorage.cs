using System;

namespace Code.GameMap.Storage
{
    public class MoneyStorage
    {
        public int CurrentMoney { get; private set; } = 5;
        
        public event Action<int> MoneyAdded;
        
        public void AddMoney(int value)
        {
            if (value == 0)
                return;
            
            CurrentMoney += value;
            
            if (value > 0)
                Game.Instance.MoneyAudio.PlayWithRandomPitch();
            else
                Game.Instance.MoneyRemAudio.PlayWithRandomPitch();
            MoneyAdded?.Invoke(value);
        }
    }
}