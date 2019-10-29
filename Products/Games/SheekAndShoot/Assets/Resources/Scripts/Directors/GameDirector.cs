using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    PlayerGenerator mPlayerGenerator;
    EnemyGenerator mEnemyGenerator;
    RippleGenerator mRippleGenerator;

    // 音声
    AudioClip[] mAudioClipSEs;
    AudioClip[] mAudioClipBGMs;

    int mGameCount;

    public int mEnemyBeatCount;
    public int mEnemyBeatCountMax;

    // ゲームオーバー画面
    public GameObject mGameOverCanvasPrefub;
    GameObject mGameOverCanvasClone;

    // ステージクリアー画面
    public GameObject mStageClearCanvasPrefub;
    GameObject mStageClearCanvasClone;

    // ステージ番号 (1つしかないけど)
    [System.NonSerialized]
    public int currentStageNum = 0;
    AudioSource audio;

    void Awake()
    {
        LoadResources();
    }

    void Start()
    {
        mGameCount = 0;
        createPlayer();
        audio = GetBgm(this.gameObject, "bgm001");
        audio.volume = 0.5f;
        mEnemyBeatCount = 0;
        mEnemyBeatCountMax = 77;
}

    void Update()
    {
        // BGM再生
        if (mGameCount == 0)
        {
            audio.Play();
        }
        
        // 生き残ったらゲームクリアー
        if (mGameCount == 18000)
        {
            StageClear();
        }

        // 死んだらゲームオーバー
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().mDeathFlag)
        {
            GameOver();
        }
        

        mGameCount++;
    }


    public int GetGameCount()
    {
        return mGameCount;
    }

    public void createPlayer()
    {
        // プレイヤを生成する。
        mPlayerGenerator = GameObject.Find("PlayerGenerator").GetComponent<PlayerGenerator>();
        PlayerController player = mPlayerGenerator.Create();

        //
        mRippleGenerator = GameObject.Find("RippleGenerator").GetComponent<RippleGenerator>();
        player.mRippleShoot = mRippleGenerator.Create();
    }

    // ゲームオーバー
    public void GameOver()
    {
        mGameOverCanvasClone = Instantiate(mGameOverCanvasPrefub);
        Button[] buttons = mGameOverCanvasClone.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(Retry);
    }

    // ステージクリアー
    public void StageClear()
    {
        mStageClearCanvasClone = Instantiate(mStageClearCanvasPrefub);
        Button[] buttons = mStageClearCanvasClone.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(BackToTitle);
        
    }

    // リトライ処理
    public void Retry()
    {
        // ゲームオーバー画面を削除する。
        Destroy(mGameOverCanvasClone);

        // 画面をロードしなおす。
        SceneManager.LoadScene("GameScene");
    }

    // タイトル画面へ戻る
    public void BackToTitle()
    {
        // ゲームオーバー画面を削除する。
        Destroy(mStageClearCanvasClone);

        // 画面をロードしなおす。
        SceneManager.LoadScene("TitleScene");
    }

    public void LoadResources()
    {
        mAudioClipSEs = Resources.LoadAll<AudioClip>("Sounds/SE");
        mAudioClipBGMs = Resources.LoadAll<AudioClip>("Sounds/BGM");
    }

    // 新しいAudioSourceを作成し，オブジェクトに設定する。
    // AudioClipについては当クラスであらかじめ読み込んでおく。
    public AudioSource GetSe(GameObject gameObject, string clipName)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        foreach (AudioClip clip in mAudioClipSEs)
        {
            if (clip.name == clipName)
            {
                audioSource.clip = clip;
                break;
            }
        }
        return audioSource;
    }

    // 新しいAudioSourceを作成し，オブジェクトに設定する。
    // AudioClipについては当クラスであらかじめ読み込んでおく。
    public AudioSource GetBgm(GameObject gameObject, string clipName)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        foreach (AudioClip clip in mAudioClipBGMs)
        {
            if (clip.name == clipName)
            {
                audioSource.clip = clip;
                break;
            }
        }
        return audioSource;
    }
}
