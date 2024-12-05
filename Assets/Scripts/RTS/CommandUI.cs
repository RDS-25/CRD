using System;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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

    private void Update()
    {
     
    }

    public void DisplayCommands(List<ISelectable> selectables)
    {
        //클릭한 캐릭터가 없을때 
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
        //클릭한 캐릭터가 하나일때
        if (selectables.Count == 1)
        {
            //초기화
            foreach (Image skillIcon in skillIcons)
            {
                skillIcon.sprite = null;
            }

            attackImage.sprite = data.attackicon;
            nameText.text = data.name;
            attackText.text = data.attackDamage.ToString();
            hpText.text = unit.hp.ToString();
            foreach (var VARIABLE in data.skills)
            {
                if (VARIABLE is SkillMana skillMana )
                {
                    mpText.text = unit.mp.ToString() + "/" + unit.Maxmp.ToString();
                }
            }
            //이미지에 스킬먼저
            for (int i = 0; i < unit.unitData.skills.Length; i++)
            {
                skillIcons[i].sprite = data.skills[i].skillIcon;
            }

            //나머지 이미지에 조합법
            for (int i = 0; i < data.craftingRecipes.Length; i++)
            {
                skillIcons[data.skills.Length + i].sprite = data.craftingRecipes[i].createSprite;
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
                    if (i < data.skills.Length)
                    {
                        skillDescriptions.text = data.skills[i].skillDescription;
                    }
                    else
                    {
                        skillDescriptions.text = data.craftingRecipes[i - data.skills.Length].description;
                        // 기존 UI 요소에 Button 컴포넌트 추가
                        AddButtonComponentToIcon(skillIcons[i], i - data.skills.Length);
                    }

                }
            }
        }
    }

    public void OnPointerExit(BaseEventData eventData)
    {

        skillDescriptions.gameObject.SetActive(false);
        skillDescriptions.text = "";

    }

    private void AddButtonComponentToIcon(Image icon, int recipeIndex)
    {
        // 아이콘에 Button 컴포넌트 추가
        Button button = icon.gameObject.GetComponent<Button>();

        // 버튼 클릭 이벤트 추가
        button.onClick.RemoveAllListeners(); // 이전 리스너 제거
        button.onClick.AddListener(() => OnRecipeButtonClick(recipeIndex));
    }


    private void OnRecipeButtonClick(int index)
    {
        data.craftingRecipes[index].CombineItem();
    }




}
