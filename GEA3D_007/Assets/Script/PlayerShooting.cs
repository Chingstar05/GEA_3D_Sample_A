using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // �߻�ü ������ �迭 (���� ������)
    public Transform firePoint;

    private Camera cam;
    private int currentWeaponIndex = 0;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        WeaponSwap(); // Z Ű�� ���� ��ü
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        // ���� ���⿡ �ش��ϴ� �߻�ü ������ ���
        Instantiate(projectilePrefabs[currentWeaponIndex],
                    firePoint.position,
                    Quaternion.LookRotation(direction));
    }

    void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // ���� �߻�ü ���������� ��ü (��ȯ)
            currentWeaponIndex = (currentWeaponIndex + 1) % projectilePrefabs.Length;
            Debug.Log("���� ���� �ε���: " + currentWeaponIndex);
        }
    }
}

