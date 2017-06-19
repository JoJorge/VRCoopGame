using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour {

    [SerializeField]
    private GameObject leftDoor;
    private GameObject rightDoor;
    private float velocity = 0.1f;
    private float distance = 2.5f;

	// Use this for initialization
	void Start () {
        leftDoor = GameObject.Find("left");
        rightDoor = GameObject.Find("right");
    }

    public IEnumerator openDoor() {
        while (distance > 0) {
            Vector3 pos = leftDoor.transform.localPosition;
            pos.x -= velocity;
            leftDoor.transform.localPosition = pos;
            pos = rightDoor.transform.localPosition;
            pos.x += velocity;
            rightDoor.transform.localPosition = pos;
            distance -= velocity;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
