using UnityEngine;
public class UIIconsManager : MonoBehaviour
{
    [SerializeField] WeaponIconUI[] weaponsIcons;

    public static UIIconsManager instance;

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);

        //foreach (WeaponIconUI i in weaponsIcons)
        //{
        //    i.IconImageUI = weaponsIcons.IconImageUI;
        //}
    }

    public void EnableIcon(string name, bool isIconEnabled)
    {
        foreach (var wp in weaponsIcons)
        {
            if (wp.IconName == name)
            {
                wp.gameObject.SetActive(isIconEnabled);
                Debug.Log("Weapon icon enabled: " + wp.IconName + wp.enabled);
            }
        }
        //WeaponIconUI i = Array.Find(weaponsIcons, weaponIcon => weaponIcon.IconName == name);
        //if (i == null)
        //{
        //    return;
        //}
        //i.enabled = true;
    }

}
