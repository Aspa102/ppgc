using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scalething : MonoBehaviour
{
	public Sprite spritey;

	void Start()
    {
		var croppedTexture = new Texture2D((int)spritey.rect.width, (int)spritey.rect.height);
		var pixels = spritey.texture.GetPixels((int)spritey.textureRect.x,
												(int)spritey.textureRect.y,
												(int)spritey.textureRect.width,
												(int)spritey.textureRect.height);
		croppedTexture.SetPixels(pixels);
		croppedTexture.wrapMode = TextureWrapMode.Repeat;
		croppedTexture.filterMode = FilterMode.Point;
		croppedTexture.Apply();
		var copy = Sprite.Create(croppedTexture, new Rect(0f, 0f, (float)croppedTexture.width, (float)croppedTexture.height), 0.5f * Vector2.one, 70f, 0, SpriteMeshType.FullRect);
		gameObject.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
		gameObject.GetComponent<SpriteRenderer>().sprite = copy;
	}
}
