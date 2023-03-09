using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour
{

    [SerializeField] private GameObject ReplayTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hi");
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hi2");
            ReplayTxt.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
