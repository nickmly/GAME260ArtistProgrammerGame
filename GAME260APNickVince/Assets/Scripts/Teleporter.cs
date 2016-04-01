using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {
	[SerializeField]
	private Teleporter otherTeleporter;
	public Transform spawnPoint;

	void Teleport(Transform transformToTeleport){
		transformToTeleport.position = otherTeleporter.spawnPoint.transform.position;
	}

	void OnTriggerEnter(Collider col){
		Teleport (col.transform);
	}
}
