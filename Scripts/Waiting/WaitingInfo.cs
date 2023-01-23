using UnityEngine;

public static class WaitingInfo
{
    public static int stage
    {
        get
        {
            return AntiCheatManager.stageSecured;
        }
        set
        {
            AntiCheatManager.stageSecured = Mathf.Clamp(value, 0, 99999);
        }
    } //단계

    public static bool autoSave; //자동 저장 여부
}