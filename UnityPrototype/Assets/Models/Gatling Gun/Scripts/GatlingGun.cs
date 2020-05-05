using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    // target the gun will aim at
    Transform go_target;

    // Gameobjects need to control rotation and aiming
    //public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    // Gun barrel rotation
    public float barrelRotationSpeed;
    float currentRotationSpeed;

    // Distance the turret can aim and fire from
    public float firingRange;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    // Used to start and stop the turret firing
    public bool fireAnimation = false;
    private Task IsFiring;
    private CancellationTokenSource source;

    public void Fire()
    {
        CancellationToken token;
        fireAnimation = true;

        if(IsFiring == null || IsFiring.Status == TaskStatus.RanToCompletion)
        {
            IsFiring = Task.Delay(200);

            source = new CancellationTokenSource();
            token = source.Token;

            IsFiring.ContinueWith(t =>
            {
                fireAnimation = false;
            }, token);
        }
        else if(IsFiring.Status == TaskStatus.WaitingForActivation || IsFiring.Status == TaskStatus.WaitingToRun)
        {
            source.Cancel();

            IsFiring.ContinueWith(t =>
            {
                fireAnimation = false;
            }, token);
        }
    }

    void Update()
    {
        AimAndFire();
    }


    void AimAndFire()
    {
        // Gun barrel rotation
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (fireAnimation)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;

            // start particle system 
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }
        }
        else
        {
            // slow down barrel rotation and stop
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 2 * Time.deltaTime);

            // stop the particle system
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }
}