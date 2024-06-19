using _Game.Scripts;

public enum WorkingSlotState
{
    Occupied, Available
}
public class WorkingSlot
{
    public WorkingSlotState State = WorkingSlotState.Available;
    public Cell cell;
    public Crew crew;

    public WorkingSlot(Cell cell)
    {
        this.cell = cell;
        State = WorkingSlotState.Available;
    }
}