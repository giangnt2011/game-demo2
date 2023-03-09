using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Transform StartPlayer;
    public Transform EndPlayer;
    [SerializeField] private Ball[] Balls;
    private Transform BallPoolTransform;
    public void InitBalls(Transform gameObjectTrans, Transform ballCol, Transform ballP)
    {
        BallPoolTransform = ballP;
        foreach (var ball in Balls)
        {
            ball.InitBall(gameObjectTrans, ballCol, ballP);
        }
    }
    private void OnDestroy()
    {
        foreach (var ball in BallPoolTransform.GetComponentsInChildren<Ball>())
        {
            if (ball != null)
            {
                Destroy(ball.gameObject);
            }
        }
    }
}
