using UnityEngine;

public sealed class Bullet : MonoBehaviour
{
    public Rigidbody2D bulletRigidbody;
    public Vector2 impulse;
    public GameObject collisionParticle;
    public Cannon cannon;
    public GameObject explosion;

    public AudioSource audioSrc;
    public AudioClip explosionAudio;
    public Transform sizeScale;

    public float rotateTo;

    private void Start()
    {
        bulletRigidbody.AddForce(impulse, ForceMode2D.Impulse);
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();

        if(cannon.transform.eulerAngles.z < 270)
        {
            rotateTo = cannon.transform.eulerAngles.z + 30;
            transform.Rotate(0, 0, rotateTo);
        }
        else if(cannon.transform.eulerAngles.z > 270)
        {
            rotateTo = cannon.transform.eulerAngles.z + 90;
            transform.Rotate(0, 0, rotateTo);
        }

        audioSrc = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
    }

    private void Update()
    {
        Debug.Log(cannon.transform.eulerAngles.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sizeScale = GetComponent<Transform>();

        if (sizeScale.localScale.x == 1)
        {
            audioSrc.pitch = 0.75f;
        }
        else audioSrc.pitch = 1;

        audioSrc.PlayOneShot(explosionAudio);
        cannon.shakeDuration = 0.2f;
        cannon.shakeAmount = 0.08f;
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(collisionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
