using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCounter : Photon.MonoBehaviour, IPointerClickHandler
{
    private int counter1;
    private int counter2;
    [SerializeField] private Text t;

    void Start()
    {
        counter1 = 0;
        counter2 = 100;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        counter1++;
        counter2--;
        t.text = counter1.ToString() + "/" + counter2;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("よばれているぜ" + counter1 + "/" + counter2);
        if (stream.isWriting)
        {
            stream.SendNext(counter1.ToString());
            stream.SendNext(counter2.ToString());
        }
        else
        {
            counter1 = int.Parse(stream.ReceiveNext().ToString());
            counter2 = int.Parse(stream.ReceiveNext().ToString());
            t.text = counter1.ToString() + "/" + counter2.ToString();
        }
    }
}
