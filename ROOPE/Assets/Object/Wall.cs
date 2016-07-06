﻿using UnityEngine;
using System.Collections;

public class Wall : Obstacle {

	public bool canDrop;
	private Rope rope;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			collideWithCharacter ();
		}

		if (other.tag == "Arrow") {
			Destroy (other.gameObject);
		}

	}

	// HP -1
	public override void collideWithCharacter ()
	{
		FindObjectOfType<GameManager> ().addHP (-1);
	}

	// if canDrop is true in Rope, Wall falls
	public void dropWall() 
	{
		GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	public override RopeCollisionType collideWithRopeHead(Rope rope) {
		this.rope = rope;
		if (isRopeAttachable () && !canDrop)
			return RopeCollisionType.CAN_ATTACH;
		else if (isRopeAttachable() && canDrop) {
			dropWall ();
			return RopeCollisionType.CAN_ATTACH_AND_DROP;
		} else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}


	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		if (isRopeAttachable ())
			return RopeCollisionType.CAN_ATTACH;
		else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}
}
