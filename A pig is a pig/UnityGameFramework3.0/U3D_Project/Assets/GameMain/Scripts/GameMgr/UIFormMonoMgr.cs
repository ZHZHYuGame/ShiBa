using UnityEngine;

public class UIFormMonoMgr : MonoSingleton<UIFormMonoMgr>
{

    [SerializeField] private Camera uiCamera;
    
    public Camera UICamera => uiCamera;
}
