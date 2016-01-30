using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Player))]
public class Input : MonoBehaviour {

    protected Player m_player;

	// Use this for initialization
	void Start () {
        m_player = GetComponent<Player>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
