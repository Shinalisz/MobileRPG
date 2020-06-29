using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager is null!");
            }
            return _instance;
        }
    }

    public Text playerGemcountText;
    public Image selectionImage;
    public Text gemCountText;
    public Image[] livesImages;

    private void Awake()
    {
        _instance = this;
        
    }

    public void OpenShop(int gemCount)
    {
        playerGemcountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    public void UpdateLives(int livesRemaining)
    {
        
        for (int i = 0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                livesImages[i].enabled = false;
            }
        }
        
    }
}
