using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class AbstractCanvas : MonoBehaviour
{
	protected static UIControl uiControl; //! The UI Controller

	/*! \brief Called on startup
	 */
	protected virtual void Awake()
	{
		uiControl = transform.parent.GetComponent<UIControl>();
	}

	/*! \brief Updates the object
	 */
	protected abstract void Update();

	/*! \brief Updates the ui for the tutorial
	 */
	public virtual void UpdateTutorialUI()
	{

	}
}
