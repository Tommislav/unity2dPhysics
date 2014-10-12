

public class CollisionState2D {
	public bool IsCollidingLeft { get; set; }
	public bool IsCollidingRight { get; set; }
	public bool IsCollidingUp { get; set; }
	public bool IsCollidingDown { get; set; }

	public bool IsOnGround { get { return IsCollidingDown; } }

	public bool HasCollisionX {get { return IsCollidingLeft || IsCollidingRight;}}
	public bool HasCollisionY {get { return IsCollidingUp || IsCollidingDown;}}
	public bool HasCollision {get { return HasCollisionX || HasCollisionY;}}

	public void Reset() {
		IsCollidingUp = IsCollidingDown = IsCollidingLeft = IsCollidingRight = false;
	}
}
