using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
	private UIControl uiControl;
	private Button backButton;

	// Use this for initialization
	private void Start()
	{
		uiControl = transform.parent.GetComponent<UIControl>();
		backButton = transform.Find("BackButton").GetComponent<Button>();
		backButton.onClick.AddListener(OnBackButtonClick);
	}

	// Update is called once per frame
	private void Update()
	{

	}

	private void OnBackButtonClick()
	{
		uiControl.SetCanvas(UIState.MAP);
	}
}
