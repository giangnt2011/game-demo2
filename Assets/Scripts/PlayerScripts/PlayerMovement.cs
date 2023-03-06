using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement instance;
        [SerializeField] private GameObject SmokeMovingPrefab;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform ballCollectionTransform;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float stepWin;
        [SerializeField] private Slider Sliderslider;
        bool win = false;
        private bool isMoving = true;
        
        private Touch touch;
        private float deltaTouchBefore = 0f;
        public Vector3 EndPoint;
        private bool active { get; set; }

        // private void Update()
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0,10f), 0.03f);
        // }
        // Update is called once per frame
        
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if (win)
            {
                Win();
                return;
            }
            
            if (isMoving)
            {
                GameObject game = Instantiate(SmokeMovingPrefab, ballCollectionTransform.GetChild(ballCollectionTransform.childCount - 1).position,
                    new Quaternion(0, 1, 1, 1));

                StartCoroutine(destroyPrefab(game));
            }
            
            GetTouchMove();
            if (isMoving)
            {
                MovePlayer();
            }
        }

        IEnumerator destroyPrefab(GameObject game)
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(game);
        }
        private void GetTouchMove()
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                isMoving = true;

                if (touch.phase == TouchPhase.Moved)
                {
                    if (Mathf.Abs(touch.deltaPosition.x - deltaTouchBefore) > 0 )
                    {
                        if(Sliderslider.value < 1 || Sliderslider.value > -1)
                        {
                            if(touch.deltaPosition.x < 0)
                            {
                                Sliderslider.value -= 0.08f;
                            }
                            else
                            {
                                Sliderslider.value += 0.08f;
                            }
                        }
                    }
                }
            }
        }
        
        void MovePlayer()
        {
            var position = transform.position;
            position = Vector3.MoveTowards(position,new Vector3(Sliderslider.value, 0, position.z + 0.5f), speed * Time.deltaTime);
            transform.position = position;
            _animator.SetBool("Run", true);
        }
        
        private void Win()
        {
            EnableDriving(false); 
            transform.position = Vector3.Lerp(transform.position, EndPoint, stepWin);
        }
        
        public void MoveToWinPoint(Vector3 End)
        {
            win = true;
            Debug.Log(win);
            StartCoroutine(SwitchAnimation());
        
        }
        public void SetEndPoint(Vector3 endpoint)
        {
            EndPoint = endpoint;
        }

        IEnumerator SwitchAnimation()
        {
            yield return new WaitForSeconds(.8f);
            _animator.SetBool("Run", false);
            _animator.SetBool("Dance", true);
        }
        private void EnableDriving(bool enable)
        {
            active = enable;
        }

    }
}
