using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementClick : MonoBehaviour
{
    public GameObject selectedObject;
    // Start is called before the first frame update

    public GameObject[] combinations;
    Vector3 mouseOffset;

    Collider2D coll;

    void Start()
    {
        coll = gameObject.GetComponent<Collider2D>();
    }
    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mouse);
            if (targetObject && targetObject.tag != "platform")
            {
                selectedObject = targetObject.transform.gameObject;
                mouseOffset = selectedObject.transform.position - mouse;
            }
        }

        if (selectedObject)
        {
            selectedObject.transform.position = mouse + mouseOffset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            var results = new List<Collider2D>();
            int numColliders = coll.OverlapCollider(new ContactFilter2D().NoFilter(), results);
            Debug.Log(numColliders);
            if (numColliders > 0)
            {
                Debug.Log("There are colliders!");
                foreach (Collider2D collider in results)
                {
                    GameObject element2 = collider.gameObject;
                    bool success = DoCombination(gameObject, element2);
                }
            }
            selectedObject = null;
        }

       float maxDistance = 0.8f;
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, maxDistance, LayerMask.GetMask("Platform"));
        if (hit.collider != null)
        {
            Debug.Log("Here");
            PlatformBehavior platformBehavior = hit.collider.gameObject.GetComponent<PlatformBehavior>();
            if (platformBehavior != null)
            {
                platformBehavior.ApplyElementEffect(gameObject.name);
                Destroy(gameObject);
            }
        }
    }

    bool DoCombination(GameObject element1, GameObject element2)
    {
        string[] elements = { "fire", "water", "wind", "earth" };
        bool element1found = false;
        bool element2found = false;
        foreach (string element in elements)
        {
            if (element.Equals(element1.name))
            {
                element1found = true;
                Debug.Log("Element1 found!");
            }
            if (!element.Equals(element2.name))
            {
                element2found = true;
                Debug.Log("Element2 found!");
            }
        }
        if (element1found && element2found)
        {
            string combination = CheckCombination(element1.name, element2.name);
            foreach (GameObject combo in combinations)
            {
                if (combo.name == combination)
                {
                    Instantiate(combo, transform.position, transform.rotation);
                    Destroy(element1);
                    Destroy(element2);
                    return true;
                }
            }
        }
        return false;
    }

    string CheckCombination(string name1, string name2)
    {
        var fireCombos = new Dictionary<string, string>{
            {"wind", "lightning"},
            {"earth", "lava"},
            {"water", "steam"}
        };
        var waterCombos = new Dictionary<string, string>{
            {"wind", "cloud"},
            {"earth", "mud"},
            {"fire", "steam"}
        };
        var earthCombos = new Dictionary<string, string>{
            {"wind", "mirror"},
            {"fire", "lava"},
            {"water", "mud"}
        };
        var windCombos = new Dictionary<string, string>{
            {"fire", "lightning"},
            {"earth", "mirror"},
            {"water", "cloud"}
        };
        if (name1 == "fire")
        {
            return fireCombos[name2];
        }
        if (name1 == "water")
        {
            return waterCombos[name2];
        }
        if (name1 == "earth")
        {
            return earthCombos[name2];
        }
        if (name1 == "wind")
        {
            return windCombos[name2];
        }
        return null;
    }
}
