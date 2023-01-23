using UnityEngine;
using System.Collections;

public class AntiCheatManager : MonoBehaviour
{
    [Header("Static")]
    public static AntiCheatManager instance; //���� ���� ����

    public static int moneySecured
    {
        get
        {
            return moneyLock + moneyKey;
        }
        set
        {
            moneyLock += value - moneySecured;
        }
    } //�� Secured
    private static int moneyLock; //�� Lock
    private static int moneyKey; //�� Key
    private readonly Pair<int, int> moneyKeyRange = new Pair<int, int>(-100000000, 100000000); //�� Key ����

    public static int profitSecured
    {
        get
        {
            return profitLock + profitKey;
        }
        set
        {
            profitLock += value - profitSecured;
        }
    } //���� Secured
    private static int profitLock; //���� Lock
    private static int profitKey; //���� Key
    private readonly Pair<int, int> profitKeyRange = new Pair<int, int>(-100000000, 100000000); //���� Key ����

    public static int killSecured
    {
        get
        {
            return killLock + killKey;
        }
        set
        {
            killLock += value - killSecured;
        }
    } //óġ Secured
    private static int killLock; //óġ Lock
    private static int killKey; //óġ Key
    private readonly Pair<int, int> killKeyRange = new Pair<int, int>(-100000000, 100000000); //óġ Key ����

    public static int stageSecured
    {
        get
        {
            return stageLock + stageKey;
        }
        set
        {
            stageLock += value - stageSecured;
        }
    } //�ܰ� Secured
    private static int stageLock = 1; //�ܰ� Lock
    private static int stageKey; //�ܰ� Key
    private readonly Pair<int, int> stageKeyRange = new Pair<int, int>(-100000000, 100000000); //�ܰ� Key ����

    public static int currentKillSecured
    {
        get
        {
            return currentKillLock + currentKillKey;
        }
        set
        {
            currentKillLock += value - currentKillSecured;
        }
    } //���� ų �� Secured
    private static int currentKillLock; //���� ų �� Lock
    private static int currentKillKey; //���� ų �� Key
    private readonly Pair<int, int> currentKillKeyRange = new Pair<int, int>(-100000000, 100000000); //���� ų �� Key ����

    public static int currentMoneySecured
    {
        get
        {
            return currentMoneyLock + currentMoneyKey;
        }
        set
        {
            currentMoneyLock += value - currentMoneySecured;
        }
    } //���� Money Secured
    private static int currentMoneyLock; //���� Money Lock
    private static int currentMoneyKey; //���� Money Key
    private readonly Pair<int, int> currentMoneyKeyRange = new Pair<int, int>(-100000000, 100000000); //���� Money Key ����

    [Header("Cache")]
    private readonly WaitForSeconds keyChangeInterval = new WaitForSeconds(2f); //Key ���� ����
    private int newKey; //���ο� Key

    private void Start()
    {
        if (!instance) instance = this;

        StartCoroutine(ChangeKey());
    }

    /* Key�� �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator ChangeKey()
    {
        while (true)
        {
            yield return keyChangeInterval; //Key ���� ���

            newKey = Random.Range(moneyKeyRange.first, moneyKeyRange.second); //���ο� Key ����
            moneyLock = moneySecured - newKey; //�� Lock ����
            moneyKey = newKey; //�� Key ����

            newKey = Random.Range(profitKeyRange.first, profitKeyRange.second); //���ο� Key ����
            profitLock = profitSecured - newKey; //���� Lock ����
            profitKey = newKey; //���� Key ����

            newKey = Random.Range(killKeyRange.first, killKeyRange.second); //���ο� Key ����
            killLock = killSecured - newKey; //óġ Lock ����
            killKey = newKey; //óġ Key ����

            newKey = Random.Range(stageKeyRange.first, stageKeyRange.second); //���ο� Key ����
            stageLock = stageSecured - newKey; //�ܰ� Lock ����
            stageKey = newKey; //�ܰ� Key ����

            newKey = Random.Range(currentKillKeyRange.first, currentKillKeyRange.second); //���ο� Key ����
            currentKillLock = currentKillSecured - newKey; //���� ų �� Lock ����
            currentKillKey = newKey; //���� ų �� Key ����

            newKey = Random.Range(currentMoneyKeyRange.first, currentMoneyKeyRange.second); //���ο� Key ����
            currentMoneyLock = currentMoneySecured - newKey; //���� Money Lock ����
            currentMoneyKey = newKey; //���� Money Key ����
        }
    }
}