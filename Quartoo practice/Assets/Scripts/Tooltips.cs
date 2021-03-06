﻿using System.Collections.Generic;
using UnityEngine;

public class Tooltips : MonoBehaviour
{
    private List<int> usedTooltips = new List<int>();
    private int popupIndex = 0;
    private int tooltipIndex;
    private string[] tooltips =
    {
        /*0: Double click */"Click the piece once to select, and a second time to confirm you want to send it to your opponent. You can disable this to a single click in settings.",
        /*1: Help */"Forgot the rules or win conditions? Click the question mark above!",
        /*2: Staging*/"When you select a piece for your opponent, it is placed at the top of the barrel. When they pick one for you, it is placed at the bottom of the barrel.",
        /*3: Any piece */"Don't forget, you can select any piece (gold or silver) on either side of the board to send to your opponent.",
        /*4: Songs */"Tired of this song? Go to settings and skip to the next or previous song!",
        /*5: AISpeed */"AI going too fast for you? Slow it down by going to settings!"
    };

    public int getUsedTooltipLength()
    {
        return usedTooltips.Count;
    }

    public int getTooltipsArrayLength()
    {
        return tooltips.Length;
    }

    public string ShowTooltip()
    {
        tooltipIndex = Random.Range(0, tooltips.Length);

        while (usedTooltips.Contains(tooltipIndex))
            tooltipIndex = Random.Range(0, tooltips.Length);

        usedTooltips.Add(tooltipIndex);

        return tooltips[tooltipIndex];
    }
}