using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextInput : MonoBehaviour
{
    GameController controller;
    public InputField inputField;

    private void Awake()
    {
        controller = GetComponent<GameController>();

        //Quand l'input est entré, lance AcceptStringInput
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    //Méthode d'ajout d'input dans le log
    void AcceptStringInput(string userInput)
    {
        //Formalisation en minuscule
        userInput = userInput.ToLower();

        //Rajoute à la lite de l'actionLog, l'input de l'utilisateur
        controller.logStringWithReturn(userInput);

        
        InputComplete();
    }

    void InputComplete()
    {
        //Affiche le texte dans le actionLog
        controller.DisplayLoggedText();

        //Reactive le champ de saisie
        inputField.ActivateInputField();

        //et l'initialise à vide
        inputField.text = null;
    }
}
