using UnityEngine;

public sealed class Cannon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float instantiateOffset = 10.0f;
    public float shootImpulse = 30.0f;

    public Vector3 startingDirection = Vector3.left;

    public Camera mainCamera;
    private Vector3 camPosition;
    public float shakeDuration = 0;
    public float shakeAmount;
    public float decreaseFactor;

    public AudioSource audioSrc;
    public AudioClip shootAudio;

    private void Start()
    {
        mainCamera = Camera.main;
        camPosition = mainCamera.transform.position;
        audioSrc = Object.FindObjectOfType<AudioSource>() as AudioSource;
    }

    private void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 mouseDirection = Input.mousePosition - screenPosition;
        mouseDirection.z = 0.0f;

        var rotation = Quaternion.FromToRotation(startingDirection, mouseDirection);
        transform.localRotation = rotation;

        if( Input.GetMouseButtonDown(0))
        {
            audioSrc.PlayOneShot(shootAudio);

            Vector2 direction = ((Vector2)mouseDirection).normalized;
            Vector3 position = transform.position + (Vector3) direction * instantiateOffset;

            shakeDuration = 0.2f;
            shakeAmount = 0.02f;
            var bullet = Instantiate(bulletPrefab.gameObject, position, Quaternion.identity).GetComponent<Bullet>();
            bullet.impulse = direction * shootImpulse;
        }

        if (shakeDuration > 0)
        {
            mainCamera.transform.position = camPosition + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0;
            mainCamera.transform.position = camPosition;
        }
    }
}
