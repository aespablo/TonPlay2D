﻿using UnityEngine;


namespace FrenzyCircle
{
    public class ObstacleRotation : MonoBehaviour
    {
        public float rot;
        public bool rotateForward = true;
        private float _timer;
        private SpriteRenderer _sp;

        private void Start()
        {
            rot = Random.Range(50f, 100f);

            _sp = GetComponent<SpriteRenderer>();
            UpdateColor();
        }

        private void FixedUpdate()
        {
            transform.Rotate(0, 0, (rotateForward ? 1 : -1) * rot * Time.deltaTime);

            _timer += Time.deltaTime;
            if (_timer > 3f)
            {
                _timer = 0f;
                Vars.ObstacleScaleSpeed += 0.0000025f;
            }

            transform.localScale = new Vector2(
                transform.localScale.x - (0.0005f + Vars.ObstacleScaleSpeed),
                transform.localScale.y - (0.0005f + Vars.ObstacleScaleSpeed)
            );

            if (transform.localScale.x < 0f)
            {
                gameObject.SetActive(false);
            }

            UpdateColor();
        }

        private void UpdateColor()
        {
            // 색상 조정
            Color c = Color.white;
            float normalizedScale = Mathf.InverseLerp(0.2f, 0.8f, transform.localScale.x);

            // 비선형 보간을 반대로 적용
            float colorFactor = 1.0f - normalizedScale + .25f;

            c.r = c.g = c.b = colorFactor;
            _sp.color = c;
        }
    }
}
