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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Debug.Log("col");
            collision.gameObject.TryGetComponent<Ball>(out var ball);
            if (!ball.hasCollided)
            {
                Vector3 lastChildPosition = ballCollection.GetChild(ballCollection.childCount - 1).transform.position;

                Vector3 newGamePo = gameObjectTransform.position;
                newGamePo.y += 1f;
                gameObjectTransform.position = newGamePo;
            
                collision.gameObject.transform.SetParent(ballCollection, true);

                collision.gameObject.transform.position = lastChildPosition;
            
                ball.hasCollided = true;
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            collision.gameObject.TryGetComponent<Wall>(out var wall);
            if (!wall.hasCollided)
            {
                transform.SetParent(ballPool, true);
            }
        }
    }
}
