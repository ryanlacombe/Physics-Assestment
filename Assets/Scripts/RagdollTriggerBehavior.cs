using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTriggerBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RagdollBehavior ragdoll = other.GetComponentInParent<RagdollBehavior>();
        if (ragdoll != null)
            ragdoll.ragdollEnabled = true;
    }
}
