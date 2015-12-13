﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking    ;

public class PlayerShooting : NetworkBehaviour {
    public float AmmoMax;
    public float ammoCount;
    public Text BulletText;
    public Text StatusUpdate;
    public float fireDelay = 0.25f;
    float cooldownTimer = 0;
    public GameObject bulletPrefab;
    public AudioClip[] ShootingSounds;
    public Vector3 bulletOffset = new Vector3(0f, 0.5f, 0f);
    public static bool revolverEnable;
    public static bool ShottyEnable;
    public static bool rifleEnable;
    [SerializeField] GameObject bullet1;
    [SerializeField]
    GameObject bullet2;
    [SerializeField]
    GameObject bullet3;
    [SerializeField]
    GameObject bullet4;
    [SerializeField]
    GameObject bullet5;
    // Update is called once per frame
    void Start()
    {
        AmmoMax = 10;
        ammoCount = AmmoMax;
        rifleEnable = false;
        revolverEnable = true;
        ShottyEnable = false;
    }
    void Update () {
	cooldownTimer -= Time.deltaTime;
        Shooting();
        
	}
    public void Shooting()
    {
        if (Input.GetMouseButton(0) && cooldownTimer <= 0 && ScoreHandler.ammo > 0 && rifleEnable)
        {
            cooldownTimer = fireDelay - 0.1f;
            Vector3 offset = transform.rotation * bulletOffset;
            bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
            PlaySound(0);
            ScoreHandler.ammo--;
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            rifleEnable = false;
            revolverEnable = true;
            ShottyEnable = false;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            rifleEnable = true;
            revolverEnable = false;
            ShottyEnable = false;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            rifleEnable = false;
            revolverEnable = false;
            ShottyEnable = true;
        }
        if (Input.GetMouseButton(0) && cooldownTimer <= 0 && ScoreHandler.ammo > 0 && revolverEnable)
        {
            cooldownTimer = fireDelay + 0.45f;
            Vector3 offset = transform.rotation * bulletOffset;
            bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(bullet1);
            PlaySound(0);
            ScoreHandler.ammo--;
        }
        if (Input.GetMouseButton(0) && cooldownTimer <= 0 && ScoreHandler.ammo > 0 && ShottyEnable)
        {
            cooldownTimer = fireDelay + 0.45f;
            Vector3 offset = transform.rotation * bulletOffset;
            GameObject bullet1 = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(bullet1);
            float zRot = transform.rotation.eulerAngles.z;
            float leftAngleMax = zRot - 25f;
            float rightAngleMax = zRot + 25f;
            float leftAngleInner = zRot - 10f;
            float rightAngleInner = zRot + 10f;
            Quaternion leftAngleQuat = Quaternion.Euler(transform.rotation.x, transform.rotation.y, leftAngleMax);
            Quaternion rightAngleQuat = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rightAngleMax);
            Quaternion leftAngleQuat2 = Quaternion.Euler(transform.rotation.x, transform.rotation.y, leftAngleInner);
            Quaternion rightAngleQuat2 = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rightAngleInner);
            bullet2 = (GameObject)Instantiate(bulletPrefab, transform.position, leftAngleQuat);
            NetworkServer.Spawn(bullet2);
            bullet3 = (GameObject)Instantiate(bulletPrefab, transform.position, rightAngleQuat);
            NetworkServer.Spawn(bullet3);
            bullet4 = (GameObject)Instantiate(bulletPrefab, transform.position, leftAngleQuat2);
            NetworkServer.Spawn(bullet4);
            bullet5 = (GameObject)Instantiate(bulletPrefab, transform.position, rightAngleQuat2);
            NetworkServer.Spawn(bullet5);
            PlaySound(0);
            ScoreHandler.ammo--;
        }
        if (Input.GetButton("Fire1") && ammoCount <= 0)
            PlaySound(1);

    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = ShootingSounds[clip];
        GetComponent<AudioSource>().Play();
    }
}
