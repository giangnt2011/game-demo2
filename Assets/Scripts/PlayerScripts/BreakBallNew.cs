using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBallNew : MonoBehaviour
{
    [SerializeField] private GameObject breakBallFrefab;
    private int count = 0;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ball"))
        {
            count++;
            if (count > 1)
            {
                StartCoroutine(BreakFirstBall(collision.gameObject));
            }
            else
            {
                StartCoroutine(BreakOtherBall(collision.gameObject));
            }
        }
    }
    IEnumerator BreakFirstBall(GameObject ball)
    {
        yield return new WaitForSeconds(5f);
        Instantiate(breakBallFrefab, transform.position, new Quaternion(0, 1, 1, 1));
        ball.SetActive(false);
    }
    IEnumerator BreakOtherBall(GameObject ball)
    {
        yield return new WaitForSeconds(1f);
        Instantiate(breakBallFrefab, transform.position, new Quaternion(0, 1, 1, 1));
        ball.SetActive(false);
    }
}
