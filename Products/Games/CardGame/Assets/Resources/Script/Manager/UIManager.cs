using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI操作
public class UIManager : MonoBehaviour
{
    // カード枚数情報
    [SerializeField] private Text playerDeckCountTransform = default;
    [SerializeField] private Text playerHandCountTransform = default;
    [SerializeField] private Text playerDeathCountTransform = default;
    [SerializeField] private Text enemyDeckCountTransform = default;
    [SerializeField] private Text enemyHandCountTransform = default;
    [SerializeField] private Text enemyDeathCountTransform = default;

    // 戦況情報
    [SerializeField] private Text battleSituationTextTransform = default;

    // スキル情報
    [SerializeField] private Text playerSkillCountText = default;
    [SerializeField] private Text enemySkillCountText = default;

    public void Update()
    {
        Draw();
    }

    public void Draw()
    {
        playerDeckCountTransform.text = "D : " + GameManager.instance.GetDeckCount(GameSide.Player).ToString();
        playerHandCountTransform.text = "H : " + GameManager.instance.GetHandCount(GameSide.Player).ToString();
        playerDeathCountTransform.text = "C : " + GameManager.instance.GetDeathCount(GameSide.Player).ToString();

        enemyDeckCountTransform.text = "D : " + GameManager.instance.GetDeckCount(GameSide.Enemy).ToString();
        enemyHandCountTransform.text = "H : " + GameManager.instance.GetHandCount(GameSide.Enemy).ToString();
        enemyDeathCountTransform.text = "C : " + GameManager.instance.GetDeathCount(GameSide.Enemy).ToString();

        int playerUnitCount = GameManager.instance.GetFightingUnitCount(GameSide.Player);
        int enemyUnitCount= GameManager.instance.GetFightingUnitCount(GameSide.Enemy);
        battleSituationTextTransform.text = playerUnitCount.ToString("D2") + " vs " + enemyUnitCount.ToString("D2");

        playerSkillCountText.text = GameManager.instance.GetSkillCount(GameSide.Player).ToString();
        enemySkillCountText.text = GameManager.instance.GetSkillCount(GameSide.Enemy).ToString();
    }

}
