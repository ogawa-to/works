using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public const float BATTLE_TIME = 120.0f;
    public const float INITIAL_MANA_AMOUNT = 4.0f;

    // prefub
    [SerializeField] private CardController cardPrefub = default;
    [SerializeField] private FieldGridController fieldGridPrefub = default;
    [SerializeField] private AttackGageController attackGagePrefub = default;
    [SerializeField] private GameObject animationPrefub = default;

    // controller
    [SerializeField] private TimeController timeController = default;
    [SerializeField] private PlayerManaController playerManaController = default;
    [SerializeField] private PlayerManaController enemyManacontroller = default;
    [SerializeField] public FieldController fieldController = default;
    [SerializeField] public SkillManager skillManager = default;
    [SerializeField] public EnemyAIController enemyAIController = default;

    // Transform
    [SerializeField] private Transform playerHandTransform = default;
    [SerializeField] public Transform enemyHandTransform = default;
    [SerializeField] private Transform fieldTranform = default;
    [SerializeField] private Transform canvasTransform = default;

    // デッキ
    private List<int> playerDeck;
    private List<int> enemyDeck;
    private List<int> playerDeckTestData = new List<int> {1, 2, 3, 4};
    private List<int> enemyDeckTestData = new List<int> { 1, 2, 3, 4 };

    // カードの手札枚数
    private int playerHandCount;
    private int enemyHandCount;
    // カードが死亡した枚数
    private int playerDeathCount;
    private int enemyDeathCount;

    public int playerSkillCount;
    public int enemySkillCount;

    // ゲームが中断しているか
    private bool isPauseGame;

    // シングルトンのインスタンスを保存する。
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Init();
    }

    // ゲーム開始時の処理
    private void Init()
    {
        float time = Time.deltaTime;
        float time2 = Time.timeScale;
        Time.timeScale = 1.0f;
        playerDeathCount = 0;
        enemyDeathCount = 0;
        playerHandCount = 0;
        enemyHandCount = 0;
        playerDeck = playerDeckTestData;
        enemyDeck = enemyDeckTestData;
        playerSkillCount = 2;
        enemySkillCount = 2;
        for (int i = 0; i < 3; i++)
        {
            DrawCard(GameSide.Player);
            DrawCard(GameSide.Enemy);
        }
        CreateField();
        timeController.Init(BATTLE_TIME);
        playerManaController.Init(INITIAL_MANA_AMOUNT);
        enemyManacontroller.Init(INITIAL_MANA_AMOUNT);
        // AIの思考開始
        enemyAIController.BeginThink();
    }

    // カードを手札に配置する。
    public void DrawCard(GameSide side)
    {
        if (side == GameSide.Player)
        {
            if (playerDeck.Count != 0)
            {
                int cardId = playerDeck[0];
                playerDeck.RemoveAt(0);
                CreateCard(cardId, playerHandTransform, side, CardPlace.HAND);
                playerHandCount++;
            }            
        } else
        {
            if (enemyDeck.Count != 0)
            {
                int cardId = enemyDeck[0];
                enemyDeck.RemoveAt(0);
                CreateCard(cardId, enemyHandTransform, side, CardPlace.HAND);
                enemyHandCount++;
            }
        }
    }

    // カードを生成する。
    public void CreateCard(int cardId, Transform transform, GameSide side, CardPlace place)
    {
        CardController card = Instantiate(cardPrefub, transform, false);
        card.Init(cardId, side, place);
    }

    // カードをフィールドに召喚する。
    public IEnumerator Summon(CardController card, FieldGridController fieldGrid)
    {
        // 自動レイアウトの再計算を待つため、フレームの終わりまで待機する。
        // = Cardの親をFieldGridに配置するまで待ちたいため。
        card.gameObject.transform.SetParent(fieldGrid.transform);
        yield return new WaitForEndOfFrame();

        // フィールド上の座標を設定する。
        card.model.gridPosition = fieldGrid.gridPosition;
        card.model.cardPlace = CardPlace.FIELD;

        fieldGrid.GetComponent<Image>().color =
            card.model.gameSide == GameSide.Player ? new Color(0.2f, 0.2f, 0.5f, 1.0f) : new Color(0.5f, 0.2f, 0.2f, 1.0f);


        // 攻撃ゲージオブジェクトを作成する。
        AttackGageController attackGage = Instantiate(attackGagePrefub, fieldGrid.transform, false);
        attackGage.Init();
        attackGage.model.card = card;
        attackGage.transform.SetParent(card.transform, false);

        // カードを1枚ドローする。
        DrawCard(card.model.gameSide);

        // 手札枚数のカウントを1減らす。
        DecrementHandCount(card.model.gameSide);

        // マナを消費する。
        GetPlayerMana(card.model.gameSide).model.ConsumeMana(card.model.cost);

        // 召喚時のアニメーション
        // card.ExtendCardParameterAnimation();

        // 召喚時のエフェクトを発動する。
        card.UseSkill(Skill.Timing.Summon);
    }

    public IEnumerator UseHeroSkill(GameSide gameSide)
    {
        GameManager.instance.UseSkillCount(gameSide);
        // オブジェクトを停止する。
        Pauser.Pause();

        // GUIを作成する。
        GameObject gameObject = Instantiate(animationPrefub, canvasTransform, false);
        HeroSkillAnimationController animation = gameObject.GetComponent<HeroSkillAnimationController>();

        StartCoroutine(animation.Animate());
        yield return new WaitForSeconds(HeroSkillAnimationController.ANIMATION_TIME);

        Destroy(gameObject);
        // オブジェクトの停止を解除する。
        Pauser.Resume();
    }

    // フィールドを生成する。
    private void CreateField()
    {
        // マス目数分
        for (int y = FieldGridController.GRID_Y_MIN; y <= FieldGridController.GRID_Y_MAX; y++)
        {
            for (int x = FieldGridController.GRID_X_MIN; x <= FieldGridController.GRID_X_MAX; x++)
            {
                FieldGridController fieldGrid = Instantiate(fieldGridPrefub, fieldTranform, false);
                fieldGrid.gridPosition = new Vector2Int(x, y);
                fieldGrid.name = "Grid" + "Y" + y + "X" + x;
                fieldGrid.transform.SetParent(fieldTranform, false);
            }
        }
    }

    // 手札の枚数を取得する。
    public int GetHandCount(GameSide side)
    {
        int count = (side == GameSide.Player) ? playerHandCount : enemyHandCount;
        return count;
    }

    // デッキの枚数を取得する。
    public int GetDeckCount(GameSide side)
    {
        List<int> deck = (side == GameSide.Player) ? playerDeck : enemyDeck;
        return deck.Count;
    }

    //　死亡カードの枚数を取得する。
    public int GetDeathCount(GameSide side)
    {
        int count = (side == GameSide.Player) ? playerDeathCount : enemyDeathCount;
        return count;
    }

    // 手札枚数を減らす。
    public void DecrementHandCount(GameSide side)
    {
        if (side == GameSide.Player)
        {
            playerHandCount--;
        }
        else
        {
            enemyHandCount--;
        }
    }

    // 死亡カードの枚数を加算する。
    public void IncrementDeathCount(GameSide side)
    {
        if (side == GameSide.Player)
        {
            playerDeathCount++;
        }
        else
        {
            enemyDeathCount++;
        }
    }

    public PlayerManaController GetPlayerMana(GameSide side)
    {
        PlayerManaController manaController = (side == GameSide.Player) ? playerManaController : enemyManacontroller;
        return manaController;
    }

    // フィールド上のユニット数を取得する。
    public int GetFightingUnitCount(GameSide side)
    {
        CardController[] cards = fieldTranform.GetComponentsInChildren<CardController>();
        int count = Array.FindAll(cards, card => card.model.gameSide == side).Length;
        return count;
    }

    // ヒーロースキルを発動する。
    public void UseSkillCount(GameSide side)
    {
        if (side == GameSide.Player)
        {
            playerSkillCount--;
        } else
        {
            enemySkillCount--;
        }
    }
    public void IncreaseSkillCount(GameSide side)
    {
        if (side == GameSide.Player)
        {
            playerSkillCount++;
        }
        else
        {
            enemySkillCount++;
        }
    }
    public int GetSkillCount(GameSide side)
    {
        return (side == GameSide.Player) ? playerSkillCount : enemySkillCount;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetGame();
        }
    }
    private void ResetGame()
    {
        SceneManager.LoadScene("BattleScene");
        /*
        FieldGridController[] gridController = fieldController.GetComponentsInChildren<FieldGridController>();
        foreach (FieldGridController g in gridController)
        {
            Destroy(g.gameObject);
        }

        CardController[] cards = enemyHandTransform.GetComponentsInChildren<CardController>();
        foreach (CardController c in cards) {
            Destroy(c.gameObject);
        }
        cards = playerHandTransform.GetComponentsInChildren<CardController>();
        foreach (CardController c in cards)
        {
            Destroy(c.gameObject);
        }

        Init();
        */
    }
}