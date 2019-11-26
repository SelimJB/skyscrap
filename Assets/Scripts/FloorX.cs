using UnityEngine;

public class FloorX : Floor
{
	public override MoveAxis MoveAxis { get; } = MoveAxis.X;
	
	void Update()
	{
		transform.position += (float)MoveDirection * transform.right * Time.deltaTime * moveSpeed;
	}

	protected override void SliceFloor(float overlay)
	{
		float newSize = LastFloor.transform.localScale.x - Mathf.Abs(overlay);
		float newPosition = LastFloor.transform.transform.position.x + overlay / 2;
		transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
		transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);

		float direction = Mathf.Sign(overlay);
		float fallingPartXSize = LastFloor.transform.localScale.x - newSize;
		float fallingPartXPosition = transform.position.x + (newSize / 2 + fallingPartXSize / 2) * direction;
		var fallingPartPosition = new Vector3(fallingPartXPosition, transform.position.y, transform.position.z);
		var fallingPartSize = new Vector3(fallingPartXSize, transform.localScale.y, transform.localScale.z);
		var force = new Vector3(direction * 1.8f, 0.85f, 0);
		SpawnFallingFloorPart(fallingPartPosition, fallingPartSize, force);
	}

	protected override float GetOverlay()
	{
		return transform.position.x - LastFloor.transform.position.x;
	}

	protected override bool IsPlayerDead(float overlay)
	{
		return Mathf.Abs(overlay) >= transform.localScale.x;
	}

	protected override void UpdateFuturePositionTweak(float overlay)
	{
		float direction = Mathf.Sign(overlay);
		float newSize = LastFloor.transform.localScale.x - Mathf.Abs(overlay);
		float fallingPartXSize = LastFloor.transform.localScale.x - newSize;
		futureXPositionTweak += (direction) * fallingPartXSize / 2;
	}

	public override void FixPosition()
	{
		if (MoveAxis != LastFloor.MoveAxis)
		{
			if (MoveAxis == MoveAxis.X)
			{
				currentZPositionTweak = Floor.futureZPositionTweak;
			}
		}
		transform.position += new Vector3(currentXPositionTweak, 0, currentZPositionTweak);
	}
}