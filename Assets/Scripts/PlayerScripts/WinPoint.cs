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
        if (other.gameObject.CompareTag("Ball"))
        {
            PlayerMovement.instance.MoveToWinPoint(end.position);
        }
    }
}
