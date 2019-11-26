using UnityEngine;

public abstract class Floor : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 2f;
    public static Floor CurrentFloor { get; set; }
    public static Floor LastFloor { get; set; }
    public abstract MoveAxis MoveAxis { get; }
    public MoveDirection MoveDirection { get; set; } = MoveDirection.forward;
    public static GameObject floors;

    protected static float currentXPositionTweak = 0f;
    protected static float currentZPositionTweak = 0f;
    protected static float futureZPositionTweak = 0f;
    protected static float futureXPositionTweak = 0f;


    protected abstract void SliceFloor(float overlay);
    protected abstract float GetOverlay();
    protected abstract bool IsPlayerDead(float overlay);
    protected abstract void UpdateFuturePositionTweak(float overlay);
    public abstract void FixPosition();

    private void OnEnable()
    {
        CurrentFloor = this;
        if (LastFloor != null)
            transform.localScale = LastFloor.transform.localScale;
    }

    public virtual void Stop()
    {
        moveSpeed = 0;
        float overlay = GetOverlay();
        if (IsPlayerDead(overlay))
        {
            GameManager.IsPlayerDead = true;
        }
        else
        {
            SliceFloor(overlay);
            UpdateFuturePositionTweak(overlay);
            LastFloor = this;
        }
    }

    public static void Initialisation(Floor prefab)
    {
        if (LastFloor == null)
        {
            LastFloor = Instantiate(prefab);
            floors = GameObject.Find("Floors");
            if (!floors)
            {
                floors = new GameObject("Floors");
            }
            LastFloor.transform.SetParent(floors.transform);
            LastFloor.moveSpeed = 0;
            LastFloor.transform.position = new Vector3(0, LastFloor.transform.localScale.y / 2, 0);
            LastFloor.hideFlags = HideFlags.HideInInspector;
            LastFloor.name = "Floor0";
            GameManager.Restart += Reset;
        }
    }

    protected void SpawnFallingFloorPart(Vector3 position, Vector3 size, Vector3 force)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Falling part";
        cube.transform.SetParent(floors.transform);

        cube.GetComponent<MeshRenderer>().material.color = new Color32(200, 0, 0, 1);
        cube.transform.position = position;
        cube.transform.localScale = size;
        var rb = cube.AddComponent<Rigidbody>();

        rb.AddForce(force, ForceMode.Impulse);
        Destroy(cube, 2f);
    }

    private static void Reset()
    {
        CurrentFloor = null;
        LastFloor = null;
		floors = null;
        futureXPositionTweak = 0;
        futureZPositionTweak = 0;
        currentXPositionTweak = 0;
        currentZPositionTweak = 0;
        GameManager.Restart -= Reset;
    }
}
