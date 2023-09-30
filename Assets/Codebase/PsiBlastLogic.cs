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

    // Start is called before the first frame update
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

    public void CreatePsiWave()
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
        Debug.Log("Wave is Called");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, psiRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, psiRadius / 2);
    }

    //public void Reset()
    //{
    //    for (int i = 0; i < objects.Length; i++)
    //    {
    //        Rigidbody rb = objects[i].GetComponent<Rigidbody>();
    //        rb.Sleep();
    //        objects[i].position = startPosition[i];
    //    }
    //}

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
}
