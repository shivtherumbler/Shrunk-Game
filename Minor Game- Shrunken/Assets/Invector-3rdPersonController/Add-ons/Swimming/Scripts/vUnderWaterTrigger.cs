using UnityEngine;
using System.Collections;
namespace Invector
{
    public class vUnderWaterTrigger : MonoBehaviour
    {

        public GameObject waterEffect;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Water"))
            {
                waterEffect.SetActive(true);
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Water"))
            {
                waterEffect.SetActive(false);
            }
        }
    }
}
