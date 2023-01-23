using UnityEngine;
using System.Collections;

public class AttackJoystick : MonoBehaviour
{
    [Header("Static")]
    public static AttackJoystick attackJoystick; //전역 참조 변수

    [Header("State")]
    private bool pointerDown; //Pointer 상태
    public float axis
    {
        get
        {
            if(Mathf.Abs(originRectTransform.position.x - handleRectTransform.position.x) < Mathf.Abs(originRectTransform.position.x - recognitionRectTransform.position.x)) //Recognition 내부이면
            {
                return 0f;
            }
            else //Recognition 외부이면
            {
                return Mathf.Sign(handleRectTransform.position.x - originRectTransform.position.x); //방향벡터 반환
            }
        }
    } //축

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
        attackJoystick = this;
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            StartCoroutine(ControlAttackJoystickForPC());
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            StartCoroutine(ControlAttackJoystickForMobile());
        }
    }

    /* AttackJoystick을 조작하는 코루틴 함수 - PC */
    private IEnumerator ControlAttackJoystickForPC()
    {
        while (true)
        {
            float axis = Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0; //축 저장
            handleRectTransform.position = new Vector3(originRectTransform.position.x + axis * Mathf.Abs(originRectTransform.position.x - restrictionRectTransform.position.x), handleRectTransform.position.y, handleRectTransform.position.z); //Handle 위치 이동

            yield return null;
        }
    }

    /* AttackJoystick을 조작하는 코루틴 함수 - Mobile */
    private IEnumerator ControlAttackJoystickForMobile()
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

                    if(Mathf.Abs(originRectTransform.position.x - Input.touches[touchID].position.x) < Mathf.Abs(originRectTransform.position.x - restrictionRectTransform.position.x)) //터치가 Restriction 내부에 있으면
                    {
                        handleRectTransform.position = new Vector3(Input.touches[touchID].position.x, handleRectTransform.position.y, handleRectTransform.position.z); //터치 위치로 Handle 이동
                    }
                    else //터치가 Restriction 외부에 있으면
                    {
                        handleRectTransform.position = new Vector3(originRectTransform.position.x + Mathf.Sign(Input.touches[touchID].position.x - originRectTransform.position.x) * Mathf.Abs(originRectTransform.position.x - restrictionRectTransform.position.x), handleRectTransform.position.y, handleRectTransform.position.z); //터치 방향 벡터로 Handle 이동
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