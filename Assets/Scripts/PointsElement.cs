using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PointsElement : MonoBehaviour
{
    GameObject pointsBar;
    Image pointsBarImage; 
    //GameObject targetObject;
    PointsElement element;
    public string assignTag = "TODO: Change";
    public string followTargetName = "TODO: Change";
    public Sprite pointsMax;
    public Sprite points90;
    public Sprite points80;
    public Sprite points70;
    public Sprite points60;
    public Sprite points50;
    public Sprite points40;
    public Sprite points30;
    public Sprite points20;
    public Sprite points10;
    public Sprite pointsZero;

    public int rangeScaleUpFactor = 5;
    public int _playerCash = 0;
    private int playerCashMax;

    public int PlayerCash
    {
        get { return _playerCash; }
        set { _playerCash = value; }
    }

    public PointsElement()
    {
        playerCashMax = 100 * rangeScaleUpFactor;
    }
  

    public void AddToPoints(int increaseBy)
    {
        if ((PlayerCash + increaseBy) < (playerCashMax)) {
           PlayerCash = PlayerCash + increaseBy; 
        } else {
            PlayerCash = 100 * rangeScaleUpFactor;
        }
        spriteChange();
        //Events.treasurePicked(gameObject, increaseBy);
    }

    public void spriteChange()
    {
    
        if (assignTag != "TODO: Change") {
            pointsBar = GameObject.FindWithTag(assignTag);
            Debug.Log("P-Bar: " + pointsBar);
        }

        if (pointsBar != null)
        {
            pointsBarImage = pointsBar.GetComponent<Image>();
            Debug.Log("P-Bar: " + pointsBarImage);
        }
        
        
        Debug.Log("P-Bar2: " + pointsBarImage);
        if (pointsBarImage != null)
        {
            Debug.Log("points Image found  " + PlayerCash);
            
            if (PlayerCash <= 0) {
                pointsBarImage.sprite = pointsZero;
            }
            else if (PlayerCash > 0 && PlayerCash <= (playerCashMax * 0.1)) {
                pointsBarImage.sprite = points10;
            }
            else if (PlayerCash > (playerCashMax * 0.1) && PlayerCash <= (playerCashMax * 0.2)) {
                pointsBarImage.sprite = points20;
            }
            else if (PlayerCash > (playerCashMax * 0.2) && PlayerCash <= (playerCashMax * 0.3)) {
                pointsBarImage.sprite = points30;
            }
            else if (PlayerCash > (playerCashMax * 0.3) && PlayerCash <= (playerCashMax * 0.4)) {
                pointsBarImage.sprite = points40;
            }
            else if (PlayerCash > (playerCashMax * 0.4) && PlayerCash <= (playerCashMax * 0.5)) {
                pointsBarImage.sprite = points50;
            }
            else if (PlayerCash > (playerCashMax * 0.5) && PlayerCash <= (playerCashMax * 0.6)) {
                pointsBarImage.sprite = points60;
            }
            else if (PlayerCash > (playerCashMax * 0.6) && PlayerCash <= (playerCashMax * 0.7)) {
                pointsBarImage.sprite = points70;
            }
            else if (PlayerCash > (playerCashMax * 0.7) && PlayerCash <= (playerCashMax * 0.8)) {
                pointsBarImage.sprite = points80;
            }
            else if (PlayerCash > (playerCashMax * 0.8) && PlayerCash < playerCashMax) {
                pointsBarImage.sprite = points90;
            }
            else {
                pointsBarImage.sprite = pointsMax;
            }
        }
        else
        {
            //Debug.LogError("points-bar Image not found");
        }
    }
}

