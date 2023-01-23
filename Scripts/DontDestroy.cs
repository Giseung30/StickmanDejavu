using UnityEngine;

public class DontDestroy : MonoBehaviour 
{
    [Header("Static")]
    public static DontDestroy dontDestroy; //���� ���� ����

    private void Awake()
    {
        if(dontDestroy) //DontDestroy ������Ʈ�� �����ϸ�
        {
            Destroy(gameObject); //������Ʈ �ı�
        }
        else //DontDestroy ������Ʈ�� �������� ������
        {
            dontDestroy = this;
            DontDestroyOnLoad(gameObject);//���� ����
        }
    }
}