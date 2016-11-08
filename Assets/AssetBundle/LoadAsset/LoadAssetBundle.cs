using UnityEngine;
using System.Collections;
using AssetBundles;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
//using AppConfig;
//using AppUsMobile.Modules.Sound;
//using Us.Mobile.CoreBase.Extention;
using System;
using AppConfig;
using emsandacom.Popup;

public class LoadAssetBundle : MonoBehaviour {
    private static LoadAssetBundle instance;

    public string sceneAssetBundle;
    public string sceneName;
    public string AssetBundleURL = /*"http://101.99.3.131:9999/"*/ /*"https://choibaidoithuong.org/"*/ /*"http://52.74.154.183:85/"*/ "";
    public Text txtMsg;
    //public Image image;
    //public Slider Progress;
    //public Image imgTest;
    private bool IsChecking = false;
    //public Image bg_splash;
    //public Image lady;
    //public Image thanbai;
    //public Image xoay;
    //public Image bgChagneScene;

    void Awake() {
        instance = this;
        ClientConfig.InitClient();
        
    }

    //public IEnumerator InitBundle()
    IEnumerator Start() {
        
        yield return StartCoroutine(Initialize());
        //bgChagneScene.gameObject.SetActive(true);
        LoadScene(sceneAssetBundle, sceneName, HideScene);
    }

    private void HideScene() {
        //bg_splash.DOFade(0, 1);
        //lady.DOFade(0, 1);
        //thanbai.DOFade(0, 1);
        //xoay.DOFade(0, 1).OnComplete(FadeXoayFinish);
        PopupAndLoadingScript.instance.LoadPopupAndLoading();
        //LoginControl.instance.MoveFormLogin();
        //if (LoadAssetView.Instance != null)
        //LoadAssetView.Instance.HideScene ();
        SceneManager.UnloadScene("load");
    }

    // Initialize the downloading url and AssetBundleManifest object.
    protected IEnumerator Initialize() {
        // Don't destroy this gameObject as we depend on it to run the loading script.
        DontDestroyOnLoad(gameObject);

        // With this code, when in-editor or using a development builds: Always use the AssetBundle Server
        // (This is very dependent on the production workflow of the project. 
        // 	Another approach would be to make this configurable in the standalone player.)
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        //AssetBundleManager.SetDevelopmentAssetBundleServer ();
        AssetBundleManager.SetSourceAssetBundleURL(AssetBundleURL);
#else
		// Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:
		AssetBundleManager.SetSourceAssetBundleURL(Application.dataPath + "/");
		// Or customize the URL based on your deployment or configuration
		AssetBundleManager.SetSourceAssetBundleURL(AssetBundleURL);
#endif

        // Initialize AssetBundleManifest which loads the AssetBundleManifest object.
        var request = AssetBundleManager.Initialize();

        if (request != null)
            yield return StartCoroutine(request);
    }

    internal static void LoadSprite(Image image, string assetBundleName, string assetName, UnityAction loadDoneCallback = null, UnityAction loadErrorCallback = null
        ) {
#if UNITY_EDITOR
        if (AssetBundleManager.SimulateAssetBundleInEditor)
            LoadAssetBundle.instance.StartCoroutine(LoadAssetBundle.instance.InstantiateTextureAsync(image, assetBundleName, assetName, loadDoneCallback, loadErrorCallback));
        else
            LoadAssetBundle.instance.StartCoroutine(LoadAssetBundle.instance.InstantiateSpritetAsync(image, assetBundleName, assetName, loadDoneCallback, loadErrorCallback));
#else
		LoadAssetBundle.instance.StartCoroutine(LoadAssetBundle.instance.InstantiateSpritetAsync(image, assetBundleName, assetName, loadDoneCallback, loadErrorCallback));
#endif
    }

    internal static void LoadPrefab(string assetBundleName, string assetName, UnityAction<GameObject> loadDoneCallback = null, UnityAction loadErrorCallback = null) {
        try {
#if UNITY_EDITOR
            LoadAssetBundle.instance.StartCoroutine(LoadAssetBundle.instance.InstantiatePrefabtAsync(assetBundleName, assetName, loadDoneCallback, loadErrorCallback));
#else
		LoadAssetBundle.instance.StartCoroutine(LoadAssetBundle.instance.InstantiatePrefabtAsync(assetBundleName, assetName, loadDoneCallback, loadErrorCallback));
#endif
        } catch (Exception e) {
            Debug.LogError("exception : " + e.StackTrace);
        }
    }

