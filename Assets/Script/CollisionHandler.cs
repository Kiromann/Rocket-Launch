using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start from here bubu");
                break;
            case "Finish":
                Debug.Log("Finish");
                break;
            case "Fuel":
                Debug.Log("You got ekstra fuel bubu");
                break;
            default:
                ReloadScene();
                break;
        }

        void ReloadScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
