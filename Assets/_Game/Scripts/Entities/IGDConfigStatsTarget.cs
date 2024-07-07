using _Game.Scripts.GD;

public interface IGDConfigStatsTarget
{
    public string Id { get; set; }
    public GDConfig GDConfig { get;}
    public StatsTemplate StatsTemplate { get; }
}