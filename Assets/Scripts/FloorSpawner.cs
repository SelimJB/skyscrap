using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
	[SerializeField]
	private Floor floorXPrefab;
	[SerializeField]
	private Floor floorZPrefab;
	[SerializeField]
	private MoveAxis moveAxis;
	[SerializeField]
	private MoveDirection moveDirection;

	public void OnEnable()
	{
		Floor.Initialisation(floorXPrefab);
	}
	
	public void Spawn()
	{
		Floor floor;
		if (moveAxis == MoveAxis.X)
			floor = Instantiate(floorXPrefab);
		else 
			floor = Instantiate(floorZPrefab);
		floor.MoveDirection = moveDirection;
		float floorYPosition = Floor.LastFloor.transform.position.y + floorXPrefab.transform.localScale.y;
		floor.transform.position = new Vector3(transform.position.x, floorYPosition, transform.position.z);
		floor.FixPosition();
		floor.name = "Floor";
		floor.transform.SetParent(Floor.floors.transform);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, floorXPrefab.transform.localScale);
	}
}