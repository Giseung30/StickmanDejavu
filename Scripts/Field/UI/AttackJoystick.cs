using UnityEngine;
using System.Collections;

public class AttackJoystick : MonoBehaviour
{
    [Header("Static")]
    public static AttackJoystick attackJoystick; //���� ���� ����

    [Header("State")]
    private bool pointerDown; //Pointer ����
    public float axis
    {
        get
        {
            if(Mathf.Abs(originRectTransform.position.x - handleRectTransform.position.x) < Mathf.Abs(originRectTransform.position.x - recognitionRectTransform.position.x)) //Recognition �����̸�
            {
                return 0f;
            }
            else //Recognition �ܺ��̸�
            {
                return Mathf.Sign(handleRectTransform.position.x - originRectTransform.position.x); //���⺤�� ��ȯ
            }
        }
    } //��

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

    /* AttackJoystick�� �����ϴ� �ڷ�ƾ �Լ� - PC */
    private IEnumerator ControlAttackJoystickForPC()
    {
        while (true)
        {
            float axis = Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0; //�� ����
            handleRectTransform.position = new Vector3(originRectTransform.position.x + axis * Mathf.Abs(originRectTransform.position.x - restrictionRectTransform.position.x), handleRectTransform.position.y, handleRectTransform.position.z); //Handle ��ġ �̵�

            yield return null;
        }
    }

    /* AttackJoystick�� �����ϴ� �ڷ�ƾ �Լ� - Mobile */
    private IEnumerator ControlAttackJoystickForMobile()
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

                    if(Mathf.Abs(originRectTransform.position.x - Input.touches[touchID].position.x) < Mathf.Abs(originRectTransform.position.x - restrictionRectTransform.position.x)) //��ġ�� Restriction ���ο� ������
                    {
                        handleRectTransform.position = new Vector3(Input.touches[touchID].position.x, handleRectTransform.position.y, handleRectTransform.position.z); //��ġ ��ġ�� Handle �̵�
                    }
                    else //��ġ�� Restriction �ܺο� ������
                    {
                        handleRectTransform.position = new Vector3(originRectTransform.position.x + Mathf.Sign(Input.touches[touchID].position.x - originRectTransform.position.x) * Mathf.Abs(originRectTransform.position.x - restrictionRectTransform.position.x), handleRectTransform.position.y, handleRectTransform.position.z); //��ġ ���� ���ͷ� Handle �̵�
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