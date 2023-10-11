using UnityEngine;

public class MinimapUIHandler : MonoBehaviour
{
    GameObject playerGO;
    [SerializeField] Vector3 boundings;
    float checkBoundingTimer, maxCheckBoundingTimer = 1f;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GameObject.FindWithTag("Player") != null) playerGO = GameObject.FindWithTag("Player");

    }

    private void Update()
    {
        transform.position = new Vector3(playerGO.transform.position.x, transform.position.y, playerGO.transform.position.z);
        checkBoundingTimer += Time.deltaTime;
        //if (checkBoundingTimer >= maxCheckBoundingTimer)
        //{
        //    var lastPos = transform.position;
        //    Debug.Log("Checking walls collision");
        //    Collider[] coll = Physics.OverlapBox(transform.position, boundings, Quaternion.identity, LayerMask.GetMask("Wall"));
        //    if (coll.Length > 0)
        //    {
        //        Debug.Log("Collision with wall sadasd: " + coll[coll.Length-1]);
        //    }
        //    checkBoundingTimer = 0f;

        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, boundings);
    }
}
