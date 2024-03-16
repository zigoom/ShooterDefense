using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMove : MonoBehaviour
{
    // 필요속성 : 이동속도
    public float speed = 10;

    // 필요속성 : Character Conetroller 컴포넌트
    CharacterController cc;


    // 필요속성 : 중력값, 수직속도
    public float gravity = -20;
    float yVelocity = 0;

    // 필요속성 : 점프파워
    float jumpPower = 6;

    //점프관련 변수
    public int canJump = 2;
    public int jumpCnt = 0;


    // 필요속성 : 회전속도
    public float rotSpeed = 200;

    // 나의 각도를 직접 속성으로 관리하겠다.
    float mx;
    float my;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        Rotation();
        Move();
    }

    void Move()
    {

#if UNITY_ANDROID


        // 사용자가 땅에 있으면 수직속도를 0으로 만들고싶다.
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
            jumpCnt = 0;
        }

        // 1. 점프버튼을 눌렀으니까!!!!
        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            if (jumpCnt < canJump)
            {
                // 3. 점프하고싶다.
                //  -> 수직속도에 값을 준다.
                yVelocity = jumpPower;
                jumpCnt++;
            }
        }

        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Debug.LogFormat("점프버튼을 눌렀습니다 2 ");
        //}
        //if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    Debug.LogFormat("점프버튼을 눌렀습니다 3 ");
        //}
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Debug.LogFormat("점프버튼을 눌렀습니다 4 ");
        //}

        Vector3 dir  = new Vector3();

        if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
        {
            //터치패드의 터치한 2D 좌표값
            Vector2 touchPos = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            Debug.LogFormat("Touch position = {0}", touchPos);

            dir = new Vector3(touchPos.x, 0, touchPos.y);
            dir = Camera.main.transform.TransformDirection(dir);
        }

        // 수직속도 구하기 v = v0 + at
        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;
        // 자료형 변수
        // 3. 이동하고 싶다.
        // P = P0 + vt
        //transform.position += dir * speed * Time.deltaTime;
        cc.Move(dir * speed * Time.deltaTime);
#endif

#if UNITY_EDITOR_WIN

        // 사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
        // 1. 사용자의 입력에따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 방향이 필요
        //Vector3 dir = Vector3.right * h + Vector3.forward * v;
        Vector3 dir2 = new Vector3(h, 0, v);
        // dir 방향을 카메라가 바라보는 시점에서의 방향으로 변경해야 한다.
        dir2 = Camera.main.transform.TransformDirection(dir2);

        // 사용자가 땅에 있으면 수직속도를 0으로 만들고싶다.
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
            jumpCnt = 0;
        }

        // 1. 점프버튼을 눌렀으니까!!!!
        if (Input.GetButtonDown("Jump"))
        {
            // 2. 점프카운트가 두번 미만일때
            if (jumpCnt < canJump)
            {
                // 3. 점프하고싶다.
                //  -> 수직속도에 값을 준다.
                yVelocity = jumpPower;
                jumpCnt++;
            }
        }

        // 수직속도 구하기 v = v0 + at
        yVelocity += gravity * Time.deltaTime;


        dir2.y = yVelocity;
        // 자료형 변수
        // 3. 이동하고 싶다.
        // P = P0 + vt
        //transform.position += dir * speed * Time.deltaTime;
        cc.Move(dir2 * speed * Time.deltaTime);
#endif
    }
    void Rotation()
    {
#if UNITY_EDITOR_WIN
        // 사용자의 마우스 입력에 따라 물체를 회전시키고 싶다.
        // 1. 사용자의 입력에 따라
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        // 2. 각도 만들기
        // P = P0 + vt
        mx += h * rotSpeed * Time.deltaTime;
        //my += v * rotSpeed * Time.deltaTime;

        // 3. 회전 각도 제어
        my = Mathf.Clamp(my, -60, 60);

        // 4. 내 각도에 적용하기
        transform.eulerAngles = new Vector3(-my, mx, 0);
#endif
    }
}
