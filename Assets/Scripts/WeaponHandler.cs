using UnityEngine;
using StarterAssets;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using System.Collections;
using System;

public class WeaponHandler : MonoBehaviour
{
    public event Action onShoot;

    [Header("References")]
    [SerializeField] private Cinemachine3rdPersonFollow cm_camera;
    private Animator anim;
    private ThirdPersonController controller;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 0.09f;
    [SerializeField] private float shootBlendTime = 0.075f;
    [SerializeField] private string shootStateName = "Fire_Rifle";
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject bullet;
    private bool canShoot = true;

    [Header("Aiming")]
    [SerializeField] private float cameraTransitionSpeed = 7f;
    [SerializeField] private float ikTransitionSpeed = 10f;
    [SerializeField] private MultiAimConstraint aimIk;
    [Space(10)]
    [SerializeField] private float aimVerticalArmLeght = 0.2f;
    [SerializeField] private float aimCameraSide = 0.75f;
    [SerializeField] private float aimCameraDistance = 0.85f;
    private float defaultVerticalArmLeght;
    private float defaultCameraSide;
    private float defaultCameraDistance;

    public bool aiming { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject crossHair;   

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();

        defaultVerticalArmLeght = cm_camera.VerticalArmLength;
        defaultCameraSide = cm_camera.CameraSide;
        defaultCameraDistance = cm_camera.CameraDistance;
    }

    private void Update()
    {
        //Input
        aiming = Input.GetButton("Fire2");
        bool shootInp = Input.GetButton("Fire1");

        //Animations

        anim.SetBool("Aiming", aiming);
        controller.strafe = aiming;

        //Adjust Camera
        float targetVerticalArmLength = aiming ? aimVerticalArmLeght : defaultVerticalArmLeght;
        float targetSide = aiming ? aimCameraSide : defaultCameraSide;
        float targetDistance = aiming ? aimCameraDistance : defaultCameraDistance;

        cm_camera.VerticalArmLength = Mathf.Lerp(cm_camera.VerticalArmLength, targetVerticalArmLength, cameraTransitionSpeed * Time.deltaTime);
        cm_camera.CameraSide = Mathf.Lerp(cm_camera.CameraSide, targetSide, cameraTransitionSpeed * Time.deltaTime);
        cm_camera.CameraDistance = Mathf.Lerp(cm_camera.CameraDistance, targetDistance, cameraTransitionSpeed * Time.deltaTime);

        //UI
        crossHair.SetActive(aiming);

        //IK
        float targetWeight = aiming ? 1 : 0;
        aimIk.weight = Mathf.Lerp(aimIk.weight, targetWeight, ikTransitionSpeed * Time.deltaTime);

        //Shoot
        if (shootInp && aiming)
            Shoot();
    }

    private void Shoot()
    {
        if (!canShoot)
            return;

        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        muzzleFlash.Play();
        anim.CrossFadeInFixedTime(shootStateName, shootBlendTime);
        StartCoroutine("ResetFireRate");
        onShoot?.Invoke();
    }

    private IEnumerator ResetFireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);//Crear la bala
        canShoot = true;
    }


}
