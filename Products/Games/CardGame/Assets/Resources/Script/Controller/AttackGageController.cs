using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アタックゲージコントローラ
public class AttackGageController : MonoBehaviour
{
    [HideInInspector] public AttackGageModel model { get; set; }
    public AttackGageView view {get; set;}

    private void Awake()
    {
        model = new AttackGageModel();
        view = GetComponent<AttackGageView>();
    }

    private void Update()
    {
        model.IncreaseAttackGage();
        view.Draw(model);
    }

    public void Init()
    {
        model.Init();
    }
}
