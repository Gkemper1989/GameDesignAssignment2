using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectHit : MonoBehaviour
{
    //parameters
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip SuccessSFX;

    //cache
    private AudioSource playerSFX;

    //state
    bool isTransitioning = false;

    private void Start()
    {
        //component reference
         playerSFX = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    { 
        

        if(isTransitioning) { return; }

        switch (other.gameObject.tag) // manage the collisions of different objects that the player can collide with
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    void StartDeathSequence()
    {
        isTransitioning = true;
        playerSFX.Stop();
        GetComponent<Mover>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        playerSFX.PlayOneShot(deathSFX);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        playerSFX.Stop();
        GetComponent<Mover>().enabled = false;
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

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //variable to store the index of the current scene(level)
        SceneManager.LoadScene(currentSceneIndex); // load the current scene(level)
    }
}
