using UnityEngine;
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
		m_InteractiveItem.OnClick += HandleClick;
		m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
	}
	
	
	private void OnDisable()
	{
		m_InteractiveItem.OnOver -= HandleOver;
		m_InteractiveItem.OnOut -= HandleOut;
		m_InteractiveItem.OnClick -= HandleClick;
		m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
	}
	
	
	//Handle the Over event
	private void HandleOver()
	{
		Debug.Log("Show over state");
	}
	
	
	//Handle the Out event
	private void HandleOut()
	{
		Debug.Log("Show out state");
	}
	
	
	//Handle the Click event
	private void HandleClick()
	{
		Debug.Log("Show click state");
	}
	
	
	//Handle the DoubleClick event
	private void HandleDoubleClick()
	{
		Debug.Log("Show double click");
	}
}