using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectHit : MonoBehaviour
{
    //parameters
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] int lifes = 3;
    [SerializeField] Text lifesText;

    //cache
    private AudioSource playerSFX;

    //state
    bool isTransitioning = false;

    private void Start()
    {
        //component reference
        playerSFX = GetComponent<AudioSource>();
        lifesText.text = lifes.ToString();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag) // manage the collisions of different objects that the player can collide with
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Cheese":
                Destroy(other.gameObject);
                break;
            default:
                lifes--;
                lifesText.text = lifes.ToString();
                StartDeathSequence();
                break;
        }
    }

    void StartDeathSequence()
    {
        if (lifes >= 1)
        {
            playerSFX.Stop();
            playerSFX.PlayOneShot(deathSFX);
        }
        else
        {
            isTransitioning = false;
            playerSFX.Stop();
            GetComponent<Mover>().enabled = false;
            Invoke("GameOverProcess", levelLoadDelay);
            playerSFX.PlayOneShot(deathSFX);
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        playerSFX.Stop();
        Invoke("LoadNextLevel", levelLoadDelay);
        playerSFX.PlayOneShot(SuccessSFX);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void GameOverProcess()
    {
        SceneManager.LoadScene(5);
    }
}
