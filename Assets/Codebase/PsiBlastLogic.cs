using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PsiBlastLogic : MonoBehaviour
{
    [SerializeField] float psiForce = 10f;
    [SerializeField] float psiRadius = 10f;
    private InputManager inputManager;

    private Vector3 intersectionPoint;

    public Transform BlastVFX;



    void Start()
    {
        BlastVFX.SetParent(null);
        BlastVFX.gameObject.GetComponent<MeshRenderer>().enabled = false;
        inputManager = InputManager.instance;
    }

    public void CreatePsiBlast()
    {
        Collider[] colliders = Physics.OverlapSphere(intersectionPoint, psiRadius);
        PsiBlastVFX();
        if (colliders.Length > 0)
        {
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(psiForce, intersectionPoint, psiRadius, 0f, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, psiRadius);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputManager.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            intersectionPoint = hit.point;
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
        }
    }

    private void PsiBlastVFX()
    {
        BlastVFX.localScale = Vector3.one;
        BlastVFX.position = intersectionPoint;
        BlastVFX.gameObject.GetComponent<MeshRenderer>().enabled = true;
        BlastVFX.DOScale(psiRadius, 0.1f).OnComplete(VFXComplete);
    }

    private void VFXComplete()
    {
        BlastVFX.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
