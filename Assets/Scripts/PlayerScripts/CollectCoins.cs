using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoins : MonoBehaviour
{
    [SerializeField] private Text Score;
    private int CoinScore = 0;

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.LoadLevel, HandleEventLoadLevel);
    }
    private void OnDisable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.LoadLevel, HandleEventLoadLevel);
    }

    private void HandleEventLoadLevel(object param)
    {
        int oldScore = PlayerPrefs.GetInt("score");
        UIController.instance.SetScoreTxt(oldScore);
        CoinScore = oldScore;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.TryGetComponent<Coin>(out var coin);
            if (!coin.hasCollided)
            {
                CoinScore++;
                UIController.instance.SetScoreTxt(CoinScore);
            }
        }
    }
}
