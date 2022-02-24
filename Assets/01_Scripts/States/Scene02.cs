using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene02 : ISceneState
{
    public Scene02(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "Scene02";
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