    protected IEnumerator InstantiateSpritetAsync(Image image, string assetBundleName, string assetName, UnityAction loadDoneCallback = null, UnityAction loadErrorCallback = null) {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(Sprite));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        // Get the asset.
        var text = request.GetAsset<Sprite>();
        //Debug.Log ("Has sprite? " + (text != null));
        if (text != null && image != null) {
            image.sprite = text;
            if (loadDoneCallback != null) {
                loadDoneCallback();
            }
        } else if (loadErrorCallback != null)
            loadErrorCallback();
    }
    protected IEnumerator InstantiatePrefabtAsync(string assetBundleName, string assetName, UnityAction<GameObject> loadDoneCallback, UnityAction loadErrorCallback) {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        // Get the asset.
        var obj = request.GetAsset<GameObject>();
        if (obj != null) {
            // GameObject.Instantiate(obj);
            if (loadDoneCallback != null) {
                loadDoneCallback(GameObject.Instantiate(obj));
            } else
                GameObject.Instantiate(obj);
        } else if (loadErrorCallback != null)
            loadErrorCallback();
    }

    protected IEnumerator InstantiateTextureAsync(Image image, string assetBundleName, string assetName, UnityAction loadDoneCallback = null, UnityAction loadErrorCallback = null) {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(Texture2D));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        // Get the asset.
        var text = request.GetAsset<Texture2D>();
        //Debug.Log ("Has sprite? " + (text != null));
        if (text != null) {
            var sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;
            if (loadDoneCallback != null) {
                loadDoneCallback();
            }
        } else if (loadErrorCallback != null)
            loadErrorCallback();
    }

    private IEnumerator CheckingDownload() {
        while (IsChecking) {
            var progressValue = AssetBundleManager.DownloadingProgress;
            if (txtMsg != null)
                txtMsg.text = (int)(progressValue * 100) + "%";
            //Progress.value = progressValue;
            yield return new WaitForEndOfFrame();
        }
    }

    internal static void LoadAdditiveScene(string sceneBundleName, string sceneName, UnityAction SceneLoadDoneCallback = null, float timedur = 0.2f) {
        if (SceneManager.GetSceneByName(sceneName) == null || !SceneManager.GetSceneByName(sceneName).isLoaded) {
            //Debug.Log ("Load new scene");
            LoadScene(sceneBundleName, sceneName, SceneLoadDoneCallback, timedur);
        } else {
            //Debug.Log ("Scene " + sceneName + " is loaded.");
            SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    static bool isLoadScene = false;
    internal static void LoadScene(string sceneBundleName, string sceneName, UnityAction SceneLoadDoneCallback = null, float timedur = 0.2f) {
        //DialogEx.ShowLoading(true);
        if (!isLoadScene)
        {
            if (SceneManager.GetSceneByName(sceneName) == null || !SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                isLoadScene = true;
                //Debug.LogError("SceneManager.GetSceneByName(sceneName) == null " + sceneName);
#if ASSET_BUNDLE
            LoadAssetBundle.instance.StartCoroutine(LoadAssetBundle.instance.InitializeLevelAsync(sceneBundleName, sceneName, SceneLoadDoneCallback, timedur));
#else
                LoadAssetBundle.instance.StartCoroutine(LoadAssetBundle.instance.LoadGameScene(sceneName, SceneLoadDoneCallback, timedur));
#endif
            }
            else
            {
                //Debug.LogError("Scene " + sceneName + " is loaded.");
                //Debug.LogError("Root Count " + SceneManager.GetSceneByName(sceneName).rootCount);
                //Debug.LogError("Root name " + SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0] + " " + SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0] == null);
                //Debug.LogError("Child name " + SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].transform.GetChild(0) + " " + SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].transform.GetChild(0) == null);
                SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].transform.GetChild(0).gameObject.SetActive(true);
                if (SceneLoadDoneCallback != null)
                    SceneLoadDoneCallback();
                //DialogEx.ShowLoading(false);
            }
        }
    }

    private IEnumerator LoadGameScene(string sceneName, UnityAction SceneLoadDoneCallback, float timedur) {
        var result = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        result.allowSceneActivation = false;

        while (!result.isDone) {
            // Loading completed
            if (result.progress == 0.9f) {
                result.allowSceneActivation = true;
            }

            yield return null;
        }
        yield return new WaitForSeconds(timedur);
        if (SceneLoadDoneCallback != null) {
            SceneLoadDoneCallback();
        }
        isLoadScene = false;
        //Debug.LogError("ShowLoading false");
        //DialogEx.ShowLoading(false);

    }

    private IEnumerator InitializeLevelAsync(string sceneBundleName, string sceneName, UnityAction SceneLoadDoneCallback, float timedur) {
        IsChecking = true;
        StartCoroutine(CheckingDownload());
        yield return new WaitForSeconds(timedur);

        //		// Load level from assetBundle.
        AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync(sceneBundleName, sceneName, true);
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        IsChecking = false;
        if (SceneLoadDoneCallback != null)
            SceneLoadDoneCallback();
        isLoadScene = false;
        //DialogEx.ShowLoading(false);
    }

    internal static void UnLoadAllScene() {
        string activeScene = SceneManager.GetActiveScene().name;
        Debug.Log("Unload " + SceneManager.sceneCount + " Scenes, Active scene: " + activeScene);
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            if (!SceneManager.GetSceneAt(i).name.Equals(activeScene)) {
                Debug.Log("Unload scene " + SceneManager.GetSceneAt(i).name);
                SceneManager.UnloadScene(SceneManager.GetSceneAt(i).name);
            }
        }
        Debug.Log("Unload scene " + activeScene);
        SceneManager.UnloadScene(activeScene);
    }

    internal static void UnLoadAllOtherScene(string currentScene) {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            if (!SceneManager.GetSceneAt(i).name.Equals(currentScene)) {
                Debug.Log("Unload scene " + SceneManager.GetSceneAt(i).name);
                SceneManager.UnloadScene(SceneManager.GetSceneAt(i).name);
            }
        }
    }
}
