using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class BreakBallNew : MonoBehaviour
{
    [SerializeField] private GameObject breakBallFrefab;
    [SerializeField] private Text Score;
    private int count = 0;
    private int plusCoin = 0;
    private string score = "score";
    private int currentScore;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ball"))
        {
            currentScore = UIController.instance.GetScoreTxt();
            count++;

            plusCoin = currentScore + count * 5;

            UIController.instance.SetScoreTxt(plusCoin);

            //scoreText.text = PlayerPrefs.GetInt(score, 0).ToString();
            if (count == 1)
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
        yield return new WaitForSeconds(3f);
        Destroy(Instantiate(breakBallFrefab, transform.position, new Quaternion(0, 1, 1, 1)));
        ball.SetActive(false);
    }
    IEnumerator BreakOtherBall(GameObject ball)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy( Instantiate(breakBallFrefab, transform.position, new Quaternion(0, 1, 1, 1)), 0.5f);
        ball.SetActive(false);
    }
}
