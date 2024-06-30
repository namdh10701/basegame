using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Map
{
    public class DottedLineRenderer : MonoBehaviour
    {
        public bool scaleInUpdate = false;
        private UILineRenderer lR;
        private Material rend;


        private void Start()
        {
            lR = GetComponent<UILineRenderer>();
            rend = Instantiate(lR.material);
            lR.material = rend;
            ScaleMaterial();
        }

        public void ScaleMaterial()
        {
            rend.mainTextureScale =
                new Vector2(Vector2.Distance(lR.GetPosition(0), lR.GetPosition(lR.Points.Length - 1)) / lR.lineThickness,
                    1);

        }

        private void Update()
        {
            rend.mainTextureScale =
                new Vector2(Vector2.Distance(lR.GetPosition(0), lR.GetPosition(lR.Points.Length - 1)) / lR.lineThickness,
                    1);
        }
    }
}