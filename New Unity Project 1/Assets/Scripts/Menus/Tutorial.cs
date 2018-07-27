using UnityEngine.UI;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public Text healthExp, pointsExp, bombsExp,
        powerUpsExp, fckWeUnderAttack, movement,
        gotIt;

    public GameObject mainMenu;

	// Use this for initialization
	void Start () {
        healthExp.text = Strings.healthPower;
        pointsExp.text = Strings.pointPower;
        bombsExp.text = Strings.bombPower;
        movement.text = Strings.moveIntro;
        gotIt.text = Strings.gotIt;
        fckWeUnderAttack.text = Strings.intro;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnGotItPressed()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
