﻿using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour, IComand
{
    [SerializeField]
    protected WeaponStats _weaponStats;
    [SerializeField]
    protected List<SkinnedMeshRenderer> weaponMaterials = new List<SkinnedMeshRenderer>();
    protected float _currentEspExeCd = 0;
    protected float _currentCD;
    protected internal int currentDurability;
    public bool drawGizmos;
    bool isDrop;
    string weaponTag;
    Rigidbody _rb;
    Animator _wpAnimator;
    protected HitCounter hitCounter;

    [SerializeField]
    protected List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    [SerializeField]
    protected List<ParticleSystem> espParticleSystems = new List<ParticleSystem>();

    //[SerializeField]
    //protected List<SkinnedMeshRenderer> weaponMaterials = new List<SkinnedMeshRenderer>();
    //[SerializeField]
    //protected SkinnedMeshRenderer weaponFresnelMaterial;

    public WeaponStats WeaponStats => _weaponStats;
    public float CurrentCD { get => _currentCD; }

    public List<ParticleSystem> ParticleSystems { get => particleSystems; }
    public Animator WpAnimator { get => _wpAnimator; }
    public float CurrentEspExeCd { get => _currentEspExeCd; }
    public string WeaponTag { get => weaponTag; }
    public bool IsDrop { get => isDrop; set => isDrop = true; }
    public Rigidbody Rb { get => _rb; set => _rb = value; }

    public virtual void Execute()
    {
        Debug.Log($"Hice {_weaponStats.AttDamage} de daño con {name} a rango melee de distancia");
    }

    public virtual void WeaponSpecialAttack()
    {
    }
    public virtual void CoolDown()
    {
        if (_currentCD > 0)
        {
            _currentCD -= Time.deltaTime;
        }
    }
    // Start is called before the first frame update

    public virtual void Start()
    {
        _currentCD = 0;
        _currentEspExeCd = WeaponStats.EspExeCd;
        currentDurability = _weaponStats.Durability;
        /* _rb = this.gameObject.GetComponent<Rigidbody>();
         if (_rb = null)
              gameObject.AddComponent<Rigidbody>();*/
        Physics.IgnoreLayerCollision(11, 10);
    }

    public void PlayParticleSystems()
    {
        foreach (var ps in particleSystems)
        {
            ps.Play();
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (currentDurability <= 0)
        {
            currentDurability = 0;
            if (this.gameObject != null && !isDrop)
            {
                FindObjectOfType<AudioManager>().Play("BrokenWeapon");
                //Debug.Log("Se desprendió la weapon");
                TurnOffWeaponFresnel();
                if (!gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
                    _rb = rb;
                }
                _rb?.AddExplosionForce(500f, transform.position, 6f);
                _rb?.AddTorque(transform.right * 1000f);

                transform.parent = null;
                var player = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>();
                player.PlayerStats.Weapon = null;

                player.EnableWeaponIcon(WeaponStats.WeaponName, false);
                //GameObject weaponRef = GameManager.Instance.PlayerInstance.GetComponent<Player_Controler>().PlayerStats.Weapon;
                isDrop = true;
                Destroy(this.gameObject, 2f);
            }
        }
        else
            isDrop = false;
        if (_currentEspExeCd < WeaponStats.EspExeCd)
        {
            _currentEspExeCd += Time.deltaTime;
        }
    }

    public void DestroyWeapon(SkinnedMeshRenderer m)
    {
        //foreach (var itemA in weaponMaterials)
        //{
        //    for (int i = 0; i < itemA.GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
        //    {
        //        var itemB = itemA.GetComponent<SkinnedMeshRenderer>().materials[i];
        //        // si el material es un enemy fresnel...
        //        if (itemB.name != m.name)
        //        {
        //            //Debug.Log("material correcto");
        //            //...seteo fesnel scale para q ya no brile
        //            itemA.GetComponent<SkinnedMeshRenderer>().materials[i].SetFloat("_FresnelScale", 0);
        //        }
        //    }
        //}
    }
    public void TurnOffWeaponFresnel()
    {
        if (TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
            {
                var b = GetComponent<MeshRenderer>().materials[i];
                //if (b.name == _weaponTag)
                if (b.shader.name == "Fresnel")
                    b.SetFloat("_FresnelScale", 0);
                else
                {
                    b.SetFloat("_FresnelRimPower", 0);
                    b.SetFloat("_FresnelSize", 0);
                }
            }
        }
    }
}
