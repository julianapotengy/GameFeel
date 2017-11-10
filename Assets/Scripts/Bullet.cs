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

    private void Start()
    {
        bulletRigidbody.AddForce(impulse, ForceMode2D.Impulse);
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();

        audioSrc = Object.FindObjectOfType<AudioSource>() as AudioSource;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sizeScale = GetComponent<Transform>();
        audioSrc.volume = sizeScale.localScale.x;

        audioSrc.PlayOneShot(explosionAudio);
        cannon.shakeDuration = 0.2f;
        cannon.shakeAmount = 0.08f;
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(collisionParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
