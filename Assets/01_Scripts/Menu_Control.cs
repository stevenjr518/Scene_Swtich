using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Control : MonoBehaviour
{
    public void ToScene01() {
        Scene_Manager.Instance.SceneSwitchBegin(new Scene01(new SceneStateController()), "Scene01", true);
    }

    public void ToScene02()
    {
        Scene_Manager.Instance.SceneSwitchBegin(new Scene02(new SceneStateController()), "Scene02", true);
    }
}
