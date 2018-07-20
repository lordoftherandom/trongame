using UnityEngine.UI;
using UnityEngine;

public class OnBomb : MonoBehaviour {
    private HUDCommands HUD;
    private Vector3 detPoint;

	// Use this for initialization
	void Start () {
		HUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDCommands>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SetOffBomb()
    {
        if (HUD.BombUsed())
        {
            Debug.Log("Spawning Bomb");
            GameObject bomb = Instantiate((GameObject)Resources.Load("Bomb"), gameObject.transform.parent);
            bomb.GetComponent<Bomb>().HUD = HUD;
            bomb.transform.localPosition = transform.localPosition;
        }
        else
        {
            ;
        }
    }
}
