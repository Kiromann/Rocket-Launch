using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 50f;
    [SerializeField] float rotationStrenght = 100f;
    [SerializeField] AudioClip engineSound;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        Thrust();
        Rotation();
    }

    private void Thrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineSound);
            }
        } else
        {
            audioSource.Stop();
        }
    }

    private void Rotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            ApplyRotation(rotationStrenght);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrenght);
        }
    }

    private void ApplyRotation(float rotationFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
