using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBallWin : MonoBehaviour
{
    public static BreakBallWin instance;
    [SerializeField] private Transform ballsCollection;
    [SerializeField] private GameObject breakBallFrefab;
    public bool breakBall = false;
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("WinPoint"))
        {
            StartCoroutine(BreakBall());
        }
    }
    IEnumerator BreakBall()
    {
        yield return new WaitForSeconds(10f);
        breakBall = true;
    }
}
