using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement instance;
        [SerializeField] private GameObject SmokeMovingPrefab;
        [SerializeField] private Transform gameObjectTrsPlayer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform ballCollectionTransform;
        [SerializeField] private Transform ballPool; 
        [SerializeField] private float speed = 10f;
        [SerializeField] private float stepWin;
        [SerializeField] private Slider Sliderslider;
        [SerializeField] private GameObject NextLevel;
        [SerializeField] private GameObject ReplayTxt;
        [SerializeField] private GameObject[] BallPrefabs;
        [SerializeField] private Transform StickmanTrans;
        [SerializeField] private Transform SparkingSmoke;

        public bool win = false;
        public bool RotateCamera = false;
        private bool isMoving = false;
        
        private Touch touch;
        private float deltaTouchBefore = 0f;
        public Vector3 EndPoint;
        //private int frameCount = 0;




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

        private void OnEnable()
        {
            EventDispatcher.Instance.RegisterListener(EventID.LoadLevel, HandleEventLoadLevel);
        }
        private void OnDisable()
        {
            EventDispatcher.Instance.RemoveListener(EventID.LoadLevel, HandleEventLoadLevel);
        }
        private void Update()
        {
            if (ballCollectionTransform.childCount == 0 && win)
            {
                NextLevel.SetActive(true);
            }
            //} else if (ballCollectionTransform.childCount == 0)
            //{
            //    ReplayTxt.SetActive(true);
            //    Time.timeScale = 0f;
            //}
            if (win)
            {
                Win();
                return;
            }
            //frameCount++;
            //Debug.Log(frameCount);
            //if (isMoving && ballCollectionTransform.childCount > 0 && frameCount > 20)
            //{
            //    Debug.Log(frameCount);
            //    GameObject game = Instantiate(SmokeMovingPrefab, ballCollectionTransform.GetChild(ballCollectionTransform.childCount - 1).position,
            //        new Quaternion(0, 1, 1, 1));
            //    Destroy(game, 1f);
            //    frameCount = 0;
            //}
            
            if(Input.GetMouseButtonDown(0))
            {
                EnableMoving(true);
            }

            GetTouchMove();
            if (isMoving)
            {
                MovePlayer();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("EndPoint"))
            {
                RotateCamera = true;
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
        
        private void EnableMoving(bool moving)
        {
            isMoving = moving;
        }

        void MovePlayer()
        {
            var position = transform.position;
            position = Vector3.MoveTowards(position,new Vector3(Sliderslider.value, 0, position.z + 0.5f), speed * Time.deltaTime);
            transform.position = position;
            SetAnimation(AnimState.Run);

        }
        
        private void Win()
        {
            EnableDriving(false); 
            transform.position = Vector3.Lerp(transform.position, EndPoint, stepWin);
        }
        
        public void MoveToWinPoint(Vector3 End)
        {
            win = true;
            StartCoroutine(SwitchAnimation());
        
        }
        public void SetEndPoint(Vector3 endpoint)
        {
            EndPoint = endpoint;
        }

        IEnumerator SwitchAnimation()
        {
            yield return new WaitForSeconds(.8f);
            SetAnimation(AnimState.Dance);
        }
        private void EnableDriving(bool enable)
        {
            active = enable;
        }

        private void HandleEventLoadLevel(object param)
        {
            SparkingSmoke.gameObject.SetActive(false);
            //set vi tri
            LevelController currentLevel = (LevelController)param;
            SetEndPoint(currentLevel.EndPlayer.position);

            ballCollectionTransform.localPosition = Vector3.zero; 
            gameObjectTrsPlayer.localPosition= Vector3.zero;
            StickmanTrans.localPosition = Vector3.up;

            GameObject ballInstantiate = Instantiate(BallPrefabs[Random.Range(0, BallPrefabs.Length)], Vector3.zero, Quaternion.identity);
            transform.position = currentLevel.StartPlayer.position;
            ballInstantiate.transform.SetParent(ballCollectionTransform, true);

            Ball ballScript = ballInstantiate.gameObject.transform.GetComponent<Ball>();
            ballScript.InitBall(gameObjectTrsPlayer, ballCollectionTransform, ballPool);

            // set animation
            SetAnimation(AnimState.Idle);

            win = false;
            RotateCamera = false;
            isMoving= false;
            SparkingSmoke.gameObject.SetActive(true);

            CameraFollow.instance.ResetCamera();


        }
        private void SetAnimation(AnimState state)
        {
            switch (state)
            {
                case (AnimState.Idle):
                    _animator.SetBool("Run", false);
                    _animator.SetBool("Dance", false);
                    break;
                case (AnimState.Run):
                    _animator.SetBool("Run", true);
                    break;
                case (AnimState.Dance):
                    _animator.SetBool("Run", false);
                    _animator.SetBool("Dance", true);
                    break;
                default:
                    break;
            }

        }
    }

    
}
public enum AnimState
{
    Idle,
    Run,
    Dance
}
