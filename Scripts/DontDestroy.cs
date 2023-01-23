using UnityEngine;

public class DontDestroy : MonoBehaviour 
{
    [Header("Static")]
    public static DontDestroy dontDestroy; //전역 참조 변수

    private void Awake()
    {
        if(dontDestroy) //DontDestroy 오브젝트가 존재하면
        {
            Destroy(gameObject); //오브젝트 파괴
        }
        else //DontDestroy 오브젝트가 존재하지 않으면
        {
            dontDestroy = this;
            DontDestroyOnLoad(gameObject);//제거 금지
        }
    }
}