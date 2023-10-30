using UnityEngine;

public class BossATCKControler : MonoBehaviour
{
    public GameObject teslaBall;
    [SerializeField]
    private GameObject smashObject;
    [SerializeField]
    private GameObject clapObject;

    public void SmashAttack()
    {
        Debug.Log("Smash attack");
        Instantiate(smashObject, transform.position, Quaternion.Euler(0, 0, 0));
    }
    public void ClapAttack()
    {
        Debug.Log("Clap attack");
        Instantiate(clapObject, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
    }
    public void TeslaAttack()
    {
        Debug.Log("Tesla attack");
        Instantiate(teslaBall, gameObject.transform.position, gameObject.transform.rotation);
    }
}