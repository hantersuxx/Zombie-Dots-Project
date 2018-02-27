using Assets.Scripts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DragAndDropZombie : DragAndDrop
{
    protected override void OnSingleClick()
    {
        base.OnSingleClick();
        var zombieScript = GetComponent<ZombieController>();
        zombieScript.StopMovement();
        zombieScript.DropDownOnClosestPosition();
        zombieScript.AllowMovement();
    }

    protected override void OnDoubleClick()
    {
        base.OnDoubleClick();
        Destroy(gameObject);
    }
}
