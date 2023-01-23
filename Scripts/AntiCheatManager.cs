using UnityEngine;
using System.Collections;

public class AntiCheatManager : MonoBehaviour
{
    [Header("Static")]
    public static AntiCheatManager instance; //전역 참조 변수

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
    } //돈 Secured
    private static int moneyLock; //돈 Lock
    private static int moneyKey; //돈 Key
    private readonly Pair<int, int> moneyKeyRange = new Pair<int, int>(-100000000, 100000000); //돈 Key 범위

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
    } //수익 Secured
    private static int profitLock; //수익 Lock
    private static int profitKey; //수익 Key
    private readonly Pair<int, int> profitKeyRange = new Pair<int, int>(-100000000, 100000000); //수익 Key 범위

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
    } //처치 Secured
    private static int killLock; //처치 Lock
    private static int killKey; //처치 Key
    private readonly Pair<int, int> killKeyRange = new Pair<int, int>(-100000000, 100000000); //처치 Key 범위

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
    } //단계 Secured
    private static int stageLock = 1; //단계 Lock
    private static int stageKey; //단계 Key
    private readonly Pair<int, int> stageKeyRange = new Pair<int, int>(-100000000, 100000000); //단계 Key 범위

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
    } //현재 킬 수 Secured
    private static int currentKillLock; //현재 킬 수 Lock
    private static int currentKillKey; //현재 킬 수 Key
    private readonly Pair<int, int> currentKillKeyRange = new Pair<int, int>(-100000000, 100000000); //현재 킬 수 Key 범위

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
    } //현재 Money Secured
    private static int currentMoneyLock; //현재 Money Lock
    private static int currentMoneyKey; //현재 Money Key
    private readonly Pair<int, int> currentMoneyKeyRange = new Pair<int, int>(-100000000, 100000000); //현재 Money Key 범위

    [Header("Cache")]
    private readonly WaitForSeconds keyChangeInterval = new WaitForSeconds(2f); //Key 변경 간격
    private int newKey; //새로운 Key

    private void Start()
    {
        if (!instance) instance = this;

        StartCoroutine(ChangeKey());
    }

    /* Key를 변경하는 코루틴 함수 */
    private IEnumerator ChangeKey()
    {
        while (true)
        {
            yield return keyChangeInterval; //Key 변경 대기

            newKey = Random.Range(moneyKeyRange.first, moneyKeyRange.second); //새로운 Key 저장
            moneyLock = moneySecured - newKey; //돈 Lock 지정
            moneyKey = newKey; //돈 Key 지정

            newKey = Random.Range(profitKeyRange.first, profitKeyRange.second); //새로운 Key 저장
            profitLock = profitSecured - newKey; //수익 Lock 지정
            profitKey = newKey; //수익 Key 지정

            newKey = Random.Range(killKeyRange.first, killKeyRange.second); //새로운 Key 저장
            killLock = killSecured - newKey; //처치 Lock 지정
            killKey = newKey; //처치 Key 지정

            newKey = Random.Range(stageKeyRange.first, stageKeyRange.second); //새로운 Key 저장
            stageLock = stageSecured - newKey; //단계 Lock 지정
            stageKey = newKey; //단계 Key 지정

            newKey = Random.Range(currentKillKeyRange.first, currentKillKeyRange.second); //새로운 Key 저장
            currentKillLock = currentKillSecured - newKey; //현재 킬 수 Lock 지정
            currentKillKey = newKey; //현재 킬 수 Key 지정

            newKey = Random.Range(currentMoneyKeyRange.first, currentMoneyKeyRange.second); //새로운 Key 저장
            currentMoneyLock = currentMoneySecured - newKey; //현재 Money Lock 지정
            currentMoneyKey = newKey; //현재 Money Key 지정
        }
    }
}