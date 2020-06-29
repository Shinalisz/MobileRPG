using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AButton : MonoBehaviour, IPointerDownHandler
{
    public bool pressed = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        StartCoroutine(ClickTimeOut());
    }


    IEnumerator ClickTimeOut()
    {
        yield return new WaitForSeconds(1.0f);
        pressed = false;
    }
}
