using UnityEngine;

public class MenuState : ISceneState
{
    public MenuState(SceneStateController Controller) : base(Controller) {
        this.StateName = "01_Menu";
    }

    public override void StateBegin()
    {
#if UNITY_EDITOR
        Debug.Log(StateName + " Begin");
#endif
    }

    public override void StateUpdate()
    {
#if UNITY_EDITOR
        Debug.Log(StateName + " Update");
#endif
    }

    public override void StateEnd()
    {
#if UNITY_EDITOR
        Debug.Log(StateName + " End");
#endif
    }
}
