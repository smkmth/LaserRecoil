using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float dampTime = 0.2f;                 
    public float screenEdgeBuffer = 4f;           
    public float minSize = 6.5f;


    public float minZPos = -86.0f;
    public float maxZPos = -40.0f; 
    public float minXPos = -30.0f;
    public float maxXPos = 30.0f; 
    public float minYPos = 5.0f;
    public float maxYPos = 20.0f;


    public List<Transform> Targets;
    public int playerIndex;
    public int maxNumberOfTargets;

    public int repriTimer;
    public int repriRate;
    private Camera playerCamera;                        
    public float zoomSpeed;                      
    public Vector3 moveVelocity;                 
    public Vector3 desiredPosition;
    public Vector3 localDesiredPos;
    public List<Renderer> visibleRenderers = new List<Renderer>();


    public AimGun aimGun;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        aimGun = GetComponent<AimGun>();
    }

   

    private void Update()
    {
        //ScanTargets();

        // Move the camera towards a desired position.
        Move();

        // Change the size of the camera based.
        Zoom();


    }


    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();

        // Smoothly transition to that position.
        playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, desiredPosition, ref moveVelocity, dampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;
       
        // Go through all the targets and add their positions together.
        for (int i = 0; i < Targets.Count; i++)
        {


            // If the target isn't active, go on to the next one.
            if (!Targets[i].gameObject.activeSelf || Targets[i] == null)
            {
                RemoveTarget(Targets[i]);
                continue;
            }

            // Add to the average and increment the number of targets in the average.
            averagePos += Targets[i].position;
            numTargets++;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (numTargets > 0)
            averagePos /= numTargets;

        // Keep the same y value.
        averagePos.z = playerCamera.transform.position.z;
        localDesiredPos = playerCamera.transform.InverseTransformPoint(desiredPosition);
        desiredPosition = averagePos;

        if (localDesiredPos.x >= maxXPos)
        {
            localDesiredPos.x = maxXPos;
        }
        if (localDesiredPos.x <= minXPos)
        {
            localDesiredPos.x = maxXPos;
        }

        if (localDesiredPos.y >= maxYPos)
        {
            //Debug.Log("local");
            localDesiredPos.y = maxYPos;
        }
        if (localDesiredPos.y <= minYPos)
        {
            localDesiredPos.y = minYPos;
        }

        if (localDesiredPos.z >= maxZPos)
        {
            localDesiredPos.z = maxZPos;
        }
        if (localDesiredPos.z <= maxZPos)
        {
            localDesiredPos.z = maxZPos;
        }
        averagePos = playerCamera.transform.TransformPoint(localDesiredPos);
        // The desired position is the average position;
    }


    private void Zoom()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        float requiredSize = FindRequiredSize();
        playerCamera.orthographicSize = Mathf.SmoothDamp(playerCamera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
      
    }

    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = playerCamera.transform.InverseTransformPoint(desiredPosition);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < Targets.Count; i++)
        {
            //// ... and if they aren't active continue on to the next target.
            //if (!Targets[i].gameObject.activeSelf)
            //    continue;

            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = playerCamera.transform.InverseTransformPoint(Targets[i].position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            

            // Choose the largest out of the current size and the distance of
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size 
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / playerCamera.aspect);

            
        }

        // Add the edge buffer to the size.
        size += screenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, minSize);

        return size;
    }

    public void AddTarget(Transform targetTransform)
    {
        if (Targets.Count <= maxNumberOfTargets)
        {
            if (!Targets.Contains(targetTransform))
            {
                Targets.Add(targetTransform);
            }
        } else
        {
            if (repriTimer >= repriRate)
            {
                repriTimer = 0;
                PrioritiseTargets(targetTransform);
            } else
            {
                repriTimer++;
            }
        }

    }

    public void RemoveTarget(Transform targetTransform)
    {
        if(targetTransform.gameObject.tag != "Player") { 
            if (Targets.Contains(targetTransform))
            {
                Targets.Remove(targetTransform);
            }
        }

    }

    private void PrioritiseTargets(Transform potentialTarget)
    {

        foreach (Transform target in Targets)
        {
            if (playerCamera.ViewportToScreenPoint(target.position).x > playerCamera.ViewportToScreenPoint(potentialTarget.transform.position).x ||
                playerCamera.ViewportToScreenPoint(target.position).y > playerCamera.ViewportToScreenPoint(potentialTarget.transform.position).y)
            {
                RemoveTarget(target);
                AddTarget(potentialTarget);
                return;

            }
        }


        
    }

    private void ScanTargets()
    {
        // Retrieve all renderers in scene
        Renderer[] sceneRenderers = FindObjectsOfType<Renderer>();

        // Store only visible renderers
        visibleRenderers.Clear();
        for (int i = 0; i < sceneRenderers.Length; i++)
            if (IsVisible(sceneRenderers[i]))
                visibleRenderers.Add(sceneRenderers[i]);

        // debug console
        string result = "Total Renderers = " + sceneRenderers.Length + ".  Visible Renderers = " + visibleRenderers.Count;
        foreach (Renderer renderer in visibleRenderers)
            result += "\n " + renderer.name;

        Debug.Log(result);
    }

    bool IsVisible(Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return (GeometryUtility.TestPlanesAABB(planes, renderer.bounds)) ? true : false;
    }

}

