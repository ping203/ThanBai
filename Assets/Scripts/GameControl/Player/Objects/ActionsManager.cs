using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionsManager : MonoBehaviour {

	public GameObject panelActions;
	public Toggle toggleAction;
	public Button buttonInvite;

	// Use this for initialization
	void Start () {
		panelActions.gameObject.SetActive (false);
		/*toggleAction.value = false;
		buttonInvite.gameObject.SetActive (true);*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onHide (bool av) {
		panelActions.gameObject.SetActive (!av);
		toggleAction.isOn = !av;
		buttonInvite.gameObject.SetActive (av);
	}
}
