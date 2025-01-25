public class Opinion
{
    protected OpinionType type;
    public OpinionType Type => type;

    protected float timelimit;
    public float Timelimit => timelimit;

    public Opinion() { }

    public Opinion(OpinionType type, float timelimit)
    {
        this.type = type;
        this.timelimit = timelimit;
    }
}
