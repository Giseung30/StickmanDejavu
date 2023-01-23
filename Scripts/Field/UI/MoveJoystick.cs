using UnityEngine;
using System.Collections;

public class MoveJoystick : MonoBehaviour
{
    [Header("Static")]
    public static MoveJoystick moveJoystick; //���� ���� ����

    [Header("State")]
    private bool pointerDown; //Pointer ����
    public Vector2 axis
    {
        get
        {
            if (Vector2.Distance(originRectTransform.position, handleRectTransform.position) < Vector2.Distance(originRectTransform.position, recognitionRectTransform.position)) //Recognition �����̸�
            {
                return Vector2.zero;
            }
            else //Recognition �ܺ��̸�
            {
                return Vector3.Normalize(handleRectTransform.position - originRectTransform.position); //���⺤�� ��ȯ
            }
        }
    } //��
    public int horizontalMoveDirection
    {
        get
        {
            if (axis.x < 0f) //X���� �����̸�
                return -1;
            else if (axis.x > 0f)//X���� ����̸�
                return 1;
            else //X���� �����̸�
                return 0;
        }
    } //���� �̵� ����

    [Header("GUI")]
    public RectTransform handleRectTransform; //Handle�� RectTransform ������Ʈ

    [Header("Pivot")]
    public RectTransform originRectTransform; //Origin�� RectTransform ������Ʈ
    public RectTransform recognitionRectTransform; //Recognition�� RectTransform ������Ʈ
    public RectTransform restrictionRectTransform; //Restriction�� RectTransform ������Ʈ

    /* Pointer�� Trigger �Ǿ��� �� ����Ǵ� �Լ� */
    public void OnTriggerPointer(bool pointerDown)
    {
        this.pointerDown = pointerDown;  //Pointer ���� ����
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

    /* MoveJoystick�� �����ϴ� �ڷ�ƾ �Լ� - PC */
    private IEnumerator ControlMoveJoystickForPC()
    {
        while (true)
        {
            Vector2 axis = new Vector2(Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0, Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0).normalized; //�� ����
            handleRectTransform.position = originRectTransform.position + (Vector3)axis * Vector3.Distance(originRectTransform.position, restrictionRectTransform.position); //Handle ��ġ �̵�

            yield return null;
        }
    }

    /* MoveJoystick�� �����ϴ� �ڷ�ƾ �Լ� - Mobile */
    private IEnumerator ControlMoveJoystickForMobile()
    {
        while (true)
        {
            if (pointerDown) //PointerDown �����̸�
            {
                if (Input.touchCount > 0) //��ġ�� �����ϸ�
                {
                    int touchID = 0;
                    for (int i = 0; i < Input.touchCount; ++i) //��� ��ġ�� Ž���ϸ鼭
                    {
                        if (Vector2.Distance(originRectTransform.position, Input.touches[i].position) < Vector2.Distance(originRectTransform.position, Input.touches[touchID].position)) //���� ����� ��ġ��
                        {
                            touchID = i; //����
                        }
                    }

                    if (Vector2.Distance(originRectTransform.position, Input.touches[touchID].position) < Vector2.Distance(originRectTransform.position, restrictionRectTransform.position)) //��ġ�� Restriction ���ο� ������
                    {
                        handleRectTransform.position = Input.touches[touchID].position; //��ġ ��ġ�� Handle �̵�
                    }
                    else //��ġ�� Restriction �ܺο� ������
                    {
                        handleRectTransform.position = (Vector2)originRectTransform.position + (Input.touches[touchID].position - (Vector2)originRectTransform.position).normalized * Vector2.Distance(originRectTransform.position, restrictionRectTransform.position); //��ġ ���� ���ͷ� Handle �̵�
                    }
                }
            }
            else //PointerUp �����̸�
            {
                handleRectTransform.position = originRectTransform.position; //Origin���� Handle �̵�
            }

            yield return null;
        }
    }
}