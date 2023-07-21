using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoad : MonoBehaviour
{
    public string SecondSceneName;

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

    }
    /*  public string firstSceneAddress;
      public string secondSceneAddress;

      void Start()
      {
          // Start the loading process
          StartCoroutine(LoadScenes());
      }

      IEnumerator LoadScenes()
      {
          // Wait for 5 seconds
          yield return new WaitForSeconds(5f);

          // Load the first scene asynchronously using Addressables
          AsyncOperationHandle<SceneInstance> firstSceneHandle = Addressables.LoadSceneAsync(firstSceneAddress, LoadSceneMode.Additive);

          // Wait until the first scene is fully loaded
          while (!firstSceneHandle.IsDone)
          {
              yield return null;
          }

          // Unload the first scene
          SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

          // Load the second scene asynchronously using Addressables
          AsyncOperationHandle<SceneInstance> secondSceneHandle = Addressables.LoadSceneAsync(secondSceneAddress, LoadSceneMode.Additive);

          // Wait until the second scene is fully loaded
          while (!secondSceneHandle.IsDone)
          {
              yield return null;
          }

          Debug.Log("Scenes loaded successfully!");
      }*/
}
