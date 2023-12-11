using UnityEngine;

public class LootDropScript : MonoBehaviour
{
    public GameObject robotPart;
    public GameObject tpItem;
    public GameObject hpItem;

    private float tpDropRate = 0.1f;
    public float TpDropRate { get { return tpDropRate; } set { tpDropRate = value; } }

    private float hpDropRate = 0.1f;
    public float HpDropRate { get { return hpDropRate; } set { hpDropRate = value; } }


    private void DropRobotPart()
    {
        if (gameObject.name == "RobotSpider(Clone)")
        {
            Instantiate(robotPart, RandomDropLocation(), transform.rotation);
        }

        if (gameObject.name == "RobotGiant(Clone)")
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(robotPart, RandomDropLocation(), transform.rotation);
            }
        }
    }

    private void DropItem(GameObject item, float dropRate)
    {
        if (Random.Range(0f, 1f) <= dropRate)
        {
            Instantiate(item, RandomDropLocation(), transform.rotation);
        }
    }
    public void DropLoot()
    {
        DropRobotPart();
        DropItem(tpItem, tpDropRate);
        DropItem(hpItem, hpDropRate);
    }

    private Vector3 RandomDropLocation()
    {
        Vector3 randomCoords = new(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        Vector3 dropLocation = transform.position + randomCoords;
        return dropLocation;

    }

}
