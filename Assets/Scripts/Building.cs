using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsFirst = true;

    [SerializeField] private Camera cam;

    [Tooltip("Ignore preview layer")]
    [SerializeField] private LayerMask layer;

    private GameObject prefab;
    private Preview preview;
    private MeshRenderer mesh;
    private BuildingDatabase db;
    private bool IsBuilding = false;

    void Awake()
    {
        db = BuildingDatabase.Instance;
        CreatePreview(db.Current());
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            IsBuilding = !IsBuilding;
        }

        if (IsBuilding)
        {
            prefab?.SetActive(true);
        }
        else
        {
            prefab?.SetActive(false);
        }

        if (IsBuilding)
        {

            if (Input.mouseScrollDelta.y != 0)
            {
                SwitchItem();
            }

            MovePreview();

            if (Input.GetMouseButtonDown(0) && (preview.IsSnapped || IsFirst))
            {
                Build();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Rotate();
            }
        }
    }

    private void SwitchItem()
    {
        GameObject template;

        if (Input.mouseScrollDelta.y > 0)
        {
            template = db.Next();
        }
        else
        {
            template = db.Prev();
        }

        if (prefab != null)
        {
            Destroy(prefab);
        }

        CreatePreview(template);
    }

    private void MovePreview()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 30f, layer))
        {
            prefab.transform.position = new Vector3(
                        Mathf.Round(hit.point.x),
                        Mathf.Round(hit.point.y),
                        Mathf.Round(hit.point.z)
                    );
        }
    }

    private void Rotate()
    {
        prefab.transform.Rotate(0, 90, 0);
    }

    private void Build()
    {
        preview.Build();

        CreatePreview(db.Current());

        IsFirst = false;
    }

    private void CreatePreview(GameObject template)
    {
        prefab = null;
        prefab = Instantiate(template);

        preview = prefab.GetComponent<Preview>();
        mesh = prefab.GetComponent<MeshRenderer>();
    }
}
