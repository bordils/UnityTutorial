using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsTrhust = 100.0f;
    [SerializeField] float mainTrhust = 100.0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem mainEnginePS;
    [SerializeField] ParticleSystem successPS;
    [SerializeField] ParticleSystem deathPS;
    [SerializeField] float levelLoadDelay = 2f;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dead, Transcending};
    State state = State.Alive;
    bool collisionsDisabled = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            ThrustInput();
            Rotate();
        }
        if(Debug.isDebugBuild)
        {
            DebugResponse();
        }
    }

    private void DebugResponse()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        } else if (Input.GetKey(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private void ThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEnginePS.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainTrhust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEnginePS.Play();
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsTrhust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.right * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.right * rotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive || collisionsDisabled) {  return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                ProcessSuccess();
                break;
            default:
                ProcessDeath();
                break;
        } 
    }

    private void ProcessSuccess()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successPS.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void ProcessDeath()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathPS.Play();
        Invoke("RestartLevel", levelLoadDelay);
        print("you died");
    }

    private void RestartLevel()
    {
        state = State.Alive;
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        state = State.Alive;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        // todo next 2 levels
    }
}
