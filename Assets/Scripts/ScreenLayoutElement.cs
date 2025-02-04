

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class ScreenLayoutElement : MonoBehaviour
{
	private RectTransform m_canvas;

	[SerializeField]
	private float m_extends;

	[SerializeField]
	private bool m_onlyWidth;

	[SerializeField]
	private bool m_heightDependsOnWidth;

	[SerializeField]
	private float m_extraHeight;

	private void Awake()
	{
		LayoutElement component = base.GetComponent<LayoutElement>();
		this.FindCanvas();
		if (this.m_canvas != null)
		{
			float num = Mathf.Min(this.m_canvas.rect.width, this.m_canvas.rect.height);
			component.preferredWidth = num - this.m_extends * 2f;
			component.minWidth = num - this.m_extends * 2f;
			if (!this.m_onlyWidth)
			{
				component.preferredHeight = num - this.m_extends * 2f;
				component.minHeight = num - this.m_extends * 2f;
				if (this.m_heightDependsOnWidth)
				{
					LayoutElement layoutElement = component;
					component.minHeight = component.minWidth + this.m_extraHeight; float num4 = layoutElement.preferredHeight = (component.minHeight);
				}
			}
		}
	}

	private void FindCanvas()
	{
		Transform transform = base.transform;
		while (true)
		{
			if (transform != null)
			{
				Canvas component = ((Component)transform).GetComponent<Canvas>();
				if (!(component != null))
				{
					transform = transform.parent;
					continue;
				}
				break;
			}
			return;
		}
		this.m_canvas = (transform as RectTransform);
	}
}


