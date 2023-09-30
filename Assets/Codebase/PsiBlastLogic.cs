using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PsiBlastLogic : MonoBehaviour
{
    [SerializeField] float psiForce = 10f;
    [SerializeField] float psiRadius = 10f;
    private Vector3 psiPosition;
    private InputManager inputManager;
    public Transform[] objects;
    private Vector3[] startPosition;
    private Vector3 intersectionPoint;

    public Transform cursor;


    void Start()
    {   
        psiPosition = transform.position;
        startPosition = new Vector3[objects.Length];


        for (int i = 0; i < objects.Length; i++)
        {
            startPosition[i] = objects[i].position;
            Debug.Log(startPosition[i]);
        }

        inputManager = InputManager.instance;
    }

    public void CreatePsiBlast()
    {



        Collider[] colliders = Physics.OverlapSphere(intersectionPoint, psiRadius);
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
            cursor.position = intersectionPoint;
        }
    }
}
