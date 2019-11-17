using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /*
     * Création d'un object dans la scène appelé GameController avec ce script attaché.
     * L'object GameController prend aussi un script appelé roomNavigation, qui contient qu'une Room, qui est un ScriptableObject
     * 
     * 
     */

    //Le script contenant les salles
    [HideInInspector] public RoomNavigation roomNavigation;

    [HideInInspector] public List<string> InteractionDescriptionRooms = new List<string>();

    //Le text dans lequel afficher le log
    public Text displayText;

    //Liste de chaînes
    List<string> actionLog = new List<string>();

    //attache à roomNavigation le script contenant les salles
    private void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    //Rajoute la description de la current room dans le text
    public void DisplayRoomText()
    {
        UnpackRoom();

        string joinedInteractionDescriptions = string.Join("\n", InteractionDescriptionRooms.ToArray());



        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

        logStringWithReturn(combinedText);
    }

    //Rajoute a l'action log la chaine passée en paramètre + retour à la ligne
    public void logStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    //Affiche le texte
    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());

        displayText.text = logAsText;
    }

    private void UnpackRoom()
    {
        roomNavigation.UnpackExitsInRoom(); 
    }


}
