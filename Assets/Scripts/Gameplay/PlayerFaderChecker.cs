using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaderChecker : MonoBehaviour
{
    [SerializeField] LayerMask availableToFadeLayermask;
    [SerializeField] Vector3 fadingDistance;
    GameObject playerReference;
    ObjectFader objFader;

    // Start is called before the first frame update
    void Start()
    {
        playerReference = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerReference != null)
        {
            var playerRef = playerReference.transform.position;
            //RaycastHit[] hits = Physics.RaycastAll(playerReference.transform.position, dir, maxFadingDstance, availableToFadeLayermask);
            //Collider[] coll = Physics.OverlapBox(new Vector3(playerRef.x * (maxFadingDistance / 4), playerRef.y + 1, playerRef.z - (maxFadingDistance / 2)),
            //    new Vector3(maxFadingDistance, 0, maxFadingDistance));
            Collider[] coll = Physics.OverlapBox(new Vector3(playerRef.x, playerRef.y, playerRef.z - (fadingDistance.z / 2)),
                new Vector3(fadingDistance.x, playerRef.y + fadingDistance.y, fadingDistance.z));
            //Collider[] coll = Physics.OverlapSphere(playerReference.transform.position, fadingDistance.z);
            if (coll.Length > 0)
            {
                Debug.Log("Hay objetos para transparentar");
                foreach(var h in coll)
                {
                    if(h.gameObject.tag == "Fadeable_Wall")
                    {
                        h.gameObject.GetComponent<ObjectFader>().IsFading = true;
                        CheckFadingReset(h.gameObject);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (playerReference != null)
        {
            Vector3 playerRef = playerReference.transform.position;
            Gizmos.color = Color.magenta;
            //Gizmos.DrawWireSphere(playerRef, fadingDistance.z);
            Gizmos.DrawWireCube(new Vector3(playerRef.x, playerRef.y, playerRef.z-(fadingDistance.z/2)),
                new Vector3(fadingDistance.x, playerRef.y + fadingDistance.y, fadingDistance.z));
        }
    }

    void CheckFadingReset(GameObject _go)
    {
        if (Vector3.Distance(playerReference.transform.position, _go.transform.position) > fadingDistance.z) _go.GetComponent<ObjectFader>().IsFading = false;
    }
}
