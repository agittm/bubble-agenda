using System;
using UnityEngine;

[Serializable]
public class BubbleData
{
    protected Side side;
    public Side Side => side;

    protected Opinion opinion;
    public Opinion Opinion => opinion;

    protected Stereotype stereotype;
    public Stereotype Stereotype => stereotype;

    public BubbleData() { }

    public BubbleData(Side side, Opinion opinion, Stereotype stereotype)
    {
        this.side = side;
        this.opinion = opinion;
        this.stereotype = stereotype;
    }
}
