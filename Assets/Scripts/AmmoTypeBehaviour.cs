using UnityEngine;

public class AmmoTypeBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _plasma;
    [SerializeField]
    private float _newSpawnDuration = .1f;
    [SerializeField]
    private float _forceMultiplier = 2f;

    [SerializeField]
    private Vector3 SpawnPos;
    [SerializeField]
    private Vector3 SpawnScreenPos;

    [SerializeField]
    private GameObject CurrentSpawn;
    [SerializeField]
    private bool SpawnReady = false;
    [SerializeField]
    private GameObject SpawnParent;

    void Start()
    {
        SpawnPos = transform.position;
        SpawnScreenPos = Camera.main.WorldToScreenPoint(SpawnPos);
        SpawnParent = new GameObject( name: "SpawnParent");
        SpawnNewObject();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Autoshoot();
        }
    }

    void Autoshoot()
    {
        if (SpawnReady)
        {
            Shoot();
        }
    }


    void SpawnNewObject()
    {
        CurrentSpawn = Instantiate(_plasma, SpawnPos, Quaternion.identity, SpawnParent.transform);
        SpawnReady = true;



        if (transform.position.y >= 5.3f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void Shoot()
    {
        Vector3 Force = Input.mousePosition - SpawnScreenPos;
        CurrentSpawn.GetComponent<Rigidbody2D>().AddForce(new Vector3(Force.x, Force.y, Force.y) * _forceMultiplier);
        //CurrentSpawn.GetComponent<DragandShoot>().SetShoot();

        SpawnReady = false;

        Invoke( methodName: "SpawnNewObject", _newSpawnDuration);
    }

}
