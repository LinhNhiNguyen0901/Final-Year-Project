using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface saveAble
{
    // Start is called before the first frame update
    object SaveState();
    void LoadState(object state);
}
