using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

using CustomExtensions;

public class GameBehaviour : MonoBehaviour, IManager
{
    public Button WinButton;
    public Button LossButton;
    public const int MaxItems = 4;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;
    public Stack<Loot> LootStack = new Stack<Loot>();

    private int _itemsCollected = 0;
    private int _playerHP = 10;
    private string _state;

    public string State
    {
        get {return _state;}
        set {_state = value;}
    }
    

    
    // Start is called before the first frame update
    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;

        Initialize();
    }

    public void Initialize()
    {
        _state = "Game Manager Initialized..";
        _state.FancyDebug();
        Debug.Log(_state);

        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));
    }

    void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    public int Items {
        get { return _itemsCollected;}

        set {_itemsCollected = value;
            ItemText.text = "Items: " + Items;
            if (_itemsCollected >= MaxItems)
            {
                WinButton.gameObject.SetActive(true);
                UpdateScene("You've found all the items!");
            }
            else 
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " left!";
            }
        }
    }

    public void RestartScene()
    {
        Utility.RestartLevel(0);
    }


    public int HP 
        {
        get {return _playerHP;}
        set {
            _playerHP = value;
            HealthText.text = "Health: " + HP;

            if(_playerHP <= 0)
            {
                LossButton.gameObject.SetActive(true);
                UpdateScene("You want another life with that?");
            } 
            else
            {
                ProgressText.text = "Ouch... That's gotta hurt!";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();

        var nextItem = LootStack.Peek();

        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem.Name, nextItem.Name);
        Debug.LogFormat("There are {0} random loot items waiting for you!", LootStack.Count);
    }

    //public void FilterLoot(){
    //    var rareLoot = LootStack.Where();
    //}

    public bool LootPredicate(Loot loot)
    {
        return loot.Rarity >= 3;
    }
}
 