using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // 발사체 프리팹 배열 (무기 종류별)
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

        WeaponSwap(); // Z 키로 무기 교체
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        // 현재 무기에 해당하는 발사체 프리팹 사용
        Instantiate(projectilePrefabs[currentWeaponIndex],
                    firePoint.position,
                    Quaternion.LookRotation(direction));
    }

    void WeaponSwap()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 다음 발사체 프리팹으로 교체 (순환)
            currentWeaponIndex = (currentWeaponIndex + 1) % projectilePrefabs.Length;
            Debug.Log("현재 무기 인덱스: " + currentWeaponIndex);
        }
    }
}

