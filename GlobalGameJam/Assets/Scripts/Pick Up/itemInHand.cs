﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    engine_broken, engine_repaired, oil, light, wheel, blue_paint, red_paint, yellow_paint
}
public class itemInHand : MonoBehaviour
{
    public Item inHand;
}
