using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーマナを管理するクラス
public class PlayerManaController : MonoBehaviour
{
    [HideInInspector] public PlayerManaModel model;
    private PlayerManaView view;

    private void Awake()
    {
        model = new PlayerManaModel();
        view = GetComponent<PlayerManaView>();
    }

    private void Update()
    {
        model.IncreaseMana();
        // Enemy側であれば、viewがアタッチされていないので描画しない。
        if (view != null)
        {
            view.Draw(model);
        }
    }

    public void Init(float amount)
    {
        model.Init(amount);
    }

    public bool IsEnoughtMana(int cost)
    {
        return model.IsEnoughMana(cost);
    }
}
