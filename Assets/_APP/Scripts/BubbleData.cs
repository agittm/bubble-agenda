using UnityEngine;

public class BubbleData
{
    protected Side side;
    public Side Side => side;

    protected OpinionType opinionType;
    public OpinionType OpinionType => opinionType;

    protected Stereotype stereotype;
    public Stereotype Stereotype => stereotype;

    public BubbleData() { }

    public BubbleData(Side side, OpinionType opinionType, Stereotype stereotype)
    {
        this.side = side;
        this.opinionType = opinionType;
        this.stereotype = stereotype;
    }
}
