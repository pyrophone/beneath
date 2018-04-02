using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
	private Button noButton;

	// Use this for initialization
	void Start()
	{
		noButton = gameObject.transform.Find("NoButton").GetComponent<Button>();
		noButton.onClick.AddListener(OnNoButtonClick);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnNoButtonClick()
	{
		gameObject.SetActive(false);
	}
}
