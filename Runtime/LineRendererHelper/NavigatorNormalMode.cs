using System;
using UnityEngine;

namespace GiangCustom.Runtime.LineRendererHelper
{
    [RequireComponent(typeof(Collider2D))]
    public class NavigatorNormalMode : MonoBehaviour
    {
        private bool isCollide = false;

        public bool IsCollide => isCollide;
        private void Awake()
        {
            isCollide = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if ((GameManager.Instance.layerBlockLine.value & (1 << other.transform.gameObject.layer)) != 0)
            {
                Debug.LogError("Block Line is true");
                isCollide = true;
            }
            else
            {
                GetComponent<Collider2D>().isTrigger = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            isCollide = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tag.blockline))
            {
                Debug.LogError("Block Line is true");
                isCollide = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            isCollide = false;
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
