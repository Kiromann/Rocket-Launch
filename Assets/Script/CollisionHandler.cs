using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentScene;

    [SerializeField] float timeBreak = 2f;
    [SerializeField] AudioClip succesSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem succesParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    bool isControllable = true;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start from here bubu");
                break;
            case "Finish":
                StartNextSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartNextSequence()
    {
        StopTheGameShortly();
        succesParticle.Play();
        audioSource.PlayOneShot(succesSound);
        Invoke("NextScene", timeBreak);
    }

    void StartCrashSequence()
    {
        StopTheGameShortly();
        crashParticle.Play();
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadScene", timeBreak);
    }

    void NextScene()
    {
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    void StopTheGameShortly()
    {
        GetComponent<Movement>().enabled = false;
        isControllable = false;
        audioSource.Stop();
    }

}
