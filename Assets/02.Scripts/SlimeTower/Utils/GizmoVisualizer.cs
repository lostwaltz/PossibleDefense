
    using UnityEngine;

    public class GizmoVisualizer: MonoBehaviour
    {
        public float radius = 1.0f; // 구의 반지름

        void OnDrawGizmos()
        {
            // Gizmos 색상 설정
            Gizmos.color = Color.red;

            // 구의 범위를 그리기
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
