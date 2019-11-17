using UnityEngine;
using System.Collections;
using TMPro;

public class Player_movement : MonoBehaviour
{

    public float speed;
    private int count;
    private int nbrOfPickUps;

    //Player's rigidbody
    private Rigidbody rb;

    //the parent containing every pick ups
    public GameObject pickUpsParent;

    //Texts of score and win
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winText;

    void Start()
    {
        //Initialize the nbrOfPickUps variable with the returned value of getAllChildren();
        nbrOfPickUps = getAllChildren();

        //Rb receives the player's rigidbody
        rb = GetComponent<Rigidbody>();

        //Initialize the score text with the count value
        SetCountText();

        //Initialize the win text with an empty
        winText.text = "";
    }

    void FixedUpdate()
    {
        //Get inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //If the player collides with a pick up
        if (other.gameObject.CompareTag("Pick Up"))
        {
            //Deactivate the pick up it collided with
            other.gameObject.SetActive(false);

            //Incremente the score and sets it
            count++;
            SetCountText();

            //Check if every collectibles were taken
            if(count == nbrOfPickUps)
            {
                winText.text = "You win !";
            }
        }
    }

    //Method to set the score text
    void SetCountText()
    {
        countText.text = count.ToString();
    }

    //Function that returns the number of children in the pick up parent
    int getAllChildren()
    {
        int nbrOfChildren = 0; 

        foreach(Transform children in pickUpsParent.transform)
        {
            nbrOfChildren++;
        }

        return nbrOfChildren;
    }
}
