using UnityEngine;

namespace _Base.Scripts.Utils
{
	public class BackgroundScaler : MonoBehaviour
	{
		[SerializeField] public SpriteRenderer background;
		void Start()
		{
			transform.localScale = new Vector3(1, 1, 1);

			float width = background.sprite.bounds.size.x;
			float height = background.sprite.bounds.size.y;

			float worldScreenHeight = Camera.main.orthographicSize * 2;
			float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
			transform.localScale = new Vector3(worldScreenWidth / width * 1.1f, worldScreenHeight / height * 1.1f,1);
		}
	}
}
