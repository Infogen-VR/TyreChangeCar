using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoad : MonoBehaviour
{
    /*public string SecondSceneName;

    void Start()
    {
        StartCoroutine(loadAndPlayScene());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator loadAndPlayScene()
    {

        yield return new WaitForSeconds(5f);
        // SceneManager.UnloadSceneAsync(firstSceneInstance);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SecondSceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }*/
    public string secondSceneAddress;

    void Start()
    {
        // Start the loading process
        StartCoroutine(LoadSecondSceneAndDestroyCurrentScene());
    }

    IEnumerator LoadSecondSceneAndDestroyCurrentScene()
    {
        // Load the second scene asynchronously using Addressables
        AsyncOperationHandle<SceneInstance> secondSceneHandle = Addressables.LoadSceneAsync(secondSceneAddress, LoadSceneMode.Single);

        // Wait until the second scene is fully loaded
        while (!secondSceneHandle.IsDone)
        {
            yield return null;
        }

        // Unload the current scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        Debug.Log("Second scene loaded and current scene destroyed successfully!");
    }
}
