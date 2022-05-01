using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipHUD : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform spaceship;
    public Camera camera;

    ShipController shipController;


    RectTransform heightBars;
    RectTransform texts;
    RectTransform crosshair;
    RectTransform hitTarget;
    Text heightText1;
    Text heightText2;
    Text heightText3;
    Text crosshairDistanceText;
    RectTransform distanceFill;

    void Start()
    {
        heightBars = transform.Find("Height Bars").GetComponent<RectTransform>();
        texts = transform.Find("Texts").GetComponent<RectTransform>();
        heightText1 = texts.Find("Height Text 1").GetComponent<Text>();
        heightText2 = texts.Find("Height Text 2").GetComponent<Text>();
        heightText3 = texts.Find("Height Text 3").GetComponent<Text>();

        crosshair = transform.Find("Crosshair").GetComponent<RectTransform>();
        hitTarget = transform.Find("Hit Target").GetComponent<RectTransform>();
        shipController = spaceship.GetComponent<ShipController>();
        crosshairDistanceText = hitTarget.Find("Distance Text").GetComponent<Text>();
        distanceFill = hitTarget.Find("Distance Fill").GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {

        float spaceshipHeight = spaceship.transform.position.y;

        float barsHeight = (-spaceshipHeight * 10) % 75;
        float textsHeight = (-spaceshipHeight * 10) % 100;

        Debug.Log("spaceshipHeight: " + spaceshipHeight + "   textsHeight: " + textsHeight);

        float midHeight = Mathf.Floor(spaceshipHeight / 10) * 10;

        heightText1.text = midHeight + 10 + "";
        heightText2.text = midHeight + "";
        heightText3.text = midHeight - 10 + "";
        

        heightBars.anchoredPosition = new Vector3(heightBars.anchoredPosition.x, barsHeight, 0);

        texts.anchoredPosition = new Vector3(texts.anchoredPosition.x, textsHeight, 0);

        crosshair.position = camera.WorldToScreenPoint(shipController.lookTarget);

        if (shipController.hit)
        {
            hitTarget.position = camera.WorldToScreenPoint(shipController.hitTarget);

            float distance = Vector3.Distance(spaceship.transform.position, shipController.hitTarget);

            distance = Mathf.Round(distance);

            crosshairDistanceText.text =  distance + "m";

            distanceFill.sizeDelta = new Vector2(100 + (distance / 4800 * 100), 35);

        } else
        {
            crosshairDistanceText.text = "***";

            distanceFill.sizeDelta = new Vector2(100, 35);

            hitTarget.position = new Vector3(-1000, -1000);

        }




        

    }

    // Update is called once per frame
    void Update()
    {

    }
}
