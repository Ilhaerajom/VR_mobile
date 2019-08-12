using System.Collections.Generic;
using UnityEngine;

namespace WSMGameStudio.Splines
{
    public class SplineFollower : MonoBehaviour
    {
        [Tooltip("One or more splines to be followed")]
        public List<Spline> splines;
        [Tooltip("Following speed")]
        public float speed;
        [Tooltip("Following behaviour")]
        public SplineFollowerBehaviour followerBehaviour;
        [Tooltip("Follower reference path")]
        public SplineFollowerReference followerReference;
        [Tooltip("Customize start position along the spline (From 0% to 100%)")]
        [Range(0, 100)]
        public float customStartPosition = 0f;
        [Tooltip("Apply spline rotation on object")]
        public bool applySplineRotation = true;
        [Tooltip("Configure stops")]
        public SplineFollowerStops cycleEndStops;
        [Tooltip("Configure stop time")]
        public float cycleStopTime = 0f;

        private float _currentStopTime = 0f;

        private float _cicleDuration;
        private float _progress;
        private bool _goingForward = true;
        private float _distance;
        private int _currentSpline = 0;

        private Vector3 _currentPosition = Vector3.zero;
        private Quaternion _currentRotation = Quaternion.identity;
        private Vector3 _lookDirection = Vector3.zero;
        private Vector3 _lastLookDirection = Vector3.zero;
        private Vector3 _lastCustomUpDirection = Vector3.up;

        private Vector3 _startPosition = Vector3.zero;
        private Vector3 _endPosition = Vector3.zero;
        private Quaternion _startRotation = Quaternion.identity;
        private Quaternion _endRotation = Quaternion.identity;
        private int _currentIndex = -1;
        private float _segmentdistance;
        private float _segmentTime;
        private float _step;

        private void Start()
        {
            if (splines == null || splines.Count == 0)
                return;

            _currentIndex = -1;
            _currentSpline = 0;
            _distance = splines[_currentSpline].Length;
            _progress = (customStartPosition * 0.01f);

            cycleStopTime = Mathf.Abs(cycleStopTime);

            _lastLookDirection = transform.forward;
            _lastCustomUpDirection = splines[_currentSpline].CustomUpwardsDirection;
        }

        private void Update()
        {
            if (cycleEndStops != SplineFollowerStops.Disabled && _currentStopTime > 0f)
            {
                _currentStopTime -= Time.deltaTime;
                return;
            }

            _goingForward = (speed >= 0);

            if (followerReference == SplineFollowerReference.Spline)
                FollowSpline();
            else if (followerReference == SplineFollowerReference.Terrain)
                FollowTerrain();
        }

        /// <summary>
        /// Follow spline path
        /// </summary>
        private void FollowSpline()
        {
            if (splines == null || splines.Count == 0)
                return;

            _cicleDuration = speed == 0f ? 0f : _distance / Mathf.Abs(speed);

            if (_goingForward)
            {
                if (_cicleDuration > 0f)
                    _progress += (Time.deltaTime / _cicleDuration);

                if (_progress > 1f)
                {
                    if (_currentSpline < splines.Count - 1)
                    {
                        _currentSpline++;
                        _progress = 0;
                        RecalculateCicleDuration();

                        if (cycleEndStops == SplineFollowerStops.EachSpline)
                            _currentStopTime = cycleStopTime;
                    }
                    else //Reached end of spline list
                    {
                        switch (followerBehaviour)
                        {
                            case SplineFollowerBehaviour.StopAtTheEnd:
                                _progress = 1f;
                                break;
                            case SplineFollowerBehaviour.Loop:
                                _currentSpline = 0;
                                _progress -= 1f;
                                RecalculateCicleDuration();
                                break;
                            case SplineFollowerBehaviour.BackAndForward:
                                _progress = 1f;
                                speed *= -1;
                                break;
                        }

                        if (cycleEndStops == SplineFollowerStops.LastSpline || cycleEndStops == SplineFollowerStops.EachSpline)
                            _currentStopTime = cycleStopTime;
                    }
                }
            }
            else
            {
                if (_cicleDuration > 0f)
                    _progress -= (Time.deltaTime / _cicleDuration);

                if (_progress < 0f)
                {
                    if (_currentSpline > 0)
                    {
                        _currentSpline--;
                        _progress = 1;
                        RecalculateCicleDuration();

                        if (cycleEndStops == SplineFollowerStops.EachSpline)
                            _currentStopTime = cycleStopTime;
                    }
                    else //Reached end of spline list
                    {
                        switch (followerBehaviour)
                        {
                            case SplineFollowerBehaviour.StopAtTheEnd:
                                _progress = 0f;
                                break;
                            case SplineFollowerBehaviour.Loop:
                                _currentSpline = splines.Count - 1;
                                _progress += 1f;
                                RecalculateCicleDuration();
                                break;
                            case SplineFollowerBehaviour.BackAndForward:
                                _progress = 0;
                                speed *= -1;
                                break;
                        }

                        if (cycleEndStops == SplineFollowerStops.LastSpline || cycleEndStops == SplineFollowerStops.EachSpline)
                            _currentStopTime = cycleStopTime;
                    }
                }
            }

            OrientedPoint orientedPoint = splines[_currentSpline].GetOrientedPoint(_progress);
            _currentPosition = orientedPoint.Position;
            _currentRotation = orientedPoint.Rotation;
            _lookDirection = _currentPosition + splines[_currentSpline].GetDirection(_progress);

            transform.position = Vector3.Lerp(transform.position, _currentPosition, 1f);
            if (applySplineRotation)
                transform.rotation = _currentRotation;
        }

