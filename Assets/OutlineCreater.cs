using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class OutlineCreater : MonoBehaviour
{
	public GameObject selectionOutlineObject;
	public Material mater;

	void Start()
    {
		this.selectionOutlineObject = new GameObject("Outline");
		this.selectionOutlineObject.layer = 8;
		SpriteRenderer spriteRenderer = this.selectionOutlineObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sortingLayerName = "Top";
		spriteRenderer.sortingOrder = int.MaxValue;
		spriteRenderer.transform.SetParent(base.transform, false);
		spriteRenderer.sharedMaterial = mater;
		SpriteRenderer component = this.selectionOutlineObject.GetComponent<SpriteRenderer>();
		component.sprite = base.GetComponent<SpriteRenderer>().sprite;
		component.sortingLayerID = SortingLayer.NameToID("Background");
		component.sortingOrder = 5;
		component.color = new Color32(18, 229, 32, 160);
		var propertyBlock = new MaterialPropertyBlock();
		component.GetPropertyBlock(propertyBlock);
		Vector2 vector = new Vector2((float)component.sprite.texture.width, (float)component.sprite.texture.height);
		Vector2 min = GetMin<Vector2>(component.sprite.uv, (Vector2 v) => v.sqrMagnitude);
		Vector2 vector2 = new Vector2(component.sprite.rect.width, component.sprite.rect.height);
		Vector4 value = new Vector4(min.x, min.y, vector2.x / vector.x, vector2.y / vector.y);
		propertyBlock.SetVector("_AtlasTransform", value);
		propertyBlock.SetTexture("_MainTex", component.sprite.texture);
		component.SetPropertyBlock(propertyBlock);
		selectionOutlineObject.transform.localScale = new Vector2(1.1f, 1.1f);
		if (base.GetComponent<SpriteRenderer>().sprite.name == "TriTile")
        {
			selectionOutlineObject.transform.localPosition = new Vector2(0.05f, 0.05f);
		}
		selectionOutlineObject.SetActive(true);
	}
	public static T GetMin<T>(ICollection<T> collection, Func<T, float> singleFunc)
	{
		float num = float.MaxValue;
		T result = default(T);
		foreach (T t in collection)
		{
			float num2 = singleFunc(t);
			if (num2 < num)
			{
				num = num2;
				result = t;
			}
		}
		return result;
	}
}
