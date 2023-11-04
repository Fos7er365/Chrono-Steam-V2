using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public GameObject target;
    private Camera cam;
    Player_Controller player;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameManager.Instance.PlayerInstance.GetComponent<Player_Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.Life_Controller.isDead) return;
        LookMouse();
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 destination = new Vector3(mousePos.x, mousePos.y, 0);
        //target.transform.position = destination;
        
    }

    void LookMouse()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
