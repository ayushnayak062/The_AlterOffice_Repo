using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    // Singleton pattern to ensure only one instance of CoroutineRunner
    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                // Create a new GameObject to attach the CoroutineRunner to
                GameObject go = new GameObject("CoroutineRunner");
                _instance = go.AddComponent<CoroutineRunner>();
            }
            return _instance;
        }
    }

    // This method is used to start coroutines from non-MonoBehaviour classes
    public void StartCoroutineFromNonMono(IEnumerator coroutine)
    {
        // Start the coroutine as usual on this MonoBehaviour
        StartCoroutine(coroutine);
    }
}
