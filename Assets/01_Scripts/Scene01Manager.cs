using UnityEngine;

public class Scene01Manager : MonoBehaviour
{
    public void ToMenu()
    {
        Scene_Manager.Instance.SceneSwitchBegin(new MenuState(new SceneStateController()), "01_Menu", true);
    }
}
