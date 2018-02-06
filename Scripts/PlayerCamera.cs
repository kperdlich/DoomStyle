using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    [SerializeField]
    private Transform _player;		
	
	void LateUpdate ()
    {
        transform.position = _player.position;
        transform.rotation = _player.rotation;
    }    
}
