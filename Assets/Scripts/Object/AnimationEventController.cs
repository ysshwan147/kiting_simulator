using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    private Controller controller;

    private void Awake() {
        controller = transform.parent.GetComponent<Controller>();
    }

    public void End()
    {        
        controller.setAnimating(false);
    }

    public void Hit()
    {
        controller.setHitting(true);
    }

    public void FootR()
    {
        
    }

    public void FootL()
    {

    }

}
