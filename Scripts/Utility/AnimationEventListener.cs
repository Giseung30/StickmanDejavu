using UnityEngine;
using UnityEngine.Events;

public class AnimationEventListener : MonoBehaviour
{
    public UnityEvent[] events; //������ �̺�Ʈ��

    /* �̺�Ʈ�� �����ϴ� �Լ� */
    public void ExcuteEvent(int index)
    {
        events[index].Invoke();
    }
}