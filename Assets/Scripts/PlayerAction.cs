using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 10f;
    [SerializeField]
    private SpringJoint joint;

    private InputAction interactAction;
    private LayerMask layerMask;
    private Item item = null;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        interactAction = InputSystem.actions.FindAction("Attack");
        layerMask = LayerMask.GetMask("Item");
    }

    void Update()
    {
        if (interactAction.WasPerformedThisFrame())
        {
            if (TryCastItem(out item))
            {
                JointItem(item);
            }
        }

        if (interactAction.WasCompletedThisFrame())
        {
            ReleaseItem();
        }
    }

    private bool TryCastItem(out Item item)
    {
        item = null;
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out var hitInfo, maxDistance, layerMask))
        {
            item = hitInfo.collider.attachedRigidbody.GetComponent<Item>();

            return item != null;
        }

        return false;
    }

    private void JointItem(Item item)
    {
        var rig = item.GetComponent<Rigidbody>();
        joint.connectedBody = rig;
    }

    private void ReleaseItem()
    {
        joint.connectedBody = null;
    }
}
