using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchUI : MonoBehaviour {

    // private const int RIGHT_ANGLE = 90;
    private bool isDisplayed = false;
    public Transform mainCamera = null;
    private double thresholdAngle = -10 ;
    public Transform reference = null;
    public Sprite unavailable = null;



    // Use this for initialization
    void Start () {
        mainCamera = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        

    }
    public bool getIsDisplayed() {
        return isDisplayed;
    }
    public void DisplayUI (double x,int num) {
        if (!isDisplayed && x <= thresholdAngle && GvrViewer.Instance.Triggered) {
            // itemList;
            isDisplayed = true;
            Debug.Log("UI Active");
            transform.Find("ButtonCollection").gameObject.SetActive(true);
            // get the orient and pos from reference
            Quaternion ori = Quaternion.Euler(reference.rotation.eulerAngles);
            Vector3 pos = reference.transform.position;
            transform.SetPositionAndRotation(pos, ori);
            changeSprite(unavailable, "ButtonLT"); // 
        }
    }
    public void disableUI() {
        isDisplayed = false;
        Debug.Log("UI Not Active");
        transform.Find("ButtonCollection").gameObject.SetActive(false);
    }
    private void changeSprite(Sprite newSprite, string btnName) {
        Image theImage = GameObject.Find(btnName).GetComponent<Image>();
        theImage.sprite = newSprite;
    }

}
