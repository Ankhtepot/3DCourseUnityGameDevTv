using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static strings;

//Fireball Games * * * PetrZavodny.com

public class RocketShip : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float thrustStrength = 100f;
    [SerializeField] private bool indestructible = false;
    [Header("SFX")]
    [NotNull] [SerializeField] private AudioClip engineAudioClip;
    [NotNull] [SerializeField] private AudioClip deathAudioClip;
    [NotNull] [SerializeField] private AudioClip finishAudioClip;
    [Header("VFX")]
    [NotNull] [SerializeField] private ParticleSystem mainThrustVFX;
    [NotNull] [SerializeField] private ParticleSystem rightThrustVFX;
    [NotNull] [SerializeField] private ParticleSystem leftThrustVFX;
    [NotNull] [SerializeField] private ParticleSystem explosion;
    [Header("General")]
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private float sceneLoadDelay = 1f;
    [SerializeField] private float reloadOnDeathDelay = 0.5f;
    [NotNull] [SerializeField] private GameObject body;
    private Rigidbody RB;
    private AudioSource AS;
    private int totalSceneCount;
    private State state;
#pragma warning restore 649

    public UnityEvent ReachedFinish;

    public enum State
    {
        Alive,
        Transcending,
        Dying
    }

    void Start()
    {
        state = State.Alive;
        initialize();
    }

    void Update()
    {
        processInput();
    }

    private void processInput()
    {
        if (state == State.Alive)
        {
            thrust();
            rotate();
        }

        cheats();
    }

    private void cheats()
    {
        if (!Debug.isDebugBuild)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(resolveNextLevelIndex());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            indestructible = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case Tags.FRIENDLY: print("friendly collision"); break;
            case Tags.FINISH: loadNextLevel(); break;
            default: die(); break;
        }
    }

    private void loadNextLevel()
    {
        state = State.Transcending;
        playOneShot(finishAudioClip, 0.3f);
        StartCoroutine(loadNextScene());
    }

    private void die()
    {
        if (indestructible)
        {
            return;
        }

        state = State.Dying;
        var explosion = Instantiate(this.explosion, transform.position, Quaternion.identity);
        explosion.Play();
        stopOtherVFX();
        body.SetActive(false);
        playOneShot(deathAudioClip, 0.4f);
        StartCoroutine(loadSceneByIndex(currentLevel, reloadOnDeathDelay));
    }

    private void stopOtherVFX()
    {
        mainThrustVFX.Stop();
        rightThrustVFX.Stop();
        leftThrustVFX.Stop();
    }

    private void playOneShot(AudioClip clip, float volume = 0.2f)
    {
        AS.Stop();
        AS.volume = volume;
        AS.PlayOneShot(clip);
    }

    private IEnumerator loadNextScene()
    {
        ReachedFinish.Invoke();
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(resolveNextLevelIndex());
    }

    private IEnumerator loadSceneByIndex(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }

    private int resolveNextLevelIndex()
    {
        return currentLevel == totalSceneCount - 1 ? 0 : ++currentLevel;
    }

    private void thrust()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainThrustVFX.Play();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            RB.AddRelativeForce(Vector3.up * thrustStrength * Time.deltaTime);
            if (!AS.isPlaying)
            {
                AS.PlayOneShot(engineAudioClip);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            mainThrustVFX.Stop();
            AS.Stop();
        }
    }

    private void rotate()
    {
        if(Input.GetKeyDown(KeyCode.A) 
           || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rightThrustVFX.Play();
        }

        if (Input.GetKeyUp(KeyCode.A)
            || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rightThrustVFX.Stop();
        }

        if (Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.RightArrow))
        {
            leftThrustVFX.Play();
        }

        if (Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyUp(KeyCode.RightArrow))
        {
            leftThrustVFX.Stop();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            FreezeRotation(true);
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
            FreezeRotation(false);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            FreezeRotation(true);
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            FreezeRotation(false);
        }
    }

    private void FreezeRotation(bool state)
    {
        if (state)
        {
            RB.freezeRotation = true;
        }
        else
        {
            RB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void initialize()
    {
        this.RB = GetComponent<Rigidbody>();
        this.AS = GetComponent<AudioSource>();

        currentLevel = SceneManager.GetActiveScene().buildIndex;
        totalSceneCount = SceneManager.sceneCountInBuildSettings;
    }
}
