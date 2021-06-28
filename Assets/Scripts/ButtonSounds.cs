using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonPressSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Button>().Select();
    }

    public void OnSelect(BaseEventData eventData) 
	{
		AudioManager.instance.Play("ButtonHover");
	}

    public void ButtonPressSound()
    {
        AudioManager.instance.Play("ButtonPress");
    }
}
