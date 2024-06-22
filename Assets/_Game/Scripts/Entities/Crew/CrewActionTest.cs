using _Game.Scripts;
using UnityEngine;

public class CrewActionTest : MonoBehaviour
{
    public Crew crew;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            crew.OnStun(2);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            crew.OnAfterStun();
        }
    }
}
