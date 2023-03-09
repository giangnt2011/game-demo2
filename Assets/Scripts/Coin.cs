using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float count = 0.0f;
    Rigidbody m_Rigidbody;
    Vector3[] point = new Vector3[3];
    private bool hasCollidedPrivate = false;
    public bool hasCollided = false;
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hasCollidedPrivate)
        {
            if (count < 1.0f)
            {
                count += 1.0f * Time.deltaTime;

                Vector3 m1 = Vector3.Lerp(point[0], point[1], count);
                Vector3 m2 = Vector3.Lerp(point[1], point[2], count);
                m_Rigidbody.transform.position = Vector3.Lerp(m1, m2, count);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            point[0] = transform.position;
            point[2] = transform.position - new Vector3(0, 0, 7);
            point[1] = point[0] + (point[2] - point[0]) / 2 + Vector3.up * 7.0f;
            hasCollidedPrivate = true;
            var constr = m_Rigidbody.constraints;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        }
    }
}
