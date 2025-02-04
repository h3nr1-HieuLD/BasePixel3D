using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeReceiver : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
{
	public Action OnSwipeLeft;

	public Action OnSwipeRight;

	public Action OnSkipIt;

	private Vector2 m_pos = Vector2.zero;

	private Vector2 m_lastPos = Vector2.zero;

	private DateTime m_lastTime = DateTime.MinValue;

	private bool m_skip;

	public void OnBeginDrag(PointerEventData eventData)
	{
		this.m_lastTime = DateTime.Now;
		this.m_pos = eventData.position;
		this.m_skip = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.m_lastTime = DateTime.Now;
		this.m_lastPos = eventData.position;
		float num = this.m_lastPos.y - this.m_pos.y;
		if (!(num > 50f) && !(num < -50f) && !this.m_skip)
		{
			return;
		}
		if (!this.m_skip)
		{
			this.m_skip = true;
			base.GetComponentInParent<ScrollRect>().SendMessage("OnBeginDrag", eventData);
		}
		else
		{
			base.GetComponentInParent<ScrollRect>().SendMessage("OnDrag", eventData);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (this.m_skip)
		{
			base.GetComponentInParent<ScrollRect>().SendMessage("OnEndDrag", eventData);
		}
		else if (!((DateTime.Now - this.m_lastTime).TotalSeconds > 0.3))
		{
			Vector2 vector = eventData.position - this.m_pos;
			float x = vector.x;
			Vector2 vector2 = eventData.position - this.m_lastPos;
			if (x * vector2.x >= 0f)
			{
				if (vector.x > 100f)
				{
					this.OnSwipeRight.SafeInvoke();
				}
				else if (vector.x < -100f)
				{
					this.OnSwipeLeft.SafeInvoke();
				}
			}
		}
	}
}


