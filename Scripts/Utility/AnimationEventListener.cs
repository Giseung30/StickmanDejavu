using UnityEngine;
using UnityEngine.Events;

public class AnimationEventListener : MonoBehaviour
{
    public UnityEvent[] events; //실행할 이벤트들

    /* 이벤트를 실행하는 함수 */
    public void ExcuteEvent(int index)
    {
        events[index].Invoke();
    }
}