using UnityEngine;

public class FloorZ : Floor
{
	public override MoveAxis MoveAxis { get; } = MoveAxis.Z;
	
	void Update()
	{
		transform.position += (float)MoveDirection * transform.forward * Time.deltaTime * moveSpeed;
	}

	protected override void SliceFloor(float overlay)
	{
        float newSize = LastFloor.transform.localScale.z - Mathf.Abs(overlay);
        float newPosition = LastFloor.transform.transform.position.z + overlay / 2;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);

        float direction = Mathf.Sign(overlay);
        float fallingPartZSize = LastFloor.transform.localScale.z - newSize;
        float fallingPartZPosition = transform.position.z + (newSize / 2 + fallingPartZSize / 2) * direction;
        var fallingPartPosition = new Vector3(transform.position.x, transform.position.y, fallingPartZPosition);
        var fallingPartSize = new Vector3(transform.localScale.x, transform.localScale.y, fallingPartZSize);
        var force = new Vector3(0, 0.85f, direction * 1.8f);
        SpawnFallingFloorPart(fallingPartPosition, fallingPartSize, force);
	}

	protected override float GetOverlay()
	{
		return transform.position.z - LastFloor.transform.position.z;
	}

	protected override bool IsPlayerDead(float overlay)
	{
		return Mathf.Abs(overlay) >= transform.localScale.z;
	}

	protected override void UpdateFuturePositionTweak(float overlay)
	{
		float direction = Mathf.Sign(overlay);
		float newSize = LastFloor.transform.localScale.z - Mathf.Abs(overlay);
		float fallingPartXSize = LastFloor.transform.localScale.z - newSize;
		futureZPositionTweak += (direction) * fallingPartXSize / 2;
	}

	public override void FixPosition()
	{
		if (MoveAxis != LastFloor.MoveAxis)
		{
            if (MoveAxis == MoveAxis.Z)
            {
                currentXPositionTweak = Floor.futureXPositionTweak;
            }
		}
		transform.position += new Vector3(currentXPositionTweak, 0, currentZPositionTweak);
	}
}