using Code;
using Code.Core;
using Code.GameMap;
using Code.GameMap.Storage;
using Code.Player;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public SendButtonsPanel SendButtonsPanel;
    public PasswordPopup PasswordPopup;
    public MessagePopup MessagePopup;
    public GameField GameField;
    public PlayerSelector PlayerSelector { get; }= new();
    public MoneyStorage MoneyStorage { get; }= new();
    public DronStats DronStats { get; } = new();
    public PasswordStorage PasswordStorage { get; } = new(new PasswordGenerator());
    
    public void Awake()
    {
        Instance = this;
    }
}
