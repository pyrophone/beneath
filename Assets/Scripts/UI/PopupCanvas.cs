using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PopupCanvas : AbstractCanvas
{
    private Transform panel; //! panel
	private Text headerText; //! The header text on the panel
	private Text bodyText; //! The body text on the panel
	private Button closeButton; //! The close button for the panel
    private Image ImgPile; //! image of Pile
    private Image ImgBlaise; //! image of st. Blaise


    /*! \brief Called on startup
	 */
    protected override void Awake()
	{
		base.Awake();

		panel = transform.Find("Panel");
		headerText = panel.Find("Header").GetComponent<Text>();
        ImgPile = panel.Find("ImgPile").GetComponent<Image>();
        ImgBlaise = panel.Find("ImgBlaise").GetComponent<Image>();
        bodyText = panel.Find("Scroll View").Find("Viewport").Find("Content").GetComponent<Text>();
		closeButton = panel.Find("CloseButton").GetComponent<Button>();
		closeButton.onClick.AddListener(OnCloseClick);
		panel.gameObject.SetActive(false);
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
		panel.gameObject.SetActive(true);
	}

    /*! \brief Populates the canvas with info
	 *
	 * \param (Marker) m - the marker
	 */
    public void PopulateCanvas(Marker m)
    {
        panel.gameObject.SetActive(true);

        // we ran out of time, for now this is hardcoded to appear with descriptions to two markers.
        // if we want to conintue, we will have to implement this with JSON

        headerText.text = m.MName;
        if (m.name == "q1.marker0")
        {
            bodyText.text = "At the Pile Gate to the Old Town, on the western side of the land walls, there is a stone bridge between two Gothic arches, which were designed by the esteemed architect Paskoje Miličević in 1471. \n\nThat bridge connects to another bridge, a wooden drawbridge which can be pulled up.During the republican era, the wooden drawbridge to the Pile Gate was hoisted each night with considerable pomp in a ceremony which delivered the city's keys to the Ragusan rector. Today, it spans a dry moat whose garden offers respite from crowds. Above the bridges, over the arch of town's principal gateway, there is a statue of city patron Saint Blaise(Croatian: Sveti Vlaho)";
            ImgPile.gameObject.SetActive(true);
            ImgBlaise.gameObject.SetActive(false);
        }
            
        else if (m.name == "q1.marker2")
        {
            bodyText.text = "The Church of St. Blaise is a Baroque church in Dubrovnik and one of the city's major sights. Saint Blaise (Croatian: Sveti Vlaho) is the patron saint of the city of Dubrovnik and formerly the protector of the independent Republic of Ragusa. \n\nThe church was built in 1715 by the Venetian architect and sculptor Marino Gropelli(1662 - 1728) on the foundations of the badly damaged Romanesque medieval church. He modeled the church on Sansovino's Venetian church of San Maurizio.";
            ImgPile.gameObject.SetActive(false);
            ImgBlaise.gameObject.SetActive(true);
        }
           
        else
            panel.gameObject.SetActive(false);

    }

    /*! \brief Callback for the close button
	 */
    private void OnCloseClick()
	{
		panel.gameObject.SetActive(false);
	}
}
