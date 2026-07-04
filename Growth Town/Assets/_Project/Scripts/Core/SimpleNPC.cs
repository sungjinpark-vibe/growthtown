using UnityEngine;

namespace LifeTown.Core
{
    public class SimpleNPC : MonoBehaviour
    {
        public float speed = 2f;
        public float changeDirInterval = 3f;
        
        private Vector3 targetDirection;
        private float timer;

        private void Start()
        {
            ChooseNewDirection();
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= changeDirInterval)
            {
                ChooseNewDirection();
                timer = 0;
            }

            // 이동
            transform.Translate(targetDirection * speed * Time.deltaTime, Space.World);

            // 맵 밖으로 안 나가게 제한 (-20 ~ 20)
            Vector3 pos = transform.position;
            if (pos.x < -18f || pos.x > 18f || pos.z < -18f || pos.z > 18f)
            {
                ChooseNewDirection();
                pos.x = Mathf.Clamp(pos.x, -18f, 18f);
                pos.z = Mathf.Clamp(pos.z, -18f, 18f);
                transform.position = pos;
            }

            // 부드럽게 회전
            if (targetDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);
            }
        }

        private void ChooseNewDirection()
        {
            float angle = Random.Range(0f, 360f);
            targetDirection = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)).normalized;
        }
    }
}
