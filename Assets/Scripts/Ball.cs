using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool hasCollided = false;
    [SerializeField] private Transform gameObjectTransform;
    [SerializeField] private Transform ballCollection;
    [SerializeField] private Transform ballPool;
    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Debug.Log("col");
            collision.gameObject.TryGetComponent<Ball>(out var ball);
            if (!ball.hasCollided)
            {
                int ballColChildCount = ballCollection.childCount;
                Vector3 lastChildPosition = ballCollection.GetChild(ballCollection.childCount - 1).transform.position;

                Vector3 newGamePo = gameObjectTransform.position;
                newGamePo.y += 1f;
                gameObjectTransform.position = newGamePo;
            
                collision.gameObject.transform.SetParent(ballCollection, true);

                collision.gameObject.transform.position = lastChildPosition;
            
                ball.hasCollided = true;

                _animator.enabled = true;
                if(ballColChildCount % 2 == 0)
                {
                    SetAnimBallScroll(BallAnimState.ScrollBack);
                }
                
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            collision.gameObject.TryGetComponent<Wall>(out var wall);
            if (!wall.hasCollided)
            {
                transform.SetParent(ballPool, true);
                _animator.enabled = false;
            }
            
        }
    }

    public void InitBall(Transform gameObjectTrans, Transform ballCol, Transform ballP)
    {
        gameObjectTransform = gameObjectTrans;
        ballCollection = ballCol;
        ballPool = ballP;

    }
    private void SetAnimBallScroll(BallAnimState state)
    {
        switch (state)
        {
            case (BallAnimState.Scroll):
                _animator.SetBool("RollBack", false);
                break;
            case (BallAnimState.ScrollBack):
                _animator.SetBool("RollBack", true);
                break;
            default:
                break;
        }
    }
}

public enum BallAnimState
{
    Scroll,
    ScrollBack
}