        /// <summary>
        /// Recalculate cicle durantion to keep consistent speed
        /// </summary>
        private void RecalculateCicleDuration()
        {
            _distance = splines[_currentSpline].Length;
            _cicleDuration = speed == 0f ? 0f : _distance / Mathf.Abs(speed);
        }

        /// <summary>
        /// Follow spline terrain projection
        /// </summary>
        private void FollowTerrain()
        {
            if (splines == null)
                return;

            //First time or end of segment
            if (_currentIndex == -1 || (_progress >= 1))
            {
                ValidateOrientedPoints();

                _progress = (_progress >= 1) ? 0f : customStartPosition * 0.01f;
                _currentIndex = (_currentIndex == -1) ? splines[_currentSpline].GetClosestOrientedPointIndex(_progress) : _currentIndex;

                if (_goingForward)
                {
                    _currentIndex++;

                    if (_currentIndex > splines[_currentSpline].OrientedPoints.Length - 2)
                    {
                        if (_currentSpline < splines.Count - 1)
                        {
                            _progress = 0f;
                            _currentSpline++;
                            _currentIndex = 0;
                            CalculateSegmentPositionsAndDirection();

                            if (cycleEndStops == SplineFollowerStops.EachSpline)
                                _currentStopTime = cycleStopTime;
                        }
                        else // Reached end of spline list
                        {
                            switch (followerBehaviour)
                            {
                                case SplineFollowerBehaviour.StopAtTheEnd:
                                    _progress = 1f;
                                    _currentIndex = splines[_currentSpline].OrientedPoints.Length - 2;
                                    CalculateSegmentPositionsAndDirection();
                                    break;
                                case SplineFollowerBehaviour.Loop:
                                    _currentSpline = 0;
                                    _currentIndex = 0;
                                    CalculateSegmentPositionsAndDirection();
                                    break;
                                case SplineFollowerBehaviour.BackAndForward:
                                    _currentIndex--;
                                    speed *= -1;
                                    CalculateSegmentPositionsAndDirection(true);
                                    break;
                            }

                            if (cycleEndStops == SplineFollowerStops.LastSpline || cycleEndStops == SplineFollowerStops.EachSpline)
                                _currentStopTime = cycleStopTime;
                        }
                    }
                    else
                        CalculateSegmentPositionsAndDirection();
                }
                else
                {
                    _currentIndex--;

                    if (_currentIndex < 1)
                    {
                        if (_currentSpline > 0)
                        {
                            _progress = 1f;
                            _currentSpline--;
                            _currentIndex = (splines[_currentSpline].OrientedPoints.Length - 1);
                            CalculateSegmentPositionsAndDirection(true);

                            if (cycleEndStops == SplineFollowerStops.EachSpline)
                                _currentStopTime = cycleStopTime;
                        }
                        else // Reached end of spline list
                        {
                            switch (followerBehaviour)
                            {
                                case SplineFollowerBehaviour.StopAtTheEnd:
                                    _progress = 1f;
                                    _currentIndex = 1;
                                    CalculateSegmentPositionsAndDirection(true);
                                    break;
                                case SplineFollowerBehaviour.Loop:
                                    _currentSpline = splines.Count - 1;
                                    _currentIndex = splines[_currentSpline].OrientedPoints.Length - 1;
                                    CalculateSegmentPositionsAndDirection(true);
                                    break;
                                case SplineFollowerBehaviour.BackAndForward:
                                    _currentIndex++;
                                    speed *= -1;
                                    CalculateSegmentPositionsAndDirection();
                                    break;
                            }

                            if (cycleEndStops == SplineFollowerStops.LastSpline || cycleEndStops == SplineFollowerStops.EachSpline)
                                _currentStopTime = cycleStopTime;
                        }
                    }
                    else
                        CalculateSegmentPositionsAndDirection(true);
                }

                _currentPosition = _startPosition;

                _segmentdistance = Mathf.Abs(Vector3.Distance(_endPosition, _startPosition));
                _segmentTime = speed == 0f ? 0f : (_segmentdistance / Mathf.Abs(speed));
                _step = _segmentTime == 0 ? 0 : (Time.deltaTime / _segmentTime);
            }

            _progress += _step;

            _currentPosition = Vector3.Lerp(_startPosition, _endPosition, Mathf.Clamp01(_progress));
            _currentRotation = Quaternion.Lerp(_startRotation, _endRotation, Mathf.Clamp01(_progress));

            transform.position = _currentPosition;
            if (applySplineRotation)
                transform.rotation = _currentRotation;
        }

