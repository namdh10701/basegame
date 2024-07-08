using _Game.Features.Gameplay;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Editor
{
    [ExecuteAlways]
    public class ShipGridGenerator : EditorWindow
    {
        public ShipGridProfile ShipGridProfile;
        public Transform root;
        [MenuItem("Window/Ship Grid Generator")]
        static void Init()
        {
            ShipGridGenerator window = (ShipGridGenerator)EditorWindow.GetWindow(typeof(ShipGridGenerator), false, "Grid Generator");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Ship Grid Generator", EditorStyles.boldLabel);

            ShipGridProfile = (ShipGridProfile)EditorGUILayout.ObjectField("Ship Grid Profile", ShipGridProfile, typeof(ShipGridProfile), false);
            root = (Transform)EditorGUILayout.ObjectField("Root", root, typeof(Transform), true);

            if (ShipGridProfile == null || root == null)
            {
                GUILayout.Label("Select profile and root", EditorStyles.boldLabel);
                return;
            }
            if (GUILayout.Button("Generate From Profile"))
            {
                Generate();
            }
        }

        void Generate()
        {
            foreach (GridDefinition gridDefinition in ShipGridProfile.GridDefinitions)
            {
                GameObject gridRoot = new GameObject();
                _Game.Features.Gameplay.Grid grid = gridRoot.AddComponent<_Game.Features.Gameplay.Grid>();
                gridRoot.transform.parent = root;
                gridRoot.transform.localPosition = gridDefinition.rootPosition;
                gridRoot.name = "Grid";

                GameObject cellRoot = new GameObject();
                cellRoot.transform.parent = gridRoot.transform;
                cellRoot.transform.localPosition = Vector3.zero;
                cellRoot.name = "Cells";
                GameObject gridItemRoot = new GameObject();
                gridItemRoot.transform.parent = gridRoot.transform;
                gridItemRoot.name = "GridItems";
                grid.CellRoot = cellRoot.transform;
                grid.GridItemRoot = gridItemRoot.transform;
                for (int j = 0; j < gridDefinition.Row; j++)
                {
                    for (int k = 0; k < gridDefinition.Col; k++)
                    {
                        Vector3 position = new Vector3(k * gridDefinition.Spaces.x, j * gridDefinition.Spaces.y, 0);

                        GameObject cellObject = ((Cell)PrefabUtility.InstantiatePrefab(gridDefinition.CellPrefab, cellRoot.transform)).gameObject;

                        cellObject.transform.localPosition = position;
                        Cell cell = cellObject.GetComponent<Cell>();
                        cell.X = k;
                        cell.Y = j;
                        cell.Grid = gridRoot.GetComponent<_Game.Features.Gameplay.Grid>();

                    }
                }
            }
        }
    }
}