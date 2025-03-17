using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LearningCurve : MonoBehaviour
{
    public int currentGold = 35;
    public bool PureOfHeart = true; 
    public bool HasSecretIncantation = false;
    public string RareItem = "The Amulet";
    //public string CharacterAction = "Attack";
    public int Dice = 7;
    public Weapon HuntingBow = new Weapon("Hunting Bow", 105);
    public GameObject DirectionalLight;
    public Transform LightTransform;

    public Transform CamTransform;

    public Dictionary<string, int> ItemInventory = new Dictionary<string, int>() {
        {"Potion", 5}, 
        {"Antidote", 7},
        {"Aspirin", 1}
        };
    public List<string> QuestPartyCharacters = new List<string>() {
        "Grim the barbarian",
        "Merlin the wise",
        "Sterling the knight"
    };


    // Start is called before the first frame update
    void Start()
    {
        //DirectionalLight = GameObject.Find("Directional Light");
        LightTransform = DirectionalLight.GetComponent<Transform>();
        Debug.Log(LightTransform.localPosition);
        Character hero = new Character("Julian");
        Character worseHero = new Character("Worse Julian");
    }

    

    /*public void PrintCharacterAction(){
        switch(CharacterAction){
            case "Heal":
            Debug.Log("potion sent!");
            break;
            case "Attack": 
            Debug.Log("To arms!");
            break;
            default:
            Debug.Log("Shields Up!");
            break;
        }
    }*/

    public void OpenTreasureChamber(){
        if(PureOfHeart && RareItem == "The Amulet"){
            if(!HasSecretIncantation){
                Debug.Log("You do not possess the secret incantation");
            } else {
                Debug.Log("You possess the secret incantation! Speak it now, and proceed");
            }
        } else {
            Debug.Log("Come back when you have the heart and steel to overcome what lies beyond");
        }
    }

    public void RollDice(){
        switch (Dice){
            case 7: 
            case 15: 
            Debug.Log("You hit! Roll for damage.");
            break;
            case 20: 
            Debug.Log("Critical hit! You roll double damage");
            break;
            default:
            Debug.Log("You missed. Your enemy takes his turn");
            break;
        
        }
        
    }
    public void thievery(){
        if(currentGold > 50) {
            Debug.Log("You're rolling in it");

        } else if (currentGold < 15) {
            Debug.Log("Nothing much there to steal");
        } else {
            Debug.Log("Looks like your purse is in the sweet spot.");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }
}
