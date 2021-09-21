using UnityEngine;

namespace Assets.UnityFoundation.Systems.BuildingPlacementSystem
{
    [CreateAssetMenu(fileName = "new_object", menuName = "BuildingPlacementSystem/Object")]
    public class GridObjectSO : ScriptableObject
    {
        [SerializeField] private string buildingName;
        [SerializeField] private string tag;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private GameObject objPrefab;

        public int Width => width;
        public int Height => height;
        public GameObject Prefab => objPrefab;
        public string BuildingName => buildingName;
        public string Tag => tag;

        public override string ToString()
        {
            return objPrefab.name;
        }
    }
}