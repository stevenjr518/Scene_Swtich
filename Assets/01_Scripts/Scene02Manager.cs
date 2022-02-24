using UnityEngine;

public class Scene02Manager : MonoBehaviour
{
    public void ToMenu()
    {
        Scene_Manager.Instance.SceneSwitchBegin(new MenuState(new SceneStateController()), "01_Menu", true);
    }
}
