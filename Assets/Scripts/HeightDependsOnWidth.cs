

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightDependsOnWidth : MonoBehaviour
{
	private enum DependType
	{
		WidthMinus20Div2,
		Equal
	}

	[SerializeField]
	private DependType m_dependType = DependType.Equal;

	private RectTransform m_rectTransform;

	private float m_width = -1f;

	private void Start()
	{
		this.m_rectTransform = (RectTransform)base.transform;
		this.UpdateHeight();
	}

	private void Update()
	{
		base.StartCoroutine(this.UpdateHeightCoroutine());
		base.enabled = false;
	}

	private void UpdateHeight()
	{
		Vector2 sizeDelta = this.m_rectTransform.sizeDelta;
		this.m_width = sizeDelta.x;
		float num = 0f;
		switch (this.m_dependType)
		{
			case DependType.Equal:
				num = this.m_width;
				break;
			case DependType.WidthMinus20Div2:
				num = (this.m_width - 20f) / 2f;
				break;
		}
		Vector2 sizeDelta2 = this.m_rectTransform.sizeDelta;
		sizeDelta2.y = num;
		this.m_rectTransform.sizeDelta = sizeDelta2;
		LayoutElement component = base.GetComponent<LayoutElement>();
		if (component != null)
		{
			LayoutElement layoutElement = component;
			component.preferredHeight = num; float num4 = layoutElement.minHeight = (component.preferredHeight);
		}
	}
	private IEnumerator UpdateHeightCoroutine()
	{
		yield return null;
		this.UpdateHeight();
	}
}