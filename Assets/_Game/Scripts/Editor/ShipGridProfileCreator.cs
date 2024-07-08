using _Game.Features.Gameplay;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Editor
{
    [ExecuteAlways]
    public class ShipGridProfileCreator : EditorWindow
    {
        const string directory = "Assets/_Game/Scriptable Objects/ShipGridProfiles/";
        [SerializeField] ShipGridProfile ShipGridProfile;

        [SerializeField] string profileName;
        [SerializeField] Transform[] gridRoots;
        [SerializeField] GridDefinition[] gridDefinitions;
        [MenuItem("Window/Ship Grid Profile Creator")]
        static void Init()
        {
            ShipGridProfileCreator window = (ShipGridProfileCreator)EditorWindow.GetWindow(typeof(ShipGridProfileCreator), false, "Ship Grid Profile Creator");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Profile Name", EditorStyles.boldLabel);
            profileName = EditorGUILayout.TextField("Profile name", profileName);

            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty stringsProperty = so.FindProperty("gridRoots");
            SerializedProperty stringsProperty2 = so.FindProperty("gridDefinitions");
            EditorGUILayout.PropertyField(stringsProperty, true);
            EditorGUILayout.PropertyField(stringsProperty2, true);

            so.ApplyModifiedProperties();
            ShipGridProfile = (ShipGridProfile)EditorGUILayout.ObjectField("Ship Grid Profile", ShipGridProfile, typeof(ShipGridProfile), false);
            if (GUILayout.Button("Load Profile"))
            {
                LoadProfile();
            }
            if (gridDefinitions == null)
            {
                return;
            }
            if (gridDefinitions.Length != gridRoots.Length)
            {
                if (GUILayout.Button("ApllyRoots"))
                {
                    ApplyRoots();
                }
            }
            if (gridDefinitions.Length == gridRoots.Length)
            {
                if (GUILayout.Button("Update Roots Position"))
                {
                    UpdateRootsPosition();
                }
            }
            if (gridDefinitions == null || gridDefinitions.Length != gridRoots.Length)
            {
                return;
            }
            if (GUILayout.Button("Try Generate Grids"))
            {
                Generate();
            }
            if (GUILayout.Button("Create Profile"))
            {
                CreateProfile();
            }

            GUILayout.Label($"Profiles will be created in directory: {directory}", EditorStyles.whiteLabel);
        }

        void Generate()
        {
            // Clear existing cells under the root transform
            foreach (Transform root in gridRoots)
            {
                foreach (Transform child in root)
                {
                    DestroyImmediate(child.gameObject);
                }
            }

            for (int i = 0; i < gridRoots.Length; i++)
            {
                Transform root = gridRoots[i];
                GridDefinition gridDefinition = gridDefinitions[i];
                for (int j = 0; j < gridDefinition.Row; j++)
                {
                    for (int k = 0; k < gridDefinition.Col; k++)
                    {
                        Vector3 position = new Vector3(k * gridDefinition.Spaces.x, j * gridDefinition.Spaces.y, 0);

                        GameObject cellObject = ((Cell)PrefabUtility.InstantiatePrefab(gridDefinition.CellPrefab, root)).gameObject;

                        cellObject.transform.localPosition = position;

                    }
                }
            }
        }
        void LoadProfile()
        {
            profileName = ShipGridProfile.name;
            gridDefinitions = ShipGridProfile.GridDefinitions;
        }
        void UpdateRootsPosition()
        {
            for (int i = 0; i < gridDefinitions.Length; i++)
            {
                gridDefinitions[i].rootPosition = gridRoots[i].localPosition;
            }
        }
        void ApplyRoots()
        {
            gridDefinitions = new GridDefinition[gridRoots.Length];

        }

        void CreateProfile()
        {
            if (string.IsNullOrEmpty(profileName))
            {
                Debug.LogError("Profile name cannot be empty.");
                return;
            }
            // Create a new instance of ShipGridProfile
            ShipGridProfile newProfile = ScriptableObject.CreateInstance<ShipGridProfile>();
            newProfile.name = profileName;
            newProfile.GridDefinitions = gridDefinitions;

            // Construct the file path
            string filePath = $"{directory}/{profileName}.asset";

            // Create the asset and save it to the specified directory
            AssetDatabase.CreateAsset(newProfile, filePath);
            AssetDatabase.SaveAssets();

            // Refresh the AssetDatabase to ensure the new asset is visible
            AssetDatabase.Refresh();
            Debug.Log($"ShipGridProfile '{profileName}' created at: {filePath}");
        }
    }
}