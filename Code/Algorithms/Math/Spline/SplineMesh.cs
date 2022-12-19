using UnityEngine;
using UnityFoundation.Code.MeshBuilders;
using UnityFoundation.Tools.Spline;

namespace UnityFoundation.Code.Spline
{
    public class SplineMesh : MonoBehaviour
    {
        [SerializeField] private SplineMono spline;

        [Range(min: 10, max: 200)]
        [SerializeField] private float meshSubdivisions = 100f;
        [SerializeField] private float meshWidth = 2f;

        private Mesh mesh;
        private MeshFilter meshFilter;

        private void Awake()
        {
            if(spline == null) spline = GetComponent<SplineMono>();
            meshFilter = GetComponent<MeshFilter>();
        }

        void Start()
        {
            transform.position = spline.transform.position;

            spline.OnSplineUpdated += () => UpdateMesh();

            UpdateMesh();
        }

        private void UpdateMesh()
        {
            if(mesh != null)
            {
                mesh.Clear();
                Destroy(mesh);
                mesh = null;
            }

            if(spline.Anchors.Count < 2) return;

            var meshBuilder = new LineMeshBuilder()
                .Width(meshWidth);

            var meshStep = 1 / meshSubdivisions;
            for(var size = 0f; size <= 1f; size += meshStep)
            {
                meshBuilder.AddPoint(spline.GetPosition(size));
            }

            mesh = meshBuilder.Build();
            meshFilter.mesh = mesh;
        }
    }
}