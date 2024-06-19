using _Game.Scripts;

public enum WorkingSlotState
{
    Occupied, Available
}
public class WorkingSlot
{
    public WorkingSlotState State;
    public Cell cell;
    public Crew crew;

    public WorkingSlot(Cell cell)
    {
        this.cell = cell; 
    }
}