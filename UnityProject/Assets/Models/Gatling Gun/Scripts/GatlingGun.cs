using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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

    // Ammo system for the turret
    public Text ammoCounter;
    public int currentAmmo = clipSize;
    public bool ammoChanged = false;
    public static int reloadTimer = 1800;
    public static int clipSize = 40;
    private Task IsReloading;

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

    public void Reload()
    {
        if (IsReloading != null && IsReloading.Status != TaskStatus.RanToCompletion)
            return;

        IsReloading = Task.Delay(reloadTimer);

        source = new CancellationTokenSource();
        CancellationToken token = source.Token;

        IsReloading.ContinueWith(t =>
        {
            currentAmmo = clipSize;
            ammoChanged = true;
        }, token);
    }

    public bool CanFire()
    {
        return currentAmmo > 0;
    }

    void Update()
    {
        PlayFireAnimation();

        if(ammoChanged)
        {
            ammoCounter.text = currentAmmo.ToString();
            ammoChanged = false;
        }
    }

    void Start()
    {
        ammoCounter = GameObject.Find("AmmoCounter").gameObject.GetComponent<Text>();
        currentAmmo = clipSize;
        ammoChanged = true;
    }

    private void ReduceAmmoCount()
    {
        currentAmmo = (currentAmmo > 0) ? --currentAmmo : 0;
        ammoChanged = true;
    }

    private void PlayFireAnimation()
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
            else  // Reduces ammo counter
                ReduceAmmoCount();
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