        /// <summary>
        /// Calculate current seggment position and direction for the follow terrain method
        /// </summary>
        /// <param name="reversed"></param>
        private void CalculateSegmentPositionsAndDirection(bool reversed = false)
        {
            int endIndex = reversed ? (_currentIndex - 1) : (_currentIndex + 1);
            ValidateOrientedPoints();

            _startPosition = splines[_currentSpline].OrientedPoints[_currentIndex].Position;
            _startRotation = splines[_currentSpline].OrientedPoints[_currentIndex].Rotation;

            _endPosition = splines[_currentSpline].OrientedPoints[endIndex].Position;
            _endRotation = splines[_currentSpline].OrientedPoints[endIndex].Rotation;

            _lastLookDirection = _lookDirection;
            _lookDirection = (_endPosition - _startPosition).normalized;
            _lookDirection = reversed ? -_lookDirection : _lookDirection;

            if (splines[_currentSpline].CustomUpwardsDirection != _lastCustomUpDirection)
            {
                _lastCustomUpDirection = splines[_currentSpline].CustomUpwardsDirection;
                _startRotation.SetLookRotation(_lookDirection, splines[_currentSpline].CustomUpwardsDirection);
            }
            else
                _startRotation.SetLookRotation(_lastLookDirection, splines[_currentSpline].CustomUpwardsDirection);

            _endRotation.SetLookRotation(_lookDirection, splines[_currentSpline].CustomUpwardsDirection);
        }

        /// <summary>
        /// Validates if oriented points were generated
        /// </summary>
        private void ValidateOrientedPoints()
        {
            if (splines[_currentSpline].OrientedPoints == null
                || (splines[_currentSpline].OrientedPoints.Length == 0
                || splines[_currentSpline].GetComponent<SplineMeshRenderer>() == null))
            {
                splines[_currentSpline].CalculateOrientedPoints(1f);
            }
        }

        /// <summary>
        /// Move object to start position
        /// </summary>
        public void MoveToStartPosition()
        {
            if (splines == null || splines.Count == 0)
                return;

            float t = customStartPosition * 0.01f;
            splines[0].CalculateOrientedPoints(1f);

            int index = (int)(splines[0].OrientedPoints.Length * t);
            OrientedPoint start = splines[0].OrientedPoints[index];

            transform.position = start.Position;
            if (applySplineRotation)
                transform.rotation = start.Rotation;
        }
    }
}
