using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour {
    public string color;
    void Start ()
    {//ignore collisions with the ground and other fish
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Pickup").GetComponent<Collider>(), GetComponent<Collider>());
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 3);
	}

    void OnTriggerEnter(Collider col)
    {//If the collision is the player add points
        if (col.gameObject.tag == "Player") {
            Destroy(this.gameObject);
            if (color == "Normal") {
                Score.UpdateScore(1,1);
            } else if (color == "Bronze") {
                Score.UpdateScore(5,1);
            } else if (color == "Silver") {
                Score.UpdateScore(20,1);
            } else if (color =="Gold") {
                Score.UpdateScore(50,1);
            }
        }
    }
}
