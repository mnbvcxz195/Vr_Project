
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [Header("Grab interactable")]
    [SerializeField] XRGrabInteractable grabInteractable;

    [Header("Raycasting info")]
    [SerializeField] Transform raycastOrigin;
    [SerializeField] LayerMask targetLayer;

    [Header("Audio SFX")]
    [SerializeField] AudioClip gunClipSFX;
    AudioSource gunAudioSource;

    [Header("Target hit graphic")]
    [SerializeField] GameObject hitGraphic;

    [Header("Gun Shot graphic")]
    [SerializeField] GameObject gunShotGraphic;
    [SerializeField] Transform gunShotPos;

    [Header("Spawn Points")]
    [SerializeField] private Transform casingSpawnPoint;        //탄피 생성 위치

    [Header("Colider Controll")]
    [SerializeField] GameObject handleColider;
    [SerializeField] GameObject gunBodyColider;

    private CasingMemoryPool casingMemoryPool; //탄피 생성 후 활성/비활성 관리

    private void Awake()
    {
        casingMemoryPool = GetComponent<CasingMemoryPool>();

        if (TryGetComponent(out AudioSource audio))
        {
            gunAudioSource = audio;
        }
        else
        {
            gunAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }
    }

    private void OnEnable() => grabInteractable.activated.AddListener(TriggerPulled);

    private void OnDisable() => grabInteractable.activated.RemoveListener(TriggerPulled);

    private void TriggerPulled(ActivateEventArgs arg0)
    {
        arg0.interactor.GetComponent<XRBaseController>().SendHapticImpulse(0.5f, 0.25f);

        gunAudioSource.PlayOneShot(gunClipSFX);

        CreateGunShotGraphic(gunShotPos.position);

        FireRaycastIntoScene();

        //탄피 생성
        casingMemoryPool.SpawnCasing(casingSpawnPoint, transform.right);
    }

    private void FireRaycastIntoScene()
    {
        RaycastHit hit;

        if(Physics.Raycast(raycastOrigin.position, raycastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, targetLayer))
        {
            if(hit.transform.GetComponent<ITargetInterface>() != null)
            {
                hit.transform.GetComponent<ITargetInterface>().TargetShot();
                MonsterManager.GetInstance().Newmonster.MonsterHp -= 10;

                CreateHitGraphicOnTarget(hit.point);
            }
            else
            {
                Debug.Log("Not interheriting from interface");
            }
        }
    }

    private void CreateHitGraphicOnTarget(Vector3 hitLocation)
    {
        GameObject hitMarker = Instantiate(hitGraphic, hitLocation, Quaternion.identity);
    }
    private void CreateGunShotGraphic(Vector3 shotLocation)
    {
        GameObject shotMarker = Instantiate(gunShotGraphic, shotLocation, Quaternion.identity);
        shotMarker.transform.rotation = this.gameObject.transform.rotation;
    }

    public void IsTriggerOn()
    {
        handleColider.GetComponentInChildren<BoxCollider>().isTrigger = true;
        gunBodyColider.GetComponentInChildren<BoxCollider>().isTrigger = true;
    }

    public void IsTriggerOff()
    {
        handleColider.GetComponentInChildren<BoxCollider>().isTrigger = false;
        gunBodyColider.GetComponentInChildren<BoxCollider>().isTrigger = false;
    }
}
