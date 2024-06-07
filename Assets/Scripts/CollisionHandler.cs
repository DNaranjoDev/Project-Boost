using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timer = 1f;
    [SerializeField] AudioClip collisionCrash;
    [SerializeField] AudioClip finishLanded;

    [SerializeField] ParticleSystem collisionParticle;
    [SerializeField] ParticleSystem finishParticle;
    AudioSource audioTrigger;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioTrigger = GetComponent<AudioSource>();

    }

    void Update()
    {
        DebugKeys();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                return;
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;

        audioTrigger.Stop();
        audioTrigger.PlayOneShot(collisionCrash);

        collisionParticle.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", timer);
    }

    void StartNextLevelSequence()
    {
        isTransitioning = true;

        audioTrigger.Stop();
        audioTrigger.PlayOneShot(finishLanded);

        finishParticle.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", timer);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Debug keys
    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    
}
