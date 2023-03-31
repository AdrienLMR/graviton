using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

    //[Header("Objects")]

    //[Header("Values")]

    private Action DoAction;

	#region Unity Methods
	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{

	}

	public void Init()
	{

	}

	private void Update()
	{

	}

	public void GameLoop()
	{

	}
	#endregion

	#region State Machine		
	public void SetModeVoid()
	{
		DoAction = DoActionVoid;
	}

	public void SetModePlay()
	{
		DoAction = DoActionPlay;
	}

	private void DoActionVoid() { }

	private void DoActionPlay()
	{

	}

	#endregion

	#region Events
	#endregion
}