using UnityEngine;

public class OldDriver : MonoBehaviour
{
    [Header("PLAYER")]
    public float speed = 10f;
    public float rotationSpeed = 100f;

    [Header("AI")]
    public float thresholdDistance = 2f;
    public float thresholdAngle = 10f;

    public float AISpeed = 100f;
    public float AIRotationSpeed = 1000f;

    [Header("Objects")]
    [SerializeField] GameObject fuel;
    [SerializeField] GameObject ammo;

    [SerializeField] bool autoFuel = false;
    [SerializeField] bool autoAmmo = false;

    void LateUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);

        transform.Rotate(0, rotation, 0);

        // -------------------------------------------------------AI Code-------------------------------------------------------

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pointing towards Fuel!");
            autoFuel = !autoFuel;
        }
        // Repeated
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Pointing towards Ammo!");
            autoAmmo = !autoAmmo;
        }
        // Repeated
        if (CalculateDistance(fuel) < thresholdDistance || CalculateDistance(ammo) < thresholdDistance)
        {
            autoFuel = false;
            autoAmmo = false;
        }

        if (autoFuel)
        {
            MoveTowardsGoal(fuel);
        }
        // Repeated
        if (autoAmmo)
        {
            MoveTowardsGoal(ammo);
        }
    }

    void MoveTowardsGoal(GameObject goal)
    {
        RotateTowardsGoal(goal);
        this.transform.position += this.transform.forward * AISpeed * Time.deltaTime;
    }

    float CalculateDistance(GameObject goal)
    {
        Vector3 tankPos = this.transform.position;
        Vector3 goalPos = goal.transform.position;

        float distance = Vector3.Distance(tankPos, goalPos);

        return distance;
    }

    void RotateTowardsGoal(GameObject goal)
    {
        Vector3 tankForward = this.transform.forward;
        Vector3 goalDirection = goal.transform.position - this.transform.position;

        Debug.DrawRay(this.transform.position, tankForward * 10, Color.black, 10);
        Debug.DrawRay(this.transform.position, goalDirection , Color.red, 10);

        float angle = Mathf.Acos(dotProduct(tankForward, goalDirection) / (tankForward.magnitude * goalDirection.magnitude));

        // Debug.Log("Angle: " + angle);

        int factor = -1;
        if (crossProduct(tankForward, goalDirection).z < 0){
            factor = 1;
        }

        if((angle * Mathf.Rad2Deg) > thresholdAngle)
        {
            this.transform.Rotate(0, angle * Mathf.Rad2Deg * factor * AIRotationSpeed * Time.deltaTime, 0);
        }
    }

    float dotProduct(Vector3 a, Vector3 b)
    {
        // Dot product formula of a and b; a . b = |a| * |b| * Cos@
        // angle @ = Cos^(-1)(a.b / |a|*|b|)

        float prod;
        prod = (a.x * b.x) + (a.y * b.y) + (a.z * b.z);

        return prod;
    }

    Vector3 crossProduct(Vector3 a, Vector3 b)
    {
        float term1 = (a.x * b.y) - (b.x * a.y);
        float term2 = (a.y * b.z) - (b.y * a.z);
        float term3 = (a.x * b.z) - (b.x * a.z);

        return (new Vector3(term1, term2, term3));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        Gizmos.DrawWireSphere(fuel.transform.position, thresholdDistance);
        // Repeated
        Gizmos.DrawWireSphere(ammo.transform.position, thresholdDistance);
    }
}
