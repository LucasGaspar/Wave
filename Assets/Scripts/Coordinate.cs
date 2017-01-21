﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Coordinate
{
	public int x;
	public int y;

	public override string ToString ()
	{
		return "("+x+", "+y+")";
	}
}
