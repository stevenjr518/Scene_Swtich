using System.Collections;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    SceneStateController m_SceneStateController;
    ISceneState toState;
    private static Scene_Manager instance;
    public static Scene_Manager Instance {
        get
        {
            return instance;
        }
    }
    private GameObject canvas;
    public RectTransform fadingBG_prefab;
    public RectTransform loading_prefab;
    public CanvasGroup fadingBG;
    public CanvasGroup loading;
    private Image loadingBar;
    private bool showLoading;
    private float fadingSpeed = 1;
    private float iconTime = 2;
    private float target;

    private Scene_Manager() { }

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(Instance.gameObject);
            Resources.UnloadUnusedAssets();
        }
        instance = this;
        m_SceneStateController = new SceneStateController();
        SetupCanvas();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke("ToMenu", iconTime);
        enabled = false;
    }

    private void SetupCanvas()
    {
        canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            fadingBG = Instantiate(fadingBG_prefab, canvas.transform).GetComponent<CanvasGroup>();
            loading = Instantiate(loading_prefab, canvas.transform).GetComponent<CanvasGroup>();
            loadingBar = UIGet.GetUIComponent<Image>(canvas, "Bar");
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("No canvas found in current scene");
#endif
        }
    }

    public void SceneSwitchBegin(ISceneState state, string sceneName, bool showLoading) {
        int idx = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (idx >= 0) {
            this.showLoading = showLoading;
            toState = state;
            if (fadingBG != null)
            {
                StartCoroutine(FadeOut(sceneName));
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("Scene " + sceneName + " doesn't exist");
#endif
        }
    }

    public void Fade_In()
    {
        StartCoroutine(FadeIn());
    }

    public void Fade_Out()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut(string sceneName = "") {
        float a = fadingBG.alpha;
        fadingBG.blocksRaycasts = true;
        if (a <= 0) {
            do
            {
                yield return new WaitForEndOfFrame();
                a += Time.deltaTime * fadingSpeed;
                fadingBG.alpha = a;
                if (a >= 1) {
                    if (sceneName != "")
                    {
                        m_SceneStateController.SetState(toState, sceneName);
                        if (showLoading) { ShowLoading(); } else { m_SceneStateController.loadingScene.allowSceneActivation = true; }
                        yield return new WaitUntil(() => m_SceneStateController.m_bRunBegin);
                        StartCoroutine(FadeIn());
                    }
                }
            } while (a < 1);
        }
    }

    public async void ShowLoading()
    {
        enabled = true;
        loading.alpha = 1;
        loadingBar.fillAmount = 0;
        target = 0;
        do
        {
            target = m_SceneStateController.loadingScene.progress;
        } while (m_SceneStateController.loadingScene.progress < 0.9f);
        await Task.Delay(1000);
        m_SceneStateController.loadingScene.allowSceneActivation = true;
        enabled = false;
    }

    private void Update()
    {
        loadingBar.fillAmount = Mathf.MoveTowards(loadingBar.fillAmount, target + 0.2f, 3 * Time.deltaTime);
    }

    public IEnumerator FadeIn()
    {
        if(canvas == null)
        {
            SetupCanvas();
        }
        fadingBG.alpha = 1;
        float a = fadingBG.alpha;
        if (a >= 1)
        {
            do
            {
                yield return new WaitForEndOfFrame();
                a -= Time.deltaTime * fadingSpeed;
                fadingBG.alpha = a;
                if (a <= 0)
                {

                }
            } while (a > 0);
        }
        fadingBG.blocksRaycasts = false;
    }


    private void ToMenu() {
        SceneSwitchBegin(new MenuState(new SceneStateController()), "01_Menu", false);
    }
}
