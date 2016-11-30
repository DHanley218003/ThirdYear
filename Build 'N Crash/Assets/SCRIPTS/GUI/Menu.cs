using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using VRStandardAssets.Utils;

public class Menu : MonoBehaviour
{
	[SerializeField] private VRInteractiveItem m_InteractiveItem;

	private void OnEnable()
	{
		m_InteractiveItem.OnOver += HandleOver;
		m_InteractiveItem.OnOut += HandleOut;
	}
	
	
	private void OnDisable()
	{
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
	}
	
	
	//Handle the Over event
	private void HandleOver()
	{
		this.gameObject.GetComponent<Button> ().Select();
		this.gameObject.GetComponent<SelectionRadial> ().Show();
	}
	
	
	//Handle the Out event
	private void HandleOut()
	{
		this.gameObject.GetComponent<SelectionRadial> ().Hide();
	}

	private void Update()
	{
		if (this.gameObject.GetComponent<SelectionRadial> ().RadialFilled)
			this.gameObject.GetComponent<Button> ().onClick.Invoke ();
	}
}