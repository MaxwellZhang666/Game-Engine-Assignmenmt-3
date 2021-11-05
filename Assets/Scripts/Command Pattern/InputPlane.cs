using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPlane : MonoBehaviour
{
    Camera maincam;
    RaycastHit hitInfo;
    public Transform cubePrefab;
    public Transform cylinderPrefab;
    public Transform spherePrefab;
    public Transform objectPrefab;

    public Button cubeButton;
    public Button cynlinderButton;
    public Button sphereButton;
    public Button largeButton;
    public Button smallButton;

    public abstract class Objects
    {
        public abstract void Process();
    }

    public class CubeObject : Objects
    {
        public override void Process()
        {
            Debug.Log("Cube");
        }
    }

    public class CylinderObject : Objects
    {
        public override void Process()
        {
            Debug.Log("Cynlinder");
        }
    }

    public class SphereObject : Objects
    {
        public override void Process()
        {
            Debug.Log("Sphere");
        }
    }

    public class GameObjectFactory
    {
        public Objects GetObjects(string objectType)
        {
            switch (objectType)
            {
                case "Cube":
                    return new CubeObject();
                // Change prefab into Cube in InputPlane.cs;
                case "Cylinder":
                    return new CylinderObject();
                // Change prefab into Cynlinder in InputPlane.cs;
                case "Sphere":
                    return new SphereObject();
                // Change prefab into Sphere in InputPlane.cs;
                default:
                    return null;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        maincam = Camera.main;
        objectPrefab.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void SpawnCube()
    {
        new GameObjectFactory().GetObjects("Cube");
        objectPrefab = cubePrefab;
    }

    public void SpawnCynlinder()
    {
        new GameObjectFactory().GetObjects("Cynlinder");
        objectPrefab = cylinderPrefab;
    }

    public void SpawnSphere()
    {
        new GameObjectFactory().GetObjects("Sphere");
        objectPrefab = spherePrefab;
    }

    public void ScaleSmaller()
    {
        objectPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void ScaleBigger()
    {
        objectPrefab.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = maincam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                Color c = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                //CubePlacer.PlaceCube(hitInfo.point, c, cubePrefab);

                ICommand command = new PlaceCubeCommand(hitInfo.point, c, objectPrefab);
                CommandInvoker.AddCopmmand(command);
            }
        }
    }

}
