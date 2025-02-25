using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CanvasRenderer))]
public class TestImg : MaskableGraphic,IPointerClickHandler
{
	[SerializeField]
	Texture2D texture2D;
	protected override void OnPopulateMesh(VertexHelper vh)
	{

		
		vh.Clear();

		var r = GetPixelAdjustedRect();
		var vLeft = new Vector4(r.x, r.y, r.x + r.width/2, r.y + r.height);//×ó±ß¾ØÐÎ
		var vRight = new Vector4(r.x + r.width / 2, r.y , r.x + r.width , r.y + r.height );//ÓÒ±ß

		Color32 color32 = color;
		vh.AddVert(new Vector3(vLeft.x, vLeft.y), color32, new Vector2(0f, 0f));
		vh.AddVert(new Vector3(vLeft.x, vLeft.w), color32, new Vector2(0f, 1f));
		vh.AddVert(new Vector3(vLeft.z, vLeft.w), color32, new Vector2(1f, 1f));
		vh.AddVert(new Vector3(vLeft.z, vLeft.y), color32, new Vector2(1f, 0f));

		vh.AddVert(new Vector3(vRight.x, vRight.y), color32, new Vector2(1f, 0f));
		vh.AddVert(new Vector3(vRight.x, vRight.w), color32, new Vector2(1f, 1f));
		vh.AddVert(new Vector3(vRight.z, vRight.w), color32, new Vector2(0f, 1f));
		vh.AddVert(new Vector3(vRight.z, vRight.y), color32, new Vector2(0f, 0f));

		vh.AddTriangle(0, 1, 2);
		vh.AddTriangle(2, 3, 0);

		vh.AddTriangle(4, 5, 6);
		vh.AddTriangle(6, 7, 4);


		//vh.AddVert(Vector3.zero, Color.red, new Vector2(0, 0));
		//vh.AddVert(new Vector3(156,0,0), Color.red, new Vector2(1, 0));
		//vh.AddVert(new Vector3(0, 95, 0), Color.red, new Vector2(0, 1));
		//vh.AddVert(new Vector3(156, 95, 0), Color.red, new Vector2(1, 1));

		//vh.AddVert(new Vector3(156, 0, 0), Color.red, new Vector2(1, 0));
		//vh.AddVert(new Vector3(312, 0, 0), Color.red, new Vector2(0, 0));
		//vh.AddVert(new Vector3(156, 95, 0), Color.red, new Vector2(1, 1));
		//vh.AddVert(new Vector3(312, 95, 0), Color.red, new Vector2(0, 1));
		//vh.AddTriangle(0, 2, 3);
		//vh.AddTriangle(0, 3, 1);
		//vh.AddTriangle(4, 6, 7);
		//vh.AddTriangle(4, 7, 5);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("11111111111111");
	}

	public override Texture mainTexture
	{
		get
		{
			return texture2D;
		}
	}
}
