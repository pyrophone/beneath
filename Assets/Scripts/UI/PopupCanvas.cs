using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PopupCanvas : AbstractCanvas
{
	private Text headerText; //! The header text on the panel
	private Text bodyText; //! The body text on the panel
	private Button closeButton; //! The close button for the panel

	/*! \brief Called on startup
	 */
	private void Awake()
	{
		base.Awake();

		Transform panel = transform.Find("Panel");
		headerText = panel.Find("Header").GetComponent<Text>();
		bodyText = panel.Find("Scroll View").Find("Viewport").Find("Content").Find("Body").GetComponent<Text>();
		closeButton = panel.Find("CloseButton").GetComponent<Button>();
		closeButton.onClick.AddListener(OnCloseClick);
		gameObject.SetActive(false);
	}

	/*! \brief Called when the object is initialized
	 */
	private void Start()
	{

	}

	/*! \brief Updates the object
	 */
	protected override void Update()
	{

	}

	/*! \brief Populates the canvas with info
	 *
	 * \param (string) header - The text for the header
	 * \param (string) body - The text for the body
	 */
	public void PopulateCanvas(string header, string body)
	{
		headerText.text = header;
		bodyText.text = body;
		gameObject.SetActive(true);
	}

	/*! \brief Callback for the close button
	 */
	private void OnCloseClick()
	{
		gameObject.SetActive(false);
	}
}
