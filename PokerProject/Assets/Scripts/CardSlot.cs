using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour 
{ 

public bool IsSelected { get; set; }
public Image highlightImage;

void Start()
{
    IsSelected = false;
    highlightImage.enabled = false;
}

public void ToggleSelection()
{
    IsSelected = !IsSelected;
    highlightImage.enabled = IsSelected;
}
}