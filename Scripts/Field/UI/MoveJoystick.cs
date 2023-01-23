using UnityEngine;
using System.Collections;

public class MoveJoystick : MonoBehaviour
{
    [Header("Static")]
    public static MoveJoystick moveJoystick; //전역 참조 변수

    [Header("State")]
    private bool pointerDown; //Pointer 상태
    public Vector2 axis
    {
        get
        {
            if (Vector2.Distance(originRectTransform.position, handleRectTransform.position) < Vector2.Distance(originRectTransform.position, recognitionRectTransform.position)) //Recognition 내부이면
            {
                return Vector2.zero;
            }
            else //Recognition 외부이면
            {
                return Vector3.Normalize(handleRectTransform.position - originRectTransform.position); //방향벡터 반환
            }
        }
    } //축
    public int horizontalMoveDirection
    {
        get
        {
            if (axis.x < 0f) //X축이 음수이면
                return -1;
            else if (axis.x > 0f)//X축이 양수이면
                return 1;
            else //X축이 원점이면
                return 0;
        }
    } //수평 이동 방향

    [Header("GUI")]
    public RectTransform handleRectTransform; //Handle의 RectTransform 컴포넌트

    [Header("Pivot")]
    public RectTransform originRectTransform; //Origin의 RectTransform 컴포넌트
    public RectTransform recognitionRectTransform; //Recognition의 RectTransform 컴포넌트
    public RectTransform restrictionRectTransform; //Restriction의 RectTransform 컴포넌트

    /* Pointer가 Trigger 되었을 때 실행되는 함수 */
    public void OnTriggerPointer(bool pointerDown)
    {
        this.pointerDown = pointerDown;  //Pointer 상태 저장
    }

    private void Awake()
    {
        moveJoystick = this;
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StartCoroutine(ControlMoveJoystickForPC());
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            StartCoroutine(ControlMoveJoystickForMobile());
        }
    }

    /* MoveJoystick을 조작하는 코루틴 함수 - PC */
    private IEnumerator ControlMoveJoystickForPC()
    {
        while (true)
        {
            Vector2 axis = new Vector2(Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0, Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0).normalized; //축 저장
            handleRectTransform.position = originRectTransform.position + (Vector3)axis * Vector3.Distance(originRectTransform.position, restrictionRectTransform.position); //Handle 위치 이동

            yield return null;
        }
    }

    /* MoveJoystick을 조작하는 코루틴 함수 - Mobile */
    private IEnumerator ControlMoveJoystickForMobile()
    {
        while (true)
        {
            if (pointerDown) //PointerDown 상태이면
            {
                if (Input.touchCount > 0) //터치가 존재하면
                {
                    int touchID = 0;
                    for (int i = 0; i < Input.touchCount; ++i) //모든 터치를 탐색하면서
                    {
                        if (Vector2.Distance(originRectTransform.position, Input.touches[i].position) < Vector2.Distance(originRectTransform.position, Input.touches[touchID].position)) //가장 가까운 터치를
                        {
                            touchID = i; //저장
                        }
                    }

                    if (Vector2.Distance(originRectTransform.position, Input.touches[touchID].position) < Vector2.Distance(originRectTransform.position, restrictionRectTransform.position)) //터치가 Restriction 내부에 있으면
                    {
                        handleRectTransform.position = Input.touches[touchID].position; //터치 위치로 Handle 이동
                    }
                    else //터치가 Restriction 외부에 있으면
                    {
                        handleRectTransform.position = (Vector2)originRectTransform.position + (Input.touches[touchID].position - (Vector2)originRectTransform.position).normalized * Vector2.Distance(originRectTransform.position, restrictionRectTransform.position); //터치 방향 벡터로 Handle 이동
                    }
                }
            }
            else //PointerUp 상태이면
            {
                handleRectTransform.position = originRectTransform.position; //Origin으로 Handle 이동
            }

            yield return null;
        }
    }
}