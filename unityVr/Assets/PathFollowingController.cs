﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnitySteer;
using UnitySteer.Behaviors;

[RequireComponent(typeof(SteerForPathSimplified))]
public class PathFollowingController : MonoBehaviour 
{

	SteerForPathSimplified _steering;

	[SerializeField] public Transform _pathRoot;

	[SerializeField] bool _followAsSpline;

	// Use this for initialization
	void Start() 
	{
		_steering = GetComponent<SteerForPathSimplifiedCircular>();
		AssignPath();
	}


	void AssignPath()
	{
		// Get a list of points to follow;
		var pathPoints    = PathFromRoot(_pathRoot);
		var splinePathWay =  new SplinePathway(pathPoints, 1);
		
		_steering.Path = _followAsSpline ? splinePathWay : new Vector3Pathway(pathPoints, 1);
	}

	static List<Vector3> PathFromRoot(Transform root)
	{
		var children = new List<Transform>();
		foreach (Transform t in root)
		{
			if (t != null)
			{
				children.Add(t);
			}
		}
		return children.OrderBy(t => t.gameObject.name).Select(t => t.position).ToList();
	}


}
