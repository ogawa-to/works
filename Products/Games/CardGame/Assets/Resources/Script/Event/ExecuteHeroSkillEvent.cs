using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExecuteHeroSkillEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameSide gameSide = default;

    bool isActiveEvent;

    public void Start()
    {
        isActiveEvent = false;
    }

    // クリックイベント
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActiveEvent)
        {
            isActiveEvent = true;
            int skillCount = GameManager.instance.GetSkillCount(gameSide);
            if (GameManager.instance.playerSkillCount > 0)
            {
                StartCoroutine(GameManager.instance.UseHeroSkill(gameSide));
            }
        }
    }
}
