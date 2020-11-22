using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public GameObject[] sceneObjects;

    public GameObject lightrays;

    private Mesh mesh;
    public float offset = .01f;

    public struct angledVerts
    {
        public Vector3 vert;

        public float angle;

        public Vector2 uv;
    }


    void Start()
    {
        //sceneObjects = GameObject.FindGameObjectsWithTag("LightObstruction");
        mesh = lightrays.GetComponent<MeshFilter>().mesh;
    }

    public static int[] AddItemsToArray(int[] original, int itemToAdd1, int itemToAdd2, int itemToAdd3)
    {
        int[] finalArray = new int[original.Length + 3];

        for (int i = 0; i < original.Length; i++)
        {
            finalArray[i] = original[i];
        }

        finalArray[original.Length] = itemToAdd1;
        finalArray[original.Length + 1] = itemToAdd2;
        finalArray[original.Length + 2] = itemToAdd3;

        return finalArray;
    }


    public static Vector3[] ConcatArrays(Vector3[] first, Vector3[] second)
    {
        Vector3[] concatted = new Vector3[first.Length + second.Length];

        System.Array.Copy(first, concatted, first.Length);
        System.Array.Copy(second, 0, concatted, first.Length, second.Length);

        return concatted;
    }


    void Update()
    {
        mesh.Clear();

        Vector3[] objectVerts = sceneObjects[0].GetComponent<MeshFilter>().mesh.vertices;
        for (int k = 1; k < sceneObjects.Length; k++)
        {
            objectVerts = ConcatArrays(objectVerts, sceneObjects[k].GetComponent<MeshFilter>().mesh.vertices);
        }

        angledVerts[] angledVerts = new angledVerts[(objectVerts.Length * 2)];
        Vector3[] verts = new Vector3[(objectVerts.Length * 2) + 1]; ;
        Vector2[] uvs = new Vector2[(objectVerts.Length * 2) + 1]; ;

        verts[0] = lightrays.transform.localToWorldMatrix.MultiplyPoint3x4(transform.position);
        uvs[0] = new Vector2(verts[0].x, verts[0].y);

        int loopCount = 0;
        

        Vector3 myLocation = this.transform.position;

        for (int i = 0; i< sceneObjects.Length; i++)
        {
            Vector3[] mesh = sceneObjects[i].GetComponent<MeshFilter>().mesh.vertices;

            for (int h = 0; h<mesh.Length; h++)
            {
                Vector3 vertLocation = sceneObjects[i].transform.localToWorldMatrix.MultiplyPoint3x4(mesh[h]);
                RaycastHit hit;
                RaycastHit hit2;

                float angle1 = Mathf.Atan2((vertLocation.x - myLocation.x - offset), (vertLocation.y - myLocation.y - offset));
                float angle2 = Mathf.Atan2((vertLocation.x - myLocation.x + offset), (vertLocation.y - myLocation.y + offset)); ;

                Physics.Raycast(myLocation, new Vector2(vertLocation.x - myLocation.x -offset, vertLocation.y - myLocation.y - offset), out hit, 100);
                Physics.Raycast(myLocation, new Vector2(vertLocation.x - myLocation.x + offset, vertLocation.y - myLocation.y + offset), out hit2, 100);
                //Physics.Raycast(myLocation, vertLocation - myLocation, out hit, 100);

                //print(hit.distance);

                Debug.DrawLine(myLocation, hit.point, Color.red);
                Debug.DrawLine(myLocation, hit2.point, Color.green);
                //Debug.DrawLine(myLocation, hit.point, Color.red);

                
                angledVerts[(loopCount * 2)].vert = lightrays.transform.localToWorldMatrix.MultiplyPoint3x4(hit.point);
                angledVerts[(loopCount * 2)].angle = angle1;
                angledVerts[(loopCount * 2)].uv = new Vector2(angledVerts[(loopCount*2)].vert.x, angledVerts[(loopCount * 2)].vert.y);

                angledVerts[(loopCount * 2) + 1].vert = lightrays.transform.localToWorldMatrix.MultiplyPoint3x4(hit2.point);
                angledVerts[(loopCount * 2) + 1].angle = angle2;
                angledVerts[(loopCount * 2) + 1].uv = new Vector2(angledVerts[(loopCount * 2) + 1].vert.x, angledVerts[(loopCount * 2) + 1].vert.y);

                loopCount++;
                
            }
        }
        
        
        System.Array.Sort(angledVerts, delegate(angledVerts one, angledVerts two){
            return one.angle.CompareTo(two.angle);
        });

        for (int n = 0; n< angledVerts.Length; n++)
        {
            verts[n + 1] = angledVerts[n].vert;
            uvs[n + 1] = angledVerts[n].uv;
        }

        mesh.vertices = verts;

        for (int n = 0; n < uvs.Length; n++)
        {
            uvs[n] = new Vector2(uvs[n].x + .5f, uvs[n].y + .5f);
        }

        mesh.uv = uvs;

        int[] triangles = {0, 1, verts.Length-1 };


        //for (int n = verts.Length-1; n > 0; n--)
        //{
            //triangles = AddItemsToArray(triangles, 0, n, n-1);
        //}

        for (int n = 0; n < verts.Length - 1; n++)
        {
            triangles = AddItemsToArray(triangles, 0, n, n + 1);
        }

        mesh.triangles = triangles;
        lightrays.transform.position = this.transform.position;
    }
}
