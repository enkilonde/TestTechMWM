using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarBehaviour : MonoBehaviour
{

    private int desiredPosition = -1;
    public float speed = 5;
    public float distance;

    [Header("References")]
    public ParticleSystem explosion;
    public Transform visual;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.right * desiredPosition * distance, speed * Time.deltaTime);

        if (desiredPosition == 0 && Mathf.Abs(transform.localPosition.x) < 0.05f)
            Explode();
    }

    public void SwitchSide()
    {
        desiredPosition *= -1;
    }

    public void Crash()
    {
        desiredPosition = 0;
    }

    public void Explode()
    {
        explosion.Play();
        visual.gameObject.SetActive(false);
        this.enabled = false;
    }

}
