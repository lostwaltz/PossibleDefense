
    using UnityEngine;

    public class GizmoVisualizer: MonoBehaviour
    {
        public float radius = 1.0f; // ���� ������

        void OnDrawGizmos()
        {
            // Gizmos ���� ����
            Gizmos.color = Color.red;

            // ���� ������ �׸���
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
