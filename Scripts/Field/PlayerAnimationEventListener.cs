using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEventListener : MonoBehaviour
{
    /* Fist */
    public UnityEvent[] fistAttackEvents; //Fist Attack�� Event��
    public void ExcuteFistAttackEvents(int index)
    {
        fistAttackEvents[index].Invoke();
    }

    public UnityEvent[] fistUltEvents; //Fist Ult�� Event��
    public void ExcuteFistUltEvents(int index)
    {
        fistUltEvents[index].Invoke();
    }

    /* Sword */
    public UnityEvent[] swordAttackEvents; //Sword Attack�� Event��
    public void ExcuteSwordAttackEvents(int index)
    {
        swordAttackEvents[index].Invoke();
    }

    public UnityEvent[] swordUltEvents; //Sword Ult�� Event��
    public void ExcuteSwordUltEvents(int index)
    {
        swordUltEvents[index].Invoke();
    }

    /* Gun */
    public UnityEvent[] gunAttackEvents; //Gun Attack�� Event��
    public void ExcuteGunAttackEvents(int index)
    {
        gunAttackEvents[index].Invoke();
    }

    public UnityEvent[] gunUltEvents; //Gun Ult�� Event��
    public void ExcuteGunUltEvents(int index)
    {
        gunUltEvents[index].Invoke();
    }

    /* Sniper */
    public UnityEvent[] sniperAttackEvents; //Sniper Attack�� Event��
    public void ExcuteSniperAttackEvents(int index)
    {
        sniperAttackEvents[index].Invoke();
    }

    public UnityEvent[] sniperUltEvents; //Sniper Ult�� Event��
    public void ExcuteSniperUltEvents(int index)
    {
        sniperUltEvents[index].Invoke();
    }

    /* Bazooka */
    public UnityEvent[] bazookaAttackEvents; //Bazooka Attack�� Event��
    public void ExcuteBazookaAttackEvents(int index)
    {
        bazookaAttackEvents[index].Invoke();
    }

    public UnityEvent[] bazookaUltEvents; //Bazooka Ult�� Event��
    public void ExcuteBazookaUltEvents(int index)
    {
        bazookaUltEvents[index].Invoke();
    }

    /* Wizard */
    public UnityEvent[] wizardAttackEvents; //Wizard Attack�� Event��
    public void ExcuteWizardAttackEvents(int index)
    {
        wizardAttackEvents[index].Invoke();
    }

    public UnityEvent[] wizardUltEvents; //Wizard Ult�� Event��
    public void ExcuteWizardUltEvents(int index)
    {
        wizardUltEvents[index].Invoke();
    }
}