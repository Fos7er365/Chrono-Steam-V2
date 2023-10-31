using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player_Controller))]
public class PlayerActions : MonoBehaviour, IComand
{
    private Player_Controller _playerController;
    private int _comboCounter;
    private float _currentReleaseTime;
    [SerializeField] float _releaseTime;
    [Header("Ability UIs")]
    [SerializeField] private Image _gunUIarea; // la imagen q voy a mover
    [SerializeField] private Image _gunUIdistance;// la imagen q marca la distancia
    [SerializeField] private Transform _canvasCenter;

    public Image GunUIarea { get => _gunUIarea; }

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<Player_Controller>();
        _comboCounter = 0;
        _currentReleaseTime = _releaseTime;
    }
    public void Execute()
    {
        //Debug.Log($"t = {t}");

        #region normal click
        if (_playerController.Inputs.Action1())
        {
            if (_playerController.PlayerStats.Weapon != null)
            {
                _comboCounter += 1;
                _playerController.IsAttacking = true;
                Debug.Log(_comboCounter);
                if (_playerController.PlayerStats.Weapon.GetComponent<Weapon>().CurrentCD <= 0)
                {
                    if (_comboCounter != 0)
                    {
                        if (_comboCounter == 1)
                        {
                            if (_playerController.PlayerStats.Weapon.CompareTag("Blade"))
                            {

                                _playerController.Animations.AttackAnimation();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Claimore"))
                            {
                                //_player.Animations.AttackAnimation();
                                _playerController.Animations.ClaymoreAttackAnimation();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[2].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Gun"))
                            {
                                _playerController.Animations.GunAttackAnimation();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Fist"))
                            {
                                _playerController.Animations.FistAttackAnimation();
                                //_player.PlayerStats.Weapon.GetComponent<Weapon>().PlayParticleSystems();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Spear"))
                            {
                                _playerController.Animations.SpearAttackAnimation();
                                //_player.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().PlayParticleSystems();
                            }
                        }
                        if (_comboCounter == 2)
                        {
                            if (_playerController.PlayerStats.Weapon.CompareTag("Blade"))
                            {
                                _playerController.Animations.AttackAnimation2();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Claimore"))
                            {
                                //_player.Animations.AttackAnimation2();
                                _playerController.Animations.ClaymoreAttackAnimation2();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[2].Play();

                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Gun"))
                            {
                                _playerController.Animations.GunAttackAnimation();
                                // _player.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Fist"))
                            {
                                _playerController.Animations.FistAttackAnimation2();
                                //_player.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Spear"))
                            {
                                _playerController.Animations.SpearAttackAnimation2();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                        }
                        if (_comboCounter == 3)
                        {
                            if (_playerController.PlayerStats.Weapon.CompareTag("Blade"))
                            {
                                _playerController.Animations.AttackAnimation3();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Claimore"))
                            {
                                //_player.Animations.AttackAnimation3();
                                _playerController.Animations.ClaymoreAttackAnimation3();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[2].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Gun"))
                            {
                                _playerController.Animations.GunAttackAnimation();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Fist"))
                            {
                                _playerController.Animations.FistAttackAnimation3();
                                //_player.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                            if (_playerController.PlayerStats.Weapon.CompareTag("Spear"))
                            {
                                _playerController.Animations.SpearAttackAnimation3();
                                _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Play();
                            }
                        }
                        if (_comboCounter >= 3)
                        {
                            _comboCounter = 0;
                        }
                    }
                    else
                        _playerController.PlayerStats.Weapon.GetComponent<Weapon>().ParticleSystems[0].Stop();
                    // Debug.Log("_playerStats.Weapon.Atack()");
                }
            }
        }
        #endregion
        #region "R" Click press
        if (_playerController.Inputs.Action2())
        {
            if (_playerController.PlayerStats.Weapon != null)
            {
                if (_playerController.PlayerStats.Weapon.CompareTag("Blade"))
                {
                    //_player.WeaponSpecialExecute();
                }
                if (_playerController.PlayerStats.Weapon.CompareTag("Claimore"))
                {
                    //_player.WeaponSpecialExecute();
                }
                if (_playerController.PlayerStats.Weapon.CompareTag("Gun"))
                {
                    if (_gunUIarea != null && _gunUIdistance != null)
                    {
                        if (_playerController.PlayerStats.Weapon.GetComponent<Weapon>().CurrentEspExeCd >= _playerController.PlayerStats.Weapon.GetComponent<Weapon>().WeaponStats.EspExeCd)
                        {
                            _gunUIarea.gameObject.SetActive(true); // activo las UI
                            _gunUIdistance.gameObject.SetActive(true);
                        }
                    }
                    else
                        Debug.Log("GunUI NOT Asigned");
                }
                if (_playerController.PlayerStats.Weapon.CompareTag("Fist"))
                {

                }
                if (_playerController.PlayerStats.Weapon.CompareTag("Spear"))
                {

                }
                // _player.WeaponSpecialExecute();
            }
        }
        #endregion
        #region "R" Click release
        if (_playerController.Inputs.Action02())
        {
            if (_playerController.PlayerStats.Weapon != null)
            {
                if (_playerController.PlayerStats.Weapon.GetComponent<Weapon>().CurrentEspExeCd >= _playerController.PlayerStats.Weapon.GetComponent<Weapon>().WeaponStats.EspExeCd)
                {
                    _playerController.PrevRotation = _playerController.Rb.rotation;
                    _playerController.IsSpecial = true;
                    if (_playerController.PlayerStats.Weapon.CompareTag("Blade"))
                    {
                        _playerController.Animations.SpecialAttackAnimation();
                    }
                    if (_playerController.PlayerStats.Weapon.CompareTag("Claimore"))
                    {
                        _playerController.Animations.SpecialClaymoreAttackAnimation();
                    }
                    if (_playerController.PlayerStats.Weapon.CompareTag("Gun"))
                    {
                        if (_gunUIarea.gameObject.activeSelf && _gunUIdistance.gameObject.activeSelf)
                        {
                            _playerController.WeaponSpecialExecute();
                            _gunUIarea.transform.position = _canvasCenter.position;
                            _gunUIarea.gameObject.SetActive(false);
                            _gunUIdistance.gameObject.SetActive(false);
                        }
                    }
                    if (_playerController.PlayerStats.Weapon.CompareTag("Fist"))
                    {
                        _playerController.Animations.SpecialFistAttackAnimation();
                        FindObjectOfType<AudioManager>().Play("Gun_Weapon_Ultimate_A");
                        FindObjectOfType<AudioManager>().Play("Gun_Weapon_Ultimate_B");
                    }
                    if (_playerController.PlayerStats.Weapon.CompareTag("Spear"))
                    {
                        _playerController.Animations.SpecialSpearAttackAnimation();
                    }

                    /* Unmerged change from project 'Assembly-CSharp.Player'
                    Before:
                                    }

                                    // _player.WeaponSpecialExecute();
                    After:
                                    }

                                    // _player.WeaponSpecialExecute();
                    */
                }

                // _player.WeaponSpecialExecute();
            }
        }
        #endregion
        #region "E" button
        if (_playerController.Inputs.Action3())
        {
            if (_currentReleaseTime <= 0)
            {
                if (_playerController.PlayerStats.Weapon != null)
                {
                    _playerController.IsWeaponSlotNull = true;
                    FindObjectOfType<AudioManager>().Play("BrokenWeapon");
                    _playerController.PlayerStats.Weapon.GetComponent<Weapon>().TurnOffWeaponFresnel();
                    //Debug.Log("Se desprendió la weapon");
                    if (!_playerController.PlayerStats.Weapon.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                    {
                        Rigidbody rb = _playerController.PlayerStats.Weapon.AddComponent<Rigidbody>();
                        _playerController.PlayerStats.Weapon.GetComponent<Weapon>().Rb = rb;
                    }
                    GameObject WeaponRef = _playerController.PlayerStats.Weapon;
                    _playerController.PlayerStats.Weapon.GetComponent<Weapon>().Rb?.AddExplosionForce(500f, transform.position, 10f);
                    _playerController.PlayerStats.Weapon.GetComponent<Weapon>().Rb?.AddTorque(_playerController.PlayerStats.Weapon.transform.right * 1000f);
                    _playerController.EnableWeaponIcon(_playerController.PlayerStats.Weapon.gameObject.GetComponent<Weapon>().WeaponStats.WeaponName, false);
                    _playerController.PlayerStats.Weapon.transform.parent = null;
                    _playerController.PlayerStats.Weapon = null;
                    _currentReleaseTime = _releaseTime;
                    Destroy(WeaponRef, 2f);
                }
            }
            else
            {
                _currentReleaseTime -= Time.deltaTime;
            }
        }
        if (_playerController.Inputs.ReleaseAction3())
        {
            _currentReleaseTime = _releaseTime;
        }
        #endregion
        if (_playerController.Inputs.Action4() && !_playerController.IsDashing && _playerController.CurrentDashCoolDown <= 0)
        {
            _playerController.Dash();
        }
        if (_playerController.Inputs.Action5() && _playerController.CurrentPunchCD <= 0)
        {
            _playerController.ThrustingPunch();
        }
    }
    public void ResetCombo()
    {
        _comboCounter = 0;
    }
    //se llama desde el animator punch animation event
    public void PunchDetection()
    {
        Collider[] enemies = Physics.OverlapSphere(_playerController.transform.position, _playerController.PlayerStats.AbilitiStats.PunchArea);
        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.gameObject.CompareTag("Enemy"))
                enemy.gameObject.GetComponent<Enemy>().EnemyHealthController.GetDamage(_playerController.PlayerStats.AbilitiStats.PunchDamage);
        }
    }

    #region WeaponAbilities
    void ThrowSpear()
    {

        //Debug.Log($"t = {t}");

        //if (t >= spearThrowMaxTimer)
        //{

        _playerController.PlayerStats.Weapon.GetComponent<Transform>().position = _playerController.PlayerStats.Weapon.GetComponent<Transform>().position
                                                                        + new Vector3(_playerController.PlayerStats.Weapon.GetComponent<SpearWeapon>().AreaStats.MaxDistance,
                                                                        0, _playerController.PlayerStats.Weapon.GetComponent<SpearWeapon>().AreaStats.MaxDistance);

        Debug.Log($"Spear new pos at {_playerController.PlayerStats.Weapon.GetComponent<Transform>().position}");

        //t = 0;

        //}
    }

    void RetrieveSpear()
    {
        //_player.PlayerStats.Weapon.GetComponent<Transform>().position = _player.PlayerStats.Weapon.GetComponent<Transform>().position
        //                                                                - new Vector3(_player.PlayerStats.Weapon.GetComponent<SpearWeapon>().AreaStats.MaxDistance,
        //                                                                0, _player.PlayerStats.Weapon.GetComponent<SpearWeapon>().AreaStats.MaxDistance);


        _playerController.PlayerStats.Weapon.GetComponent<Transform>().position = _playerController.GetComponent<Transform>().position;

    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position,_player.PlayerStats.AbilitiStats.PunchArea);
    }
}
