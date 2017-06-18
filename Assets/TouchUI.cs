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
    public List<string> listOfBtn;



    // Use this for initialization
    void Start () {
        mainCamera = Camera.main.transform;
        listOfBtn.Add("ButtonLT");
        listOfBtn.Add("ButtonRT");
        listOfBtn.Add("ButtonLD");
        listOfBtn.Add("ButtonRD");
    }
    // Update is called once per frame
    void Update()  {
        

    }
    public bool getIsDisplayed() {
        return isDisplayed;
    }
    public void DisplayUI (double x, List<Item> itemList) {  
        if (!isDisplayed && x <= thresholdAngle && GvrViewer.Instance.Triggered) {
            transform.Find("ButtonCollection").gameObject.SetActive(true);
            // change image of the btn
            for (int i = 0; i < 4; i++) {
                if (i < itemList.Count) {
                    changeIamge(itemList[i].icon, listOfBtn[i]);
                }
                else {
                    changeIamge(unavailable, listOfBtn[i]);
                }
            }
            isDisplayed = true;
            Debug.Log("UI Active");
           
            // get the orient and pos from reference
            Quaternion ori = Quaternion.Euler(reference.rotation.eulerAngles);
            Vector3 pos = reference.transform.position;
            transform.SetPositionAndRotation(pos, ori);
        }
    }
    public void disableUI() {
        isDisplayed = false;
        Debug.Log("UI Not Active");
        transform.Find("ButtonCollection").gameObject.SetActive(false);
    }
    private void changeIamge(Sprite newSprite, string btnName) {
        Debug.Log(btnName);
        Image theImage = GameObject.Find(btnName).GetComponent<Image>();
        theImage.sprite = newSprite;
    }

}
