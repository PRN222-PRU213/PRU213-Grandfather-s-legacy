using UnityEngine;

public class ShipDocking : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform ship;
    [SerializeField] private Transform dockPoint; // Vị trí neo đậu
    
    [Header("Settings")]
    [SerializeField] private float dockingSpeed = 2f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float stopDistance = 0.5f;
    
    private bool isDocking = false;
    
    void Update()
    {
        if (isDocking)
        {
            DockShip();
        }
    }
    
    public void StartDocking()
    {
        isDocking = true;
    }
    
    private void DockShip()
    {
        // Di chuyển tàu đến dock
        float distance = Vector3.Distance(ship.position, dockPoint.position);
        
        if (distance > stopDistance)
        {
            // Lerp vị trí
            ship.position = Vector3.Lerp(
                ship.position, 
                dockPoint.position, 
                dockingSpeed * Time.deltaTime
            );
            
            // Lerp xoay
            ship.rotation = Quaternion.Lerp(
                ship.rotation, 
                dockPoint.rotation, 
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            // Đã đến - snap vào vị trí chính xác
            ship.position = dockPoint.position;
            ship.rotation = dockPoint.rotation;
            isDocking = false;
            
            // Trigger event hoàn thành
            OnDockingComplete();
        }
    }
    
    private void OnDockingComplete()
    {
        Debug.Log("Tàu đã cập bến!");
        // Mở UI shop, inventory, etc.
    }
}