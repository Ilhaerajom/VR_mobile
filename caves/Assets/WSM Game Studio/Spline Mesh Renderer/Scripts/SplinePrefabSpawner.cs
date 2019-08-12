using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSMGameStudio.Splines
{
    [ExecuteInEditMode]
    public class SplinePrefabSpawner : MonoBehaviour
    {
        [Tooltip("Reference spline")]
        public Spline spline;
        [Tooltip("Number of instances to be spawned")]
        public int instances = 1;
        [Tooltip("Prefabs to be spawned along spline")]
        public GameObject[] prefabs;
        [Tooltip("(Optional) Spawn position offset relative to spline")]
        public Vector3 spawnOffset = Vector3.zero;

        private int _instanceID;
        private List<GameObject> _toDestroy;

        void OnEnable()
        {
            if (spline == null)
                spline = GetComponent<Spline>();

            _instanceID = GetInstanceID();
        }

        /// <summary>
        /// Spawn prefabs along spline
        /// </summary>
        public void SpawnPrefabs()
        {
            if (spline == null)
            {
                Debug.Log("Please select a reference spline to spawn prefabs.");
                return;
            }

            ResetObjects();

            instances = Mathf.Abs(instances);

            if (instances <= 0 || prefabs == null || prefabs.Length == 0)
                return;

            float stepSize = instances * prefabs.Length;
            float t;
            // if loop does not spawn a double at the end
            stepSize = (spline.Loop || stepSize == 1) ? (1f / stepSize) : (1f / (stepSize - 1));

            GameObject newClone;
            Vector3 clonePosition;
            Quaternion cloneRotation;
            Vector3 cloneDirection;

            for (int positionIndex = 0, instanceIndex = 0; instanceIndex < instances; instanceIndex++)
            {
                for (int prefabIndex = 0; prefabIndex < prefabs.Length; prefabIndex++, positionIndex++)
                {
                    newClone = Instantiate(prefabs[prefabIndex]);
                    t = positionIndex * stepSize;

                    if (spline.FollowTerrain)
                    {
                        ValidateOrientedPoints();

                        int index = spline.GetClosestOrientedPointIndex(t);
                        clonePosition = spline.OrientedPoints[index].Position;
                        cloneRotation = spline.OrientedPoints[index].Rotation;

                        int nextIndex = index + 1;

                        if (nextIndex > spline.OrientedPoints.Length - 1)
                        {
                            if (spline.Loop)
                                nextIndex = 0;
                            else
                            {
                                nextIndex = index;
                                index--;
                            }
                        }

                        cloneDirection = (spline.OrientedPoints[nextIndex].Position - spline.OrientedPoints[index].Position).normalized;
                    }
                    else
                    {
                        clonePosition = spline.GetPoint(t) + spawnOffset;
                        cloneRotation = spline.GetRotation(t);
                        cloneDirection = spline.GetDirection(t);
                    }

                    //cloneDirection = spline.GetDirection(t);

                    newClone.transform.localPosition = clonePosition;

                    newClone.transform.rotation = cloneRotation;
                    newClone.transform.LookAt(clonePosition + cloneDirection, newClone.transform.up);

                    newClone.transform.parent = transform;
                }
            }
        }

        /// <summary>
        /// Reset all objects
        /// </summary>
        public void ResetObjects()
        {
            _toDestroy = new List<GameObject>();

            //Get children to delete
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetInstanceID() == _instanceID)
                    continue;

                _toDestroy.Add(child.gameObject);
            }

            //Delete objects
            for (int i = (_toDestroy.Count - 1); i >= 0; i--)
            {
                _toDestroy[i].SetActive(false);
                DestroyImmediate(_toDestroy[i].gameObject);
            }

            _toDestroy.Clear();
        }

        /// <summary>
        /// Make sure Oriented Points were calculated
        /// </summary>
        private void ValidateOrientedPoints()
        {
            if (spline.OrientedPoints == null || (spline.OrientedPoints.Length == 0 || spline.GetComponent<SplineMeshRenderer>() == null))
                spline.CalculateOrientedPoints(1f);
        }
    }
}
