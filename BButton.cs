using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BButton : MonoBehaviour, IPointerClickHandler
{
    public bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    public void OnPointerClick(PointerEventData eventData)
    {
        pressed = true;
        StartCoroutine(ClickTimeOut());
    }
    IEnumerator ClickTimeOut()
    {
        yield return new WaitForSeconds(0.1f);
        pressed = false;
    }
}
