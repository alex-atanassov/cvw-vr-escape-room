using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerSetup : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private XRBaseInteractable handle = null;
    [SerializeField] private PhysicsMover mover = null;

    [Header("Direction")]
    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    private Vector3 grabPosition = Vector3.zero;
    private float startingPercentage = 0.0f;
    private float currentPercentage = 0.0f;

    public AudioSource audio;
    private bool isMoving = false;

    protected virtual void OnEnable()
    {
        handle.selectEntered.AddListener(StoreGrabInfo);
    }

    protected virtual void OnDisable()
    {
        handle.selectEntered.RemoveListener(StoreGrabInfo);
    }

    private void StoreGrabInfo(SelectEnterEventArgs args)
    {
        startingPercentage = currentPercentage;

        grabPosition = args.interactor.transform.position;
    }

    private void Update()
    {
        if (handle.isSelected)
        {
            if (!isMoving)
            {
                isMoving = true;
                audio.Play();
            }
            UpdateDrawer();
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                audio.Stop();
            }
        }
        //if (handle.isSelected)
        //{
        //    UpdateDrawer();
        //}
    }

    private void UpdateDrawer()
    {
        float newPercentage = startingPercentage + FindPercentageDifference();

        mover.MoveTo(Vector3.Lerp(start.position, end.position, newPercentage));

        currentPercentage = Mathf.Clamp01(newPercentage);
    }

    private float FindPercentageDifference()
    {
        Vector3 handPosition = handle.selectingInteractor.transform.position;
        Vector3 pullDirection = handPosition - grabPosition;
        Vector3 targetDirection = end.position - start.position;

        float length = targetDirection.magnitude;
        targetDirection.Normalize();

        return Vector3.Dot(pullDirection, targetDirection) / length;
    }

    private void OnDrawGizmos()
    {
        if(start && end)
        {
            Gizmos.DrawLine(start.position, end.position);
        }
    }
}
