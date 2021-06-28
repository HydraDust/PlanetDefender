using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonInfo : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject info;

    public void OnSelect(BaseEventData eventData) 
	{
		info.SetActive(true);
	}

    public void OnDeselect(BaseEventData data) 
	{
		info.SetActive(false);
	}

}
