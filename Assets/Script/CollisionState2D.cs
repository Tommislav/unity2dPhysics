

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

	public void SetCollisionX(int dir) {
		if (dir < 0) IsCollidingLeft = true;
		else if (dir > 0) IsCollidingRight = true;
	}
	public void SetCollisionY(int dir) {
		if (dir < 0) IsCollidingDown = true;
		else if (dir > 0) IsCollidingUp = true;
	}

	public string Debug() {
		return "Coll=D:" + boolToStr(IsCollidingDown) + ", U:" + boolToStr(IsCollidingUp) + ", L:" + boolToStr(IsCollidingLeft) + ", R:" + boolToStr(IsCollidingRight);
	}
	private string boolToStr(bool flag) {
		return flag ? "1" : "0";
	}
}
