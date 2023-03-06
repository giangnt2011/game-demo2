using PlayerScripts;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    [SerializeField] Transform end;
    private const string PlayerTag = "Player";
    private void Start()
    {
        PlayerMovement.instance.SetEndPoint(end.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (!other.gameObject.CompareTag("Ball")) return;
        // Debug.Log("dam vao winpoint");
        // PlayerMovement.instance.MoveToWinPoint(end.transform.position);
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log(other.gameObject.tag);
            PlayerMovement.instance.MoveToWinPoint(end.position);
        }
    }
}
