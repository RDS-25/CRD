using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image attackImage;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public Image[] skillIcons;
    
    public TextMeshProUGUI skillDescriptions;
    public UnitData data;
    public void DisplayCommands(List<ISelectable> selectables)
    {
        
        if (selectables.Count == 0 || !(selectables[0] is Unit unit))
        {
            // 모든 UI 요소 초기화
            nameText.text = "";
            attackImage.sprite = null;
            attackText.text = "";
            hpText.text = "";
            mpText.text = "";
            // skillIcons 초기화
            foreach (Image skillIcon in skillIcons)
            {
                skillIcon.sprite = null;
            }
            return;
        }
        data = unit.unitData;
        
        if (selectables.Count == 1)
        {
            
            attackImage.sprite = data.attackicon;
            nameText.text = data.name;
            attackText.text = data.attackDamage.ToString();
            hpText.text = unit.hp.ToString();
            mpText.text = unit.mp.ToString();
            for (int i = 0; i < unit.unitData.skills.Length; i++)
            {
                skillIcons[i].sprite = data.skills[i].skillIcon;
                
            }
        }
    }

    
    
    public void OnPointerEnter(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        if (pointerData == null) return;

        for (int i = 0; i < skillIcons.Length; i++)
        {
            if (pointerData.pointerCurrentRaycast.gameObject == skillIcons[i].gameObject)
            {
                if (skillIcons[i].sprite != null)
                {
                    skillDescriptions.gameObject.SetActive(true);
                    skillDescriptions.gameObject.transform.position = pointerData.position;
                    skillDescriptions.text = data.skills[i].skillDescription;
                    break;
                }
            }
        }
    }
    public void OnPointerExit(BaseEventData eventData)
    {
        
        skillDescriptions.gameObject.SetActive(false);
        skillDescriptions.text = "";
                
    }

   
}
