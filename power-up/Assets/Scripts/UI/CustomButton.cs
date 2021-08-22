using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    bool mouseIn;
    Texture startImage;
    [SerializeField] Texture highlightImage;
    [SerializeField] Texture pressedImage;


    [SerializeField] TextMeshProUGUI TMPRo;
    private RawImage imageRend;
    private int ID;
    BuildingUpgradeHandles upgradeHander;
    

    void Awake()
    {
        
    }
    void Start()
    {
        imageRend = GetComponent<RawImage>();
        startImage = imageRend.texture;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        imageRend.texture = pressedImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imageRend.texture = highlightImage;
        mouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageRend.texture = startImage;
        mouseIn = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (mouseIn)
        {
            imageRend.texture = highlightImage;
            upgradeHander.TriggerButton(ID);
        }
    }

    public void startBottun(BuildingUpgradeHandles buh, int _ID)
    {
        upgradeHander = buh;
        ID = _ID;
    }

    public void updateTextTag(string tag)
    {
        TMPRo.text = tag;
    }

}
