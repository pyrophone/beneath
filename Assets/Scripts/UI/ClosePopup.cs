using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*! \class ClosePopup
 *	\brief Closes the popup panel
 */
public class ClosePopup : MonoBehaviour
{
	private Button noButton; //! The no button

	/*! \brief Called when the object is initialized
	 */
	void Start()
	{
		noButton = gameObject.transform.Find("NoButton").GetComponent<Button>();
		noButton.onClick.AddListener(OnNoButtonClick);
	}

	/*! \brief Updates the object
	 */
	void Update()
	{

	}

	/*! \brief Closes the popup
	 */
	void OnNoButtonClick()
	{
		gameObject.SetActive(false);
	}
}
