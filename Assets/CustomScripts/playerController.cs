using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
    private static CardboardControl cardboard;
    public float speed;
    public Material activeScreen;
    public Material activeIndicator;
    public Material inactiveIndicator;

    private bool moving = false;    
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start () {
        cardboard = GameObject.Find("CardboardControlManager").GetComponent<CardboardControl>();
        cardboard.trigger.OnDown += OnTriggerDown;
        cardboard.trigger.OnUp += OnTriggerUp;
        cardboard.gaze.OnStare += OnStare;
        cardboard.gaze.OnChange += OnChange;


    }
	
	// Update is called once per frame
	void Update () {
        CharacterController player = GetComponent<CharacterController>();
        if (moving)
        {
            moveDirection = Camera.main.transform.forward;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            player.Move(moveDirection * Time.deltaTime);

        }
	}

    void OnTriggerDown(object sender)
    {
       moving = !moving;        
   
    
    }

    void OnTriggerUp(object sender)
    {
        moving = !moving;
    }

    void OnStare(object sender) {
        CardboardControlGaze gaze = sender as CardboardControlGaze;
   

        if (gaze.IsHeld() && gaze.Object().tag == "Main Screen")
        {
            Debug.Log(gaze.Object().tag);
            gaze.Object().GetComponent<Renderer>().material = activeScreen;
        }

        if (gaze.IsHeld() && (gaze.Object().tag == "Next" || gaze.Object().tag == "Prev"))
        {
            Debug.Log(gaze.Object().tag);
            gaze.Object().GetComponent<Renderer>().material = activeIndicator;
        }


    }


    void OnChange(object sender)
    {
        CardboardControlGaze gaze = sender as CardboardControlGaze;

        /*
        if (gaze.WasHeld() && gaze.PreviousObject().tag == "Prev" || gaze.PreviousObject().tag == "Next")
        {
            gaze.PreviousObject().GetComponent<Renderer>().material = inactiveIndicator;
        } else
        {
            return;
        }*/
        Debug.Log("changed:");


    }

}
