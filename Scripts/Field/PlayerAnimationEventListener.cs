using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEventListener : MonoBehaviour
{
    /* Fist */
    public UnityEvent[] fistAttackEvents; //Fist Attack의 Event들
    public void ExcuteFistAttackEvents(int index)
    {
        fistAttackEvents[index].Invoke();
    }

    public UnityEvent[] fistUltEvents; //Fist Ult의 Event들
    public void ExcuteFistUltEvents(int index)
    {
        fistUltEvents[index].Invoke();
    }

    /* Sword */
    public UnityEvent[] swordAttackEvents; //Sword Attack의 Event들
    public void ExcuteSwordAttackEvents(int index)
    {
        swordAttackEvents[index].Invoke();
    }

    public UnityEvent[] swordUltEvents; //Sword Ult의 Event들
    public void ExcuteSwordUltEvents(int index)
    {
        swordUltEvents[index].Invoke();
    }

    /* Gun */
    public UnityEvent[] gunAttackEvents; //Gun Attack의 Event들
    public void ExcuteGunAttackEvents(int index)
    {
        gunAttackEvents[index].Invoke();
    }

    public UnityEvent[] gunUltEvents; //Gun Ult의 Event들
    public void ExcuteGunUltEvents(int index)
    {
        gunUltEvents[index].Invoke();
    }

    /* Sniper */
    public UnityEvent[] sniperAttackEvents; //Sniper Attack의 Event들
    public void ExcuteSniperAttackEvents(int index)
    {
        sniperAttackEvents[index].Invoke();
    }

    public UnityEvent[] sniperUltEvents; //Sniper Ult의 Event들
    public void ExcuteSniperUltEvents(int index)
    {
        sniperUltEvents[index].Invoke();
    }

    /* Bazooka */
    public UnityEvent[] bazookaAttackEvents; //Bazooka Attack의 Event들
    public void ExcuteBazookaAttackEvents(int index)
    {
        bazookaAttackEvents[index].Invoke();
    }

    public UnityEvent[] bazookaUltEvents; //Bazooka Ult의 Event들
    public void ExcuteBazookaUltEvents(int index)
    {
        bazookaUltEvents[index].Invoke();
    }

    /* Wizard */
    public UnityEvent[] wizardAttackEvents; //Wizard Attack의 Event들
    public void ExcuteWizardAttackEvents(int index)
    {
        wizardAttackEvents[index].Invoke();
    }

    public UnityEvent[] wizardUltEvents; //Wizard Ult의 Event들
    public void ExcuteWizardUltEvents(int index)
    {
        wizardUltEvents[index].Invoke();
    }
